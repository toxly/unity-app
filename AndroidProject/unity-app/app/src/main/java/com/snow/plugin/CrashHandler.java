package com.snow.plugin;

import android.content.Context;
import android.content.SharedPreferences.Editor;
import android.os.AsyncTask;

import org.apache.http.NameValuePair;
import org.apache.http.client.entity.UrlEncodedFormEntity;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.impl.client.DefaultHttpClient;
import org.apache.http.message.BasicNameValuePair;
import org.apache.http.params.CoreProtocolPNames;
import org.apache.http.protocol.HTTP;

import java.io.PrintWriter;
import java.io.StringWriter;
import java.io.Writer;
import java.lang.Thread.UncaughtExceptionHandler;
import java.util.ArrayList;
import java.util.List;
import java.util.UUID;

public class CrashHandler implements UncaughtExceptionHandler {

    private static CrashHandler _instance;
    private Context context;

    public static String errorContent = "";

    // 私有化构造方法
    private CrashHandler() {

    }

    public static synchronized CrashHandler getInstance() {
        if (_instance != null) {
            return _instance;
        } else {
            _instance = new CrashHandler();
            return _instance;
        }
    }

    public void init(Context context) {
        this.context = context;
        Thread.setDefaultUncaughtExceptionHandler(this);

        CrashHandlerTask.client = new DefaultHttpClient();
    }

    @Override
    public void uncaughtException(Thread arg0, Throwable arg1) {
        // 把错误的堆栈信息 获取出来
        String errorInfo = getErrorInfo(arg1);
        String uuidKey = UUID.nameUUIDFromBytes(errorInfo.getBytes()).toString();

        String existsException = Utils.getSharedPreferences("uncaughtException").getString(uuidKey, "");
        if (!existsException.equals("")) {
            // -- this uncaughtException has got
            Debug.Log("Exception has got yet: " + errorInfo);
            Debug.Log("kill self() ");
            android.os.Process.killProcess(android.os.Process.myPid());
            return;
        }

        Editor editor = Utils.getSharedPreferences("uncaughtException").edit();
        editor.putString(uuidKey, "Mark");
        editor.commit();

        String content = DeviceInfo.getInfo(context).toString();
        String msg = content + "\nStack:\n" + errorInfo;

        Debug.Log("uncaughtException: " + msg);

        errorContent = msg;

//        sendExceptionMsg();
    }

    private String getErrorInfo(Throwable arg1) {
        Writer writer = new StringWriter();
        PrintWriter pw = new PrintWriter(writer);
        arg1.printStackTrace(pw);
        pw.close();
        return writer.toString();
    }

    public static void sendExceptionMsg() {
        CrashHandlerTask postTask = new CrashHandlerTask();
        postTask.execute();
    }
}

final class CrashHandlerTask extends AsyncTask<String, String, String> {
    public static DefaultHttpClient client = null;

    @Override
    protected String doInBackground(String... params) {
        String loginStatus = "";

        String url = "";

        try {
            HttpPost httppost = new HttpPost(url);
            httppost.getParams().setBooleanParameter(CoreProtocolPNames.USE_EXPECT_CONTINUE, false);
            List<NameValuePair> parameters = new ArrayList<NameValuePair>();
            parameters.add(new BasicNameValuePair("msg", CrashHandler.errorContent));
            httppost.setEntity(new UrlEncodedFormEntity(parameters, HTTP.UTF_8));
            client.execute(httppost);
        } catch (Throwable e) {
            Debug.Log(e.toString() + "");
        }

        // 重启
        Utils.restartGame();
        return loginStatus;
    }

    @Override
    protected void onPostExecute(String result) {
    }
}
