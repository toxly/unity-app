package com.snow.plugin;

import android.content.Context;
import android.util.Log;

import org.json.JSONObject;

public class NativeBridge {
    private static Context context;

    public static native String getCppVersion();

    public static void Init(final Context pContext) {
        Debug.Log("NativeBridge.Init()");
        context = pContext;
    }

    public static void NativeGetAppVersion() {
        Debug.Log("Java: call NativeGetAppVersion()");
        JSONObject json = DeviceInfo.getInfo(context);
        String notifyResult = json.toString();

        // -- notify
        com.unity3d.player.UnityPlayer.UnitySendMessage("Main Camera", "NotifyNativeGetAppVersion", notifyResult);

//        ActivityManager.StartJavaConsoleActivity();
    }

    public static void NativeGetCppVersion() {
        Debug.Log("Java: call NativeGetCppVersion()");
        String cppVersion = getCppVersion();
        com.unity3d.player.UnityPlayer.UnitySendMessage("Main Camera", "NotifyNativeGetCppVersion", "CPP Version: " + cppVersion);
    }
}
