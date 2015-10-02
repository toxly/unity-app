using System;
using UnityEngine;
using System.Collections;
using Assets.Scripts.Utils;

public class Debug
{
    public static void Log(object message, UnityEngine.Object obj = null)
    {
        if (GateKeeper.LogEnabled)
        {
            DateTime now = DateTime.Now;
            string nowTime = now.ToString("[HH:mm:ss zz] ");

            UnityEngine.Debug.Log(nowTime + message, obj);
        }
    }
}