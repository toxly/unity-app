package com.snow.unityapp;

import android.app.Activity;
import android.os.Bundle;
import android.os.Handler;
import android.view.MotionEvent;
import android.view.View;
import android.widget.ImageView;

import com.snow.plugin.Debug;


/**
 * Created by Snow on 2016-1-14.
 */
public class BootActivity extends Activity {
    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        setContentView(R.layout.activity_boot_activity);

        new Handler(getMainLooper()).postDelayed(new Runnable() {
            public void run() {
                finish();
            }
        }, 2000L);

        ImageView imageView = (ImageView) findViewById(R.id.bootBGImage);
        imageView.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                finish();
            }
        });

        Debug.Log("BootActivity onCreate!");
    }

    @Override
    public boolean onTouchEvent(MotionEvent event) {
        finish();
        return super.onTouchEvent(event);
    }
}
