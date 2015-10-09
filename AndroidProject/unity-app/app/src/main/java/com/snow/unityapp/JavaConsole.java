package com.snow.unityapp;

import android.app.Activity;
import android.os.Bundle;
import android.os.Handler;
import android.os.Message;
import android.view.MotionEvent;
import android.view.View;
import android.widget.TextView;

import com.snow.plugin.Debug;
import com.snow.plugin.Telnet;
import com.snow.plugin.Utils;

public class JavaConsole extends Activity {
    private View telnetButton;
    private TextView outputText;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        Debug.Log("JavaConsole.onCreate().");
        super.onCreate(savedInstanceState);

        setContentView(R.layout.activity_java_console);

        telnetButton = findViewById(R.id.button);
        outputText = (TextView)findViewById(R.id.textView);

        telnetButton.setOnTouchListener(new View.OnTouchListener() {
            @Override
            public boolean onTouch(View view, MotionEvent motionEvent) {
                Debug.Log("Start onTouch().");
                outputText.setText("Click Telnet.\n");

                Debug.Log("Before connect.");
                Telnet telnet = new Telnet("127.0.0.1", 27615);
                Debug.Log("After connect.");
                telnet.sendCommand("export LANG=en");
                String r2 = telnet.sendCommand("pwd");
                telnet.closeConnection();

                outputText.append("Telnet Result: \n");
                outputText.append(r2 + "\n");


                return false;
            }
        });
    }
}
