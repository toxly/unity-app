package com.snow.unityapp;

import android.app.Activity;
import android.os.AsyncTask;
import android.os.Bundle;
import android.view.View;
import android.widget.TextView;

import com.snow.plugin.Debug;
import com.snow.plugin.Telnet;

public class JavaConsole extends Activity {
    private View telnetButton;
    public static TextView outputText;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        Debug.Log("JavaConsole.onCreate().");
        super.onCreate(savedInstanceState);

        setContentView(R.layout.activity_java_console);

        telnetButton = findViewById(R.id.button);
        outputText = (TextView)findViewById(R.id.textView);

        telnetButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Debug.Log("Start onClick().");
                outputText.setText("Click Telnet.\n");

                NetTask netTask = new NetTask();
                netTask.execute();
            }
        });
    }
}

/**
 *
 * @author Snow
 * @desc AsyncTask<>的参数类型由用户设定，这里设为三个String<br />
 *       第一个String代表输入到任务的参数类型，也即是doInBackground()的参数类型<br />
 *       第二个String代表处理过程中的参数类型，也就是doInBackground()执行过程中的产出参数类型，通过publishProgress
 *       () 传递给onProgressUpdate()一般用来更新界面<br />
 *       第三个String代表任务结束的产出类型，也就是doInBackground()的返回值类型，和onPostExecute()的参数类型<br />
 */
final class NetTask extends AsyncTask<String, String, String> {

    /**
     * 后台任务开始执行之前调用，用于进行一些界面上的初始化操作，比如显示一个进度条对话框等。
     */
    @Override
    protected void onPreExecute() {
        Debug.Log("onPreExecute().");
    }

    /**
     * 这个方法中的所有代码都会在子线程中运行，我们应该在这里去处理所有的耗时任务。
     * 任务一旦完成就可以通过return语句来将任务的执行结果进行返回，如果AsyncTask的第三个泛型参数指定的是Void
     * ，就可以不返回任务执行结果。
     * 注意，在这个方法中是不可以进行UI操作的，如果需要更新UI元素，比如说反馈当前任务的执行进度，可以调用publishProgress
     * (Progress...)方法来完成。
     */
    @Override
    protected String doInBackground(String... params) {
        Debug.Log("doInBackground().");
        Debug.Log("Before connect.");
        Telnet telnet = new Telnet("127.0.0.1", 27615);
        Debug.Log("After connect.");
        telnet.sendCommand("export LANG=en");
        String r2 = telnet.sendCommand("pwd");
        telnet.closeConnection();
        Debug.Log("result is:" + r2);

        String telnetResult = "";
        telnetResult += "Telnet Result: \n";
        telnetResult += r2 + "\n";

        publishProgress(telnetResult);
        return "";
    }
    /**
     * 当在后台任务中调用了publishProgress(Progress...)方法后，这个方法就很快会被调用，
     * 方法中携带的参数就是在后台任务中传递过来的。 在这个方法中可以对UI进行操作，利用参数中的数值就可以对界面元素进行相应的更新。
     */
    @Override
    protected void onProgressUpdate(String... progress) {
        Debug.Log("onProgressUpdate().");
        JavaConsole.outputText.append(progress[0]);
    }

    /**
     * 当后台任务执行完毕并通过return语句进行返回时，这个方法就很快会被调用。返回的数据会作为参数传递到此方法中，
     * 可以利用返回的数据来进行一些UI操作，比如说提醒任务执行的结果，以及关闭掉进度条对话框等。
     */
    @Override
    protected void onPostExecute(String result) {
        Debug.Log("onPostExecute()");
    }
}
