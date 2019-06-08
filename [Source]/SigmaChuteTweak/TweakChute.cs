namespace SigmaTweakChutesPlugin
{
    public class TweakChute : ModuleParachute
    {
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
    }
}
