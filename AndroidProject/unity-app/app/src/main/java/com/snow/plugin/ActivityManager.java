/**
 * Created by Snow on 2015-10-7.
 */
package com.snow.plugin;

import com.snow.unityapp.JavaConsole;

import android.content.Intent;
import android.os.Handler;

public class ActivityManager {
    private static Handler handler;
    private static final String tag = "UnityApp";

    public static void StartJavaConsoleActivity() {
        Debug.Log("call StartJavaConsoleActivity()");

        Utils.runOnUIThread(new Runnable() {
            @Override
            public void run() {
                Intent target = new Intent(Utils.activity(), JavaConsole.class);
                Utils.activity().startActivityForResult(target, 10000);
            }
        });
    }
}