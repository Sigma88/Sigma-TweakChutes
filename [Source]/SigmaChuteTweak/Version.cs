using UnityEngine;


namespace SigmaTweakChutesPlugin
{
    [KSPAddon(KSPAddon.Startup.Instantly, true)]
    public class Version : MonoBehaviour
    {
        static bool first = true;
        public static readonly System.Version number = new System.Version("0.3.1");

        void Awake()
        {
            if (first)
            {
                first = false;
                Debug.Log("[SigmaLog] Version Check:   Sigma TweakChutes v" + number);
            }
        }
    }
}
