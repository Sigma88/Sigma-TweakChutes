namespace SigmaTweakChutesPlugin
{
    public class TweakChute : ModuleParachute
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

        protected override void FixedUpdate()
        {
            if (deploymentState == deploymentStates.SEMIDEPLOYED)
            {
                base.FixedUpdate();
            }
            else
            {
                float temp = deployAltitude;
                deployAltitude = 0;
                base.FixedUpdate();
                deployAltitude = temp;
            }
        }

        public override void OnStart(StartState state)
        {
            UI_FloatRange uI_Altitude = (UI_FloatRange)(Fields)["deployAltitude"].uiControlFlight;
            UI_FloatRange uI_Pressure = (UI_FloatRange)(Fields)["minAirPressureToOpen"].uiControlFlight;

            if (HighLogic.LoadedScene == GameScenes.EDITOR)
            {
                uI_Altitude = (UI_FloatRange)(Fields)["deployAltitude"].uiControlEditor;
                uI_Pressure = (UI_FloatRange)(Fields)["minAirPressureToOpen"].uiControlEditor;
            }

            uI_Altitude.minValue = altitudeSliderMin;
            uI_Altitude.maxValue = altitudeSliderMax;
            uI_Altitude.stepIncrement = altitudeSliderStep;

            clampMinAirPressure = pressureSliderMin >= 0 ? pressureSliderMin : clampMinAirPressure;
            uI_Pressure.minValue = pressureSliderMin >= 0 ? pressureSliderMin : clampMinAirPressure;
            uI_Pressure.maxValue = pressureSliderMax;
            uI_Pressure.stepIncrement = pressureSliderStep;

            base.OnStart(state);
        }
    }
}
