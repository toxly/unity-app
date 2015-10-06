package com.snow.plugin;

import android.app.ActivityManager;
import android.content.Context;
import android.content.pm.PackageInfo;
import android.content.pm.PackageManager;
import android.os.Build;
import android.text.format.Formatter;
import android.util.DisplayMetrics;
import android.util.Log;

import org.apache.http.HttpEntity;
import org.apache.http.HttpResponse;
import org.apache.http.client.ClientProtocolException;
import org.apache.http.client.HttpClient;
import org.apache.http.client.methods.HttpGet;
import org.apache.http.impl.client.DefaultHttpClient;
import org.apache.http.util.EntityUtils;
import org.json.JSONException;
import org.json.JSONObject;

import java.io.BufferedReader;
import java.io.FileReader;
import java.io.IOException;

/**
 * Created by Snow on 2015-10-4.
 */
public class DeviceInfo {
    private static JSONObject jsonInfo = new JSONObject();
    private static Context context;

    public static JSONObject getInfo(Context pContext) {
        context = pContext;

        getAppInfo();
        getSystemAvailableMemInfo();
        getSystemTotalMemInfo();
        getSystemCPUInfo();
//        getExternalIP();
        getDeviceInfo();

        return jsonInfo;
    }

    private static void getAppInfo() {
        try {
            PackageInfo pInfo = context.getPackageManager().getPackageInfo(context.getPackageName(), 0);
            jsonInfo.put("VersionName", pInfo.versionName);
            jsonInfo.put("versionCode", pInfo.versionCode);
        } catch (PackageManager.NameNotFoundException e) {
            e.printStackTrace();
        } catch (JSONException e) {
            e.printStackTrace();
        }
    }

    private static void getSystemAvailableMemInfo() {
        ActivityManager am = (ActivityManager) context.getSystemService(Context.ACTIVITY_SERVICE);
        ActivityManager.MemoryInfo mi = new ActivityManager.MemoryInfo();
        am.getMemoryInfo(mi);

        String sysAvailableMem = Formatter.formatFileSize(context, mi.availMem);

        try {
            jsonInfo.put("AvailableMemory", sysAvailableMem);
        } catch (JSONException e) {
            e.printStackTrace();
        }
    }

    private static void getSystemTotalMemInfo() {
        String memInfoFile = "/proc/meminfo";
        String lineInfo;
        String[] arrInfo;
        long totalMemory = 0;

        try {
            FileReader fr = new FileReader(memInfoFile);
            BufferedReader br = new BufferedReader(fr, 8192);
            lineInfo = br.readLine();
            arrInfo = lineInfo.split("\\s+");

            totalMemory = Integer.valueOf(arrInfo[1]).intValue() * 1024;
            br.close();
        } catch (Throwable e) {
            e.printStackTrace();
        }

        String sysTotalMem = Formatter.formatFileSize(context, totalMemory);

        try {
            jsonInfo.put("TotalMemory", sysTotalMem);
        } catch (JSONException e) {
            e.printStackTrace();
        }
    }

    private static void getSystemCPUInfo() {
        String memInfoFile = "/proc/cpuinfo";
        String lineInfo;
        String[] arrInfo;
        String cpuInfo = "";

        try {
            FileReader fr = new FileReader(memInfoFile);
            BufferedReader br = new BufferedReader(fr, 8192);
            lineInfo = br.readLine();
            arrInfo = lineInfo.split(":+");

            cpuInfo = arrInfo[1];
            br.close();
        } catch (Throwable e) {
            e.printStackTrace();
        }

        try {
            jsonInfo.put("CPU", cpuInfo);
        } catch (JSONException e) {
            e.printStackTrace();
        }
    }

    private static void getExternalIP() {
        try {
            String externalIP = "";
            HttpClient httpclient = new DefaultHttpClient();
            HttpGet httpget = new HttpGet("http://ifcfg.me/ip");
            HttpResponse response = httpclient.execute(httpget);

            HttpEntity entity = response.getEntity();
            if (entity != null) {
                String content = EntityUtils.toString(entity).replace("\n", "");
                if (content.length() < 20) {
                    externalIP = content;
                }
                Log.i("ExternalIP: ", externalIP);
            }

            jsonInfo.put("ExternalIP", externalIP);

        } catch (JSONException e) {
            e.printStackTrace();
        } catch (ClientProtocolException e) {
            e.printStackTrace();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    private static void getDeviceInfo() {
        try {
            jsonInfo.put("DeviceType", Build.MODEL + "-" + Build.DEVICE + "-" + Build.MANUFACTURER);
            jsonInfo.put("OsVersion", Build.DISPLAY + "-" + Build.VERSION.SDK_INT);
            jsonInfo.put("Serial", Build.SERIAL);

            // -- 分辨率
            DisplayMetrics dm = new DisplayMetrics();
            NativeBridge.activity().getWindowManager().getDefaultDisplay().getMetrics(dm);

            int width = dm.widthPixels; // 宽度（PX）
            int height = dm.heightPixels; // 高度（PX）

            float density = dm.density; // 密度（0.75 / 1.0 / 1.5）
            int densityDpi = dm.densityDpi; // 密度DPI（120 / 160 / 240）

            jsonInfo.put("Resolution", width + "x" + height);
            jsonInfo.put("DPI", densityDpi);
            jsonInfo.put("Density", density);
        } catch (JSONException e) {
            e.printStackTrace();
        }
    }
}
