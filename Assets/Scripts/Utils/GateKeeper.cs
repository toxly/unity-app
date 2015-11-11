using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Utils
{
    public class GateKeeper
    {
        private static bool _logEnabled = true;
        private static string _cSharpVersion = "c#_1.0";
        private static string _helpShiftApiKey = "cc52db426d11106a9721ecc7d9f09815";
        private static string _helpShiftDomainName = "snow.helpshift.com";
        private static string _helpShiftAppID = "snow_platform_20151105082053045-426c98dff916c1a";

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

        public static string HelpShiftApiKey
        {
            get
            {
                return _helpShiftApiKey;
            }

            set
            {
                _helpShiftApiKey = value;
            }
        }

        public static string HelpShiftAppId
        {
            get
            {
                return _helpShiftAppID;
            }

            set
            {
                _helpShiftAppID = value;
            }
        }

        public static string HelpShiftDomainName
        {
            get
            {
                return _helpShiftDomainName;
            }

            set
            {
                _helpShiftDomainName = value;
            }
        }
    }
}

