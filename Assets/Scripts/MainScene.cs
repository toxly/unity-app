using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Utils;
using UnityEngine.UI;
using Helpshift;

namespace Assets.Scripts
{
    public class MainScene : MonoBehaviour
    {
        public Button CSharpButton;
        public Button JavaButton;
        public Button CppButton;
        public Text OutputText;
        public static MainScene Instance;
        private HelpshiftSdk help;


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

        // -- https://developers.helpshift.com/unity/sdk-configuration-ios/#install-config
        public void Init()
        {
            CSharpButton.onClick.AddListener(ClickCSharpButton);
            JavaButton.onClick.AddListener(ClickJavaButton);
            CppButton.onClick.AddListener(ClickCppButton);

            help = HelpshiftSdk.getInstance();
            Dictionary<string, object> configMap = new Dictionary<string, object>();
            configMap.Add("unityGameObject", "UIGetVersion");
            configMap.Add("enableInAppNotification", "yes");

#if UNITY_IOS
            help.registerForLocalNotifications();
		    help.registerHelpshiftDeepLinking();
#endif
            help.install(GateKeeper.HelpShiftApiKey, GateKeeper.HelpShiftDomainName, GateKeeper.HelpShiftAppId, configMap);

            help.setNameAndEmail("Snow Li", "toxly@msn.com");
            help.setUserIdentifier("userid.10015");

            getNofify();
        }

        private void getNofify()
        {
            Debug.Log("Start getNotificationCount");
            help.getNotificationCount(true);
        }

        public void didReceiveNotificationCount(string count)
        {
            Debug.Log("Notification async count : " + count);
            OutputText.text = "Notification async count : " + count;
        }

        public void didReceiveInAppNotificationCount(string count)
        {
            Debug.Log("In-app Notification count : " + count);
            OutputText.text = "Notification in-app async count : " + count;
        }
        public void newConversationStarted(string newConversationMessage)
        {
            Debug.Log("newConversationStarted: " + newConversationMessage);
        }
        public void userRepliedToConversation(string newMessage)
        {
            Debug.Log("userRepliedToConversation: " + newMessage);

            if (newMessage.Equals(HelpshiftSdk.HSUserAcceptedTheSolution))
            {
            }
        }
        public void userCompletedCustomerSatisfactionSurvey(string json)
        {
            Debug.Log("userCompletedCustomerSatisfactionSurvey: " + json);
        }



        private void ClickCSharpButton()
        {
            OutputText.text = "C#\nDLL Verion: " + GateKeeper.CSharpVersion;
            ShowHelpShift();
        }

        private void ClickCppButton()
        {
//            PlatformTools.NativeGetCppVersion();
            getNofify();
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


        // 
        private void ShowHelpShift()
        {
            Dictionary<string, object> configD = new Dictionary<string, object>();
            configD.Add("gotoConversationAfterContactUs", "yes");
            configD.Add("enableContactUs", "yes");

            Dictionary<string, object> configMap = new Dictionary<string, object>();
            configMap.Add("Level", "91");
            configMap.Add("Spend", "48.55 USD");
            configMap.Add("Device Timestamp", DateTime.UtcNow.ToLongTimeString());
            configMap.Add(HelpshiftSdk.HSTAGSKEY, new string[] { "vip","tier5","hero" });
            configD.Add(HelpshiftSdk.HSCUSTOMMETADATAKEY, configMap);
            help.showFAQs(configD);
        }
    }
}