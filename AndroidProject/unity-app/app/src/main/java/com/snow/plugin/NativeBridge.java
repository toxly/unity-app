package com.snow.plugin;

import android.content.Context;
import android.util.Log;

import com.snow.unityapp.MainActivity;

import org.json.JSONObject;

public class NativeBridge {
    public static String tag = "UnityAPP";
    private static Context context;

    public static native String getCppVersion();

    public static void Init(final Context pContext) {
        Log.d(tag, "NativeBridge.Init()");
        context = pContext;
    }

    public static MainActivity activity() {
        if (null == context) {
            return null;
        }

        MainActivity activity = (MainActivity) context;
        return activity;
    }

    public static void NativeGetAppVersion() {
        Log.d(tag, "Java: call NativeGetAppVersion()");
        JSONObject json = DeviceInfo.getInfo(context);
        String notifyResult = json.toString();
        // -- notify
        com.unity3d.player.UnityPlayer.UnitySendMessage("Main Camera", "NotifyNativeGetAppVersion", notifyResult);
    }

    public static void NativeGetCppVersion() {
        Log.d(tag, "Java: call NativeGetCppVersion()");
        String cppVersion = getCppVersion();
        com.unity3d.player.UnityPlayer.UnitySendMessage("Main Camera", "NotifyNativeGetCppVersion", "CPP Version: " + cppVersion);
    }
}
