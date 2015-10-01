package com.snow.plugin;

import org.json.JSONObject;

import android.content.Context;
import android.content.pm.PackageInfo;
import android.util.Log;

public class NativeBridge {
	public static String tag = "UnityAPP";
	private static Context context;
	private static String appVersionName = "";
	private static int appVersionCode = 0;
	
	public static void Init(final Context pContext) {
		context = pContext.getApplicationContext();
	}
	
	public static void NativeGetAppVersion() {
		Log.d(tag, "Java: call NativeGetAppVersion()");
		getAppInfo();
		NotifyNativeGetAppVersion();
	}
	
	public static void NotifyNativeGetAppVersion() {
		Log.d(tag, "Java: call NotifyNativeGetAppVersion()");
		JSONObject json = new JSONObject();
		String notifyResult = "";
		try {
			json.put("VersionName", appVersionName);
			json.put("VersionCode", appVersionCode);
			notifyResult = json.toString();
		} catch (Throwable e) {
			e.printStackTrace();
		}
		
		com.unity3d.player.UnityPlayer.UnitySendMessage("Main Camera", "NotifyNativeGetAppVersion", notifyResult);
	}
	
	private static void getAppInfo() {
		try {
			PackageInfo pInfo = context.getPackageManager().getPackageInfo(context.getPackageName(), 0);
			appVersionName = pInfo.versionName;
			appVersionCode = pInfo.versionCode;
		} catch (Throwable e) {
			e.printStackTrace();
		}
	}
}