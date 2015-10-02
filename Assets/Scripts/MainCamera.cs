using UnityEngine;
using System.Collections;

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
            MainScene.ShowJaveResult(result);
        }

        public void NotifyNativeGetCppVersion(string result)
        {
            Debug.Log("C#: NotifyNativeGetCppVersion, result: " + result);
            MainScene.ShowCppResult(result);
        }
    }
}
