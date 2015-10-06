using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts
{
    public class MainCamera : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        // -- can not be static
        public void NotifyNativeGetAppVersion(string result)
        {
            Debug.Log("C#: NotifyNativeGetAppVersion, result: " + result);

            string resultStr = "";
            Dictionary<string, object> jsonDictionary = (Dictionary<string, object>)fastJSON.JSON.Parse(result);
            foreach (KeyValuePair<string, object> kp in jsonDictionary)
            {
                resultStr += kp.Key + ": " + kp.Value.ToString() + "\n";
            }
            MainScene.ShowJaveResult(resultStr);
        }

        public void NotifyNativeGetCppVersion(string result)
        {
            Debug.Log("C#: NotifyNativeGetCppVersion, result: " + result);
            MainScene.ShowCppResult(result);
        }
    }
}
