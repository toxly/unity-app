package com.snow.plugin;
import android.util.Log;

/**
 * Created by Snow on 2015-10-8. 
 */
public class Debug {
    private final static Boolean LogEnabled = true;

    public static void Info(String logStr) {
        if (LogEnabled) {
            Log.i(Utils.tag, logStr);
        }
    }
    public static void Log(String logStr) {
        if (LogEnabled) {
            Log.d(Utils.tag, logStr);
        }
    }
    public static void Warn(String logStr) {
        if (LogEnabled) {
            Log.w(Utils.tag, logStr);
        }
    }
    public static void Error(String logStr) {
        Log.e(Utils.tag, logStr);
    }
}
