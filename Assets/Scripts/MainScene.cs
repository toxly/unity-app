﻿using System;
using UnityEngine;
using System.Collections;
using Assets.Scripts.Utils;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class MainScene : MonoBehaviour
    {
        public Button CSharpButton;
        public Button JavaButton;
        public Text OutputText;
        public static MainScene Instance;

        // Use this for initialization
        private void Start()
        {
            Instance = this;
            Init();
        }

        // Update is called once per frame
        private void Update()
        {

        }

        public void Init()
        {
            CSharpButton.onClick.AddListener(ClickCSharpButton);
            JavaButton.onClick.AddListener(ClickJavaButton);
        }

        private void ClickCSharpButton()
        {
            OutputText.text = "This is output by c#.";
        }

        private static void ClickJavaButton()
        {
            PlatformTools.NativeGetAppVersion();
        }

        public static void ShowJaveResult(string result)
        {
            Instance.OutputText.text = "This is output by java:\n"+result;
        }
    }
}