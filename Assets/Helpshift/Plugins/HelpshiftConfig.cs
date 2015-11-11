using UnityEngine;
using System.IO;
#if UNITY_IOS || UNITY_ANDROID
using Helpshift;
#endif
using System.Collections.Generic;
using HSMiniJSON;

#if UNITY_EDITOR
using UnityEditor;

[InitializeOnLoad]
#endif
[System.Serializable]
public class HelpshiftConfig : ScriptableObject
{
	private static HelpshiftConfig instance;
	private const string helpshiftConfigAssetName = "HelpshiftConfig";
	private const string helpshiftConfigPath = "Helpshift/Resources";

	[SerializeField]
	private string apiKey;
	[SerializeField]
	private string domainName;
	[SerializeField]
	private string iosAppId;
	[SerializeField]
	private string androidAppId;
	[SerializeField]
	private int contactUsOption;
	[SerializeField]
	private bool gotoConversation;
	[SerializeField]
	private bool presentFullScreen;
	[SerializeField]
	private bool enableDialogUIForTablets;
	[SerializeField]
	private bool enableInApp;
	[SerializeField]
	private bool requireEmail;
	[SerializeField]
	private bool hideNameAndEmail;
	[SerializeField]
	private bool enablePrivacy;
	[SerializeField]
	private bool showSearchOnNewConversation;
	[SerializeField]
	private bool showConversationResolutionQuestion;
	[SerializeField]
	private bool enableDefaultFallbackLanguage;
	[SerializeField]
	private string conversationPrefillText;
	[SerializeField]
#if UNITY_IOS || UNITY_ANDROID
	private string[] contactUsOptions = {Helpshift.HelpshiftSdk.CONTACT_US_ALWAYS,
		Helpshift.HelpshiftSdk.CONTACT_US_NEVER,
		Helpshift.HelpshiftSdk.CONTACT_US_AFTER_VIEWING_FAQS};
#else
	private string[] contactUsOptions = {"always", "never", "after_viewing_faqs"};
#endif
	[SerializeField]
	private string unityGameObject;
	[SerializeField]
	private string notificationIcon;
	[SerializeField]
	private string notificationSound;

	public static HelpshiftConfig Instance
	{
		get
		{
			instance = Resources.Load(helpshiftConfigAssetName) as HelpshiftConfig;
			if (instance == null) {
				instance = CreateInstance<HelpshiftConfig>();
#if UNITY_EDITOR
				string properPath = Path.Combine(Application.dataPath, helpshiftConfigPath);
				if (!Directory.Exists(properPath))
				{
					AssetDatabase.CreateFolder("Assets/Helpshift", "Resources");
				}

				string fullPath = Path.Combine(Path.Combine("Assets", helpshiftConfigPath),
				                               helpshiftConfigAssetName + ".asset"
				                               );
				AssetDatabase.CreateAsset(instance, fullPath);
#endif
			}
			return instance;
		}
	}

	#if UNITY_EDITOR
	[MenuItem("Helpshift/Edit Config")]
	public static void Edit()
	{
		Selection.activeObject = Instance;
	}

	[MenuItem("Helpshift/Developers Page")]
	public static void OpenAppPage()
	{
		string url = "https://developers.helpshift.com/unity/";
		Application.OpenURL(url);
	}

	[MenuItem("Helpshift/SDK Twitter handler")]
	public static void OpenFacebookGroup()
	{
		string url = "https://twitter.com/helpshiftsdk";
		Application.OpenURL(url);
	}

	[MenuItem("Helpshift/Report an SDK Bug")]
	public static void ReportABug()
	{
		string url = "mailto:support@helpshift.com";
		Application.OpenURL(url);
	}
	#endif

	public bool GotoConversation
	{
		get { return gotoConversation; }
		set
		{
			if (gotoConversation != value)
			{
				gotoConversation = value;
				DirtyEditor();
			}
		}
	}

	public int ContactUs
	{
		get { return contactUsOption; }
		set
		{
			if (contactUsOption != value)
			{
				contactUsOption = value;
				DirtyEditor();
			}
		}
	}

	public bool PresentFullScreenOniPad
	{
		get { return presentFullScreen; }
		set
		{
			if (presentFullScreen != value)
			{
				presentFullScreen = value;
				DirtyEditor();
			}
		}
	}

	public bool EnableInAppNotification
	{
		get { return enableInApp; }
		set
		{
			if (enableInApp != value)
			{
				enableInApp = value;
				DirtyEditor();
			}
		}
	}

	public bool EnableDialogUIForTablets
	{
		get { return enableDialogUIForTablets; }
		set
		{
			if (enableDialogUIForTablets != value)
			{
				enableDialogUIForTablets = value;
				DirtyEditor();
			}
		}
	}

	public bool RequireEmail
	{
		get { return requireEmail; }
		set
		{
			if (requireEmail != value)
			{
				requireEmail = value;
				DirtyEditor();
			}
		}
	}

	public bool HideNameAndEmail
	{
		get { return hideNameAndEmail; }
		set
		{
			if (hideNameAndEmail != value)
			{
				hideNameAndEmail = value;
				DirtyEditor();
			}
		}
	}

	public bool EnablePrivacy
	{
		get { return enablePrivacy; }
		set
		{
			if (enablePrivacy != value)
			{
				enablePrivacy = value;
				DirtyEditor();
			}
		}
	}

	public bool ShowSearchOnNewConversation
	{
		get { return showSearchOnNewConversation; }
		set
		{
			if (showSearchOnNewConversation != value)
			{
				showSearchOnNewConversation = value;
				DirtyEditor();
			}
		}
	}

	public bool ShowConversationResolutionQuestion
	{
		get { return showConversationResolutionQuestion; }
		set
		{
			if (showConversationResolutionQuestion != value)
			{
				showConversationResolutionQuestion = value;
				DirtyEditor();
			}
		}
	}

	public bool EnableDefaultFallbackLanguage
	{
		get { return enableDefaultFallbackLanguage; }
		set
		{
			if (enableDefaultFallbackLanguage != value)
			{
				enableDefaultFallbackLanguage = value;
				DirtyEditor();
			}
		}
	}

	public string ConversationPrefillText
	{
		get { return conversationPrefillText; }
		set
		{
			if (conversationPrefillText != value)
			{
				conversationPrefillText = value;
				DirtyEditor();
			}
		}
	}

	public string ApiKey
	{
		get { return apiKey; }
		set
		{
			if (apiKey != value)
			{
				apiKey = value;
				DirtyEditor();
			}
		}
	}

	public string DomainName
	{
		get { return domainName; }
		set
		{
			if (domainName != value)
			{
				domainName = value;
				DirtyEditor();
			}
		}
	}

	public string AndroidAppId
	{
		get { return androidAppId; }
		set
		{
			if (androidAppId != value)
			{
				androidAppId = value;
				DirtyEditor();
			}
		}
	}

	public string iOSAppId
	{
		get { return iosAppId; }
		set
        {
            if (iosAppId != value)
            {
                iosAppId = value;
                DirtyEditor();
            }
        }
	}

	public string UnityGameObject
	{
		get { return unityGameObject; }
		set
		{
			if (unityGameObject != value)
			{
				unityGameObject = value;
				DirtyEditor();
			}
		}
	}

	public string NotificationIcon
	{
		get { return notificationIcon; }
		set
        {
            if (notificationIcon != value)
            {
                notificationIcon = value;
                DirtyEditor();
            }
        }
	}

	public string NotificationSound
	{
		get { return notificationSound; }
		set
		{
			if (notificationSound != value)
			{
				notificationSound = value;
				DirtyEditor();
			}
		}
	}

	public Dictionary<string, string> InstallConfig
	{
		get { return instance.getInstallConfig(); }
	}

	public Dictionary<string, object> ApiConfig
	{
		get { return instance.getApiConfig(); }
	}

	private static void DirtyEditor()
	{
#if UNITY_EDITOR
		EditorUtility.SetDirty(Instance);
		instance.SaveConfig();
#endif
	}

	public void SaveConfig () {
#if UNITY_EDITOR
		AssetDatabase.SaveAssets();
		string apiJson = Json.Serialize(instance.ApiConfig);
		string installJson = Json.Serialize(instance.InstallConfig);

        string androidSdkPath = Path.Combine(Application.dataPath, "Plugins/Android/helpshift");
        if (Directory.Exists(androidSdkPath)) {
            string androidPath = Path.Combine(Application.dataPath, "Plugins/Android/helpshift/res/raw/");
            if (!Directory.Exists(androidPath)) {
                AssetDatabase.CreateFolder("Assets/Plugins/Android/helpshift/res", "raw");
                androidPath = Path.Combine(Application.dataPath, "Plugins/Android/helpshift/res/raw/");
            }
			System.IO.File.WriteAllText (androidPath + "helpshiftapiconfig.json", apiJson);
			System.IO.File.WriteAllText (androidPath + "helpshiftinstallconfig.json", installJson);
        }

		string iosPath = Path.Combine(Application.dataPath, "Helpshift/Plugins/iOS/");
        if (Directory.Exists(iosPath)) {
            System.IO.File.WriteAllText (iosPath + "HelpshiftApiConfig.json", apiJson);
            System.IO.File.WriteAllText (iosPath + "HelpshiftInstallConfig.json", installJson);
        }
#endif
	}

	public Dictionary<string, object> getApiConfig () {
		Dictionary<string, object> configDictionary = new Dictionary<string, object>();

		string enableContactUsString = instance.contactUsOptions[instance.contactUsOption];
		configDictionary.Add("enableContactUs", enableContactUsString);

		configDictionary.Add("gotoConversationAfterContactUs", instance.gotoConversation == true ? "yes" : "no");
		configDictionary.Add("presentFullScreenOniPad", instance.presentFullScreen == true ? "yes" : "no");
		configDictionary.Add("requireEmail", instance.requireEmail == true ? "yes" : "no");
		configDictionary.Add("hideNameAndEmail", instance.hideNameAndEmail == true ? "yes" : "no");
		configDictionary.Add("enableFullPrivacy", instance.enablePrivacy == true ? "yes" : "no");
		configDictionary.Add("showSearchOnNewConversation", instance.showSearchOnNewConversation == true ? "yes" : "no");
		configDictionary.Add("showConversationResolutionQuestion", instance.showConversationResolutionQuestion == true ? "yes" : "no");

		configDictionary.Add("conversationPrefillText", instance.conversationPrefillText);
		return configDictionary;
	}

	public Dictionary<string, string> getInstallConfig () {
		Dictionary<string, string> installDictionary = new Dictionary<string, string>();

		installDictionary.Add("unityGameObject", instance.unityGameObject);
        installDictionary.Add("notificationIcon", instance.notificationIcon);
		installDictionary.Add("notificationSound", instance.notificationSound);
		installDictionary.Add("enableDialogUIForTablets", instance.enableDialogUIForTablets == true ? "yes" : "no");
		installDictionary.Add("enableInAppNotification", instance.enableInApp == true ? "yes" : "no");
		installDictionary.Add("enableDefaultFallbackLanguage", instance.enableDefaultFallbackLanguage == true ? "yes" : "no");
		installDictionary.Add("__hs__apiKey", instance.ApiKey);
		installDictionary.Add("__hs__domainName", instance.DomainName);
#if UNITY_ANDROID
		installDictionary.Add("__hs__appId", instance.AndroidAppId);
#elif UNITY_IOS
        installDictionary.Add("__hs__appId", instance.iOSAppId);
#endif

		return installDictionary;
	}
}
