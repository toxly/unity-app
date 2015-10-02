package com.snow.plugin;

import org.json.JSONObject;

import android.app.ActivityManager;
import android.content.Context;
import android.content.pm.PackageInfo;
import android.text.format.Formatter;
import android.util.Log;

import org.apache.http.HttpEntity;
import org.apache.http.HttpResponse;
import org.apache.http.client.HttpClient;
import org.apache.http.client.methods.HttpGet;
import org.apache.http.conn.util.InetAddressUtils;
import org.apache.http.impl.client.DefaultHttpClient;
import org.apache.http.util.EntityUtils;

import java.io.BufferedReader;
import java.io.FileReader;

public class NativeBridge {
	public static String tag = "UnityAPP";
	private static Context context;
	private static String appVersionName = "";
	private static int appVersionCode = 0;
	private static String sysAvailableMem = "";
	private static String sysTotalMem = "";
	private static String externalIP = "";

	public static native String getCppVersion();

	public static void Init(final Context pContext) {
		Log.d(tag, "NativeBridge.Init()");
		context = pContext.getApplicationContext();
	}
	
	public static void NativeGetAppVersion() {
		Log.d(tag, "Java: call NativeGetAppVersion()");
		getAppInfo();
		getSystemAvailableMemInfo();
		getSystemTotalMemInfo();
		getExternalIP();

		// -- notify
		NotifyNativeGetAppVersion();
	}

	public static void NativeGetCppVersion() {
		Log.d(tag, "Java: call NativeGetCppVersion()");
		String cppVersion = getCppVersion();
		com.unity3d.player.UnityPlayer.UnitySendMessage("Main Camera", "NotifyNativeGetCppVersion", cppVersion);
	}
	
	public static void NotifyNativeGetAppVersion() {
		Log.d(tag, "Java: call NotifyNativeGetAppVersion()");
		JSONObject json = new JSONObject();
		String notifyResult = "";
		try {
			json.put("VersionName", appVersionName);
			json.put("VersionCode", appVersionCode);
			json.put("AvailableMemory", sysAvailableMem);
			json.put("TotalMemory", sysTotalMem);
			json.put("ExternalIP", externalIP);
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

	private static void getSystemAvailableMemInfo() {
		ActivityManager am = (ActivityManager)context.getSystemService(Context.ACTIVITY_SERVICE);
		ActivityManager.MemoryInfo mi = new ActivityManager.MemoryInfo();
		am.getMemoryInfo(mi);

		sysAvailableMem = Formatter.formatFileSize(context, mi.availMem);
	}

	private static void getSystemTotalMemInfo() {
		String meminfoFile = "/proc/meminfo";
		String lineInfo;
		String[] arrInfo;
		long totalMemero = 0;

		try {
			FileReader fr = new FileReader(meminfoFile);
			BufferedReader br = new BufferedReader(fr, 8192);
			lineInfo = br.readLine();
			arrInfo = lineInfo.split("\\s+");

			totalMemero = Integer.valueOf(arrInfo[1]).intValue() * 1024;
			br.close();
		} catch (Throwable e) {
			e.printStackTrace();
		}

		sysTotalMem = Formatter.formatFileSize(context, totalMemero);
	}

	private static void getExternalIP() {
		try {
			HttpClient httpclient = new DefaultHttpClient();
			HttpGet httpget = new HttpGet("http://ifcfg.me/ip");
			HttpResponse response = httpclient.execute(httpget);

			HttpEntity entity = response.getEntity();
			if (entity != null) {
				String content = EntityUtils.toString(entity).replace("\n","");
				if (content.length() < 20) {
					externalIP = content;
				}
				Log.i("externalip: ",externalIP);
			}
		}
		catch (Exception e)
		{
			e.printStackTrace();
		}
	}
}
