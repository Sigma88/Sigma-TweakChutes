namespace SigmaTweakChutesPlugin
{
    public class TweakChute : PartModule
    {
        [KSPField]
        public float altitudeSliderMin = 50f;
        [KSPField]
        public float altitudeSliderMax = 5000f;
        [KSPField]
        public float altitudeSliderStep = 50f;

        [KSPField]
        public float pressureSliderMin = -1;
        [KSPField]
        public float pressureSliderMax = 0.75f;
        [KSPField]
        public float pressureSliderStep = 0.01f;

        float temp = -1;

        void EarlyFixedUpdate()
        {
            ModuleParachute MP = GetComponent<ModuleParachute>();
            if (MP.deploymentState == ModuleParachute.deploymentStates.ACTIVE)
            {
                temp = MP.deployAltitude;
                MP.deployAltitude = 0;
            }
        }

        void LateFixedUpdate()
        {
            ModuleParachute MP = GetComponent<ModuleParachute>();
            if (temp >= 0)
            {
                MP.deployAltitude = temp;
                temp = -1;
            }
        }

        public override void OnStart(StartState state)
        {
            TimingManager.FixedUpdateAdd(TimingManager.TimingStage.Early, EarlyFixedUpdate);
            TimingManager.FixedUpdateAdd(TimingManager.TimingStage.Late, LateFixedUpdate);

            ModuleParachute MP = GetComponent<ModuleParachute>();

            UI_FloatRange uI_Altitude = (UI_FloatRange)(MP.Fields)["deployAltitude"].uiControlFlight;
            UI_FloatRange uI_Pressure = (UI_FloatRange)(MP.Fields)["minAirPressureToOpen"].uiControlFlight;

            if (HighLogic.LoadedScene == GameScenes.EDITOR)
            {
                uI_Altitude = (UI_FloatRange)(MP.Fields)["deployAltitude"].uiControlEditor;
                uI_Pressure = (UI_FloatRange)(MP.Fields)["minAirPressureToOpen"].uiControlEditor;
            }

            uI_Altitude.minValue = altitudeSliderMin;
            uI_Altitude.maxValue = altitudeSliderMax;
            uI_Altitude.stepIncrement = altitudeSliderStep;

            MP.clampMinAirPressure = pressureSliderMin >= 0 ? pressureSliderMin : MP.clampMinAirPressure;
            uI_Pressure.minValue = pressureSliderMin >= 0 ? pressureSliderMin : MP.clampMinAirPressure;
            uI_Pressure.maxValue = pressureSliderMax;
            uI_Pressure.stepIncrement = pressureSliderStep;
        }

        public void OnDestroy()
        {
            TimingManager.FixedUpdateRemove(TimingManager.TimingStage.Early, EarlyFixedUpdate);
            TimingManager.FixedUpdateRemove(TimingManager.TimingStage.Late, LateFixedUpdate);
        }
    }
}
