using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Utils;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class MainScene : MonoBehaviour
    {
        public Button CSharpButton;
        public Button JavaButton;
        public Button CppButton;
        public Text OutputText;
        public static MainScene Instance;

        // Use this for initialization
        private void Start()
        {
            Debug.Log("C# run, version: " + GateKeeper.CSharpVersion);
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
            CppButton.onClick.AddListener(ClickCppButton);
        }

        private void ClickCSharpButton()
        {
            OutputText.text = "C#\nDLL Verion: " + GateKeeper.CSharpVersion;
        }

        private void ClickCppButton()
        {
            PlatformTools.NativeGetCppVersion();
        }

        private static void ClickJavaButton()
        {
            PlatformTools.NativeGetAppVersion();
        }

        public static void ShowJaveResult(string result)
        {
            Instance.OutputText.text = "Java\n"+result;
        }

        public static void ShowCppResult(string result)
        {
            Instance.OutputText.text = "C++\n" + result;
        }
    }
}