package com.snow.plugin;
import org.apache.commons.net.telnet.TelnetClient;

import java.io.IOException;
import java.io.InputStream;
import java.io.PrintStream;
import java.net.SocketException;

/**
 * Created by Snow on 2015-10-9.
 */
public class Telnet {
    private TelnetClient tc = new TelnetClient();
    private String ip = "";
    private int port = 80;
    private InputStream in;
    private PrintStream out;
    private char prompt = '$';

    public Telnet(String ip, int port) {
        this.ip = ip;
        this.port = port;

        try {
            tc.connect(this.ip, this.port);
            in = tc.getInputStream();
            out = new PrintStream(tc.getOutputStream());
        } catch (SocketException e) {
            e.printStackTrace();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    public String sendCommand(String command) {
        try {
            write(command);
            return readUntil(prompt + " ");
        } catch (Exception e) {
            e.printStackTrace();
        }
        return null;
    }

    private void write(String value) {
        out.println(value);
        out.flush();
    }

    private String readUntil(String pattern) {
        try {
            char lastChar = pattern.charAt(pattern.length() - 1);
            StringBuffer sb = new StringBuffer();
            char ch = (char) in.read();
            while (true) {
                sb.append(ch);
                if (ch == lastChar) {
                    if (sb.toString().endsWith(pattern)) {
                        return sb.toString();
                    }
                }
                ch = (char) in.read();
            }
        } catch (Exception e) {
            e.printStackTrace();
        }
        return null;
    }

    public void closeConnection() {
        try {
            tc.disconnect();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }
}
