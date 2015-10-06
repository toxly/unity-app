using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Utils
{
    public class GateKeeper
    {
        private static bool _logEnabled = true;
        private static string _cSharpVersion = "c#_1.0";

        public static string CSharpVersion
        {
            get
            {
                return _cSharpVersion;
            }

            set
            {
                _cSharpVersion = value;
            }
        }

        public static bool LogEnabled
        {
            get
            {
                return _logEnabled;
            }

            set
            {
                _logEnabled = value;
            }
        }
    }
}

