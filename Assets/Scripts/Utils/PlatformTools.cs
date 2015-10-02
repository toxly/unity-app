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
            AndroidJavaClass jc = new AndroidJavaClass("com.snow.plugin.NativeBridge");
            jc.CallStatic("NativeGetAppVersion");
#endif
        }

        public static void NativeGetCppVersion()
        {
#if !UNITY_EDITOR && UNITY_ANDROID
            AndroidJavaClass jc = new AndroidJavaClass("com.snow.plugin.NativeBridge");
            jc.CallStatic("NativeGetCppVersion");
#endif
        }
    }
}
