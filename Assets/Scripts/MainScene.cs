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

        // Use this for initialization
        private void Start()
        {
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
    }
}