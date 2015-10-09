package com.snow.plugin;

import android.app.AlarmManager;
import android.app.PendingIntent;
import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Handler;

import com.snow.unityapp.MainActivity;

/**
 * Created by Snow on 2015-10-8.
 */
public class Utils {
    public static String tag = "UnityApp";
    private static Handler appHandler;
    private static Context context;

    public static void init(final Context pContext) {
        context = pContext;
        appHandler = new Handler(context.getMainLooper());
    }

    public static MainActivity activity() {
        if (null == context) {
            return null;
        }

        MainActivity activity = (MainActivity) context;
        return activity;
    }

    public static void runOnUIThread(final Runnable pRunnable) {
        if (appHandler != null) {
            appHandler.post(pRunnable);
        }
    }

    public static SharedPreferences getSharedPreferences(String spName) {
        MainActivity activity = (MainActivity) context;
        return activity.getSharedPreferences(spName, context.MODE_PRIVATE);
    }

    public static void restartGame() {
        Debug.Log("restartGmae after 500ms.");
        Intent intent = new Intent(context.getApplicationContext(), MainActivity.class);
        PendingIntent restartIntent = PendingIntent.getActivity(
                context.getApplicationContext(), 0, intent,
                Intent.FILL_IN_ACTION);
        //退出程序
        AlarmManager mgr = (AlarmManager)context.getSystemService(Context.ALARM_SERVICE);
        mgr.set(AlarmManager.RTC, System.currentTimeMillis() + 500,  restartIntent); // 500ms后重启应用

        Debug.Log("kill self() ");
        android.os.Process.killProcess(android.os.Process.myPid());
    }
}
