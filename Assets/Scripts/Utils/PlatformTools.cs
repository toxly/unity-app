using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Utils
{
    public class PlatformTools
    {
        public static void NativeGetAppVersion()
        {
            Debug.Log("PlatformTools.PTGetAppVersion()");
#if !UNITY_EDITOR && UNITY_ANDROID
            AndroidJavaClass jc = new AndroidJavaClass("com.snow.unityapp.NativeBridge");
            jc.CallStatic("NativeGetAppVersion");
#endif
        }
    }
}
