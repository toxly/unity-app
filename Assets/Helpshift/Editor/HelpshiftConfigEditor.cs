using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(HelpshiftConfig))]
public class HelpshiftConfigEditor : Editor {

	bool showHelpshiftConfig = false;

	GUIContent apiKeyLabel = new GUIContent("Api Key [?]:", "Helpshift App Ids can be found at https://example.helpshift.com/admin/settings/download-sdk/");
	GUIContent domainNameLabel = new GUIContent("Domain Name [?]:", "Helpshift's domain name\n For example, 'example.helpshift.com'");
	GUIContent androidAppIdLabel = new GUIContent("Android App Id [?]:", "Helpshift App Ids can be found at https://example.helpshift.com/admin/settings/download-sdk/");
	GUIContent iOSAppIdLabel = new GUIContent("iOS App Id [?]:", "Helpshift App Ids can be found at https://example.helpshift.com/admin/settings/download-sdk/");
	GUIContent enableInAppLabel = new GUIContent(" Enable In-app Notifications [?]:", "The enableInAppNotification flag controls whether in app notifications will be shown for Helpshift conversation updates");

	GUIContent enableContactUsLabel = new GUIContent(" Contact Us [?]:", "The enableContactUs flag controls the visibility of Contact Us button");
	GUIContent gotoConvLabel = new GUIContent(" Go to Conversation [?]", "Determines which screen a user sees after filing an issue via 'Contact Us'.");
	GUIContent presentFullScreenLabel = new GUIContent(" Present in Fullscreen on iPad [?]", "The presentFullScreenOniPad flag will determine whether to present support views in UIModalPresentationFullScreen or UIModalPresentationFormSheet modal presentation style in iPad. Only takes effect on iPad.");
	GUIContent enableDialogUILabel = new GUIContent(" Enable dialog UI for Tablets [?]", "This flag will determine whether we will show a dialog style UI for Tablets. Only applicable for Android");
	GUIContent requireEmailLabel = new GUIContent(" Require Email [?]", "The requireEmail option determines whether email is required or optional for starting a new conversation.");
	GUIContent hideNameAndEmailLabel = new GUIContent(" Hide Name and Email [?]", "The hideNameAndEmail flag will hide the name and email fields when the user starts a new conversation.");
	GUIContent enablePrivacyLabel = new GUIContent(" Enable Privacy [?]", "In scenarios where the user attaches objectionable content in the screenshots, it becomes a huge COPPA concern. The enableFullPrivacy option will help solve this problem.");
	GUIContent showSearchLabel = new GUIContent(" Show Search results after New Conversation [?]", "Use this option to provide better ticket deflection.");
	GUIContent showConversationResolutionLabel = new GUIContent(" Show Conversation Resolution Question [?]", "Use this option to show conversation resolution question to the user.");
	GUIContent enableDefaultFallbackLabel = new GUIContent(" Enable fallback to default language. [?]", "Use this option to enable fallback to default language that is English for FAQs.");

	GUIContent conversationPrefillLabel = new GUIContent(" Conversation Prefill Text [?]", "The conversationPrefillText API option prefills a new conversation with the supplied string. You can use this option to add crash logs to a new conversation and prompt the user to send those logs as a support ticket. You can also use this option to set context depending on where and when in the app showConversation is being launched from.");

	public override void OnInspectorGUI () {

		HelpshiftConfig helpshiftConfig = HelpshiftConfig.Instance;

		EditorGUILayout.LabelField("Install Time Configurations");
		EditorGUILayout.HelpBox("1) Add the Unity game object which will respond to Helpshift callbacks", MessageType.None);
        helpshiftConfig.UnityGameObject = EditorGUILayout.TextField(helpshiftConfig.UnityGameObject);
        EditorGUILayout.HelpBox("2) Filename of the notification icon which you want to display in the notification center. (Android only)", MessageType.None);
        helpshiftConfig.NotificationIcon = EditorGUILayout.TextField(helpshiftConfig.NotificationIcon);
		EditorGUILayout.HelpBox("3) Filename of the notification sound file which you want to use for Helpshift notifications. (Android only)", MessageType.None);
		helpshiftConfig.NotificationSound = EditorGUILayout.TextField(helpshiftConfig.NotificationSound);

		EditorGUILayout.Space();

		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField(apiKeyLabel);
		helpshiftConfig.ApiKey = EditorGUILayout.TextField(helpshiftConfig.ApiKey);
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField(domainNameLabel);
		helpshiftConfig.DomainName = EditorGUILayout.TextField(helpshiftConfig.DomainName);
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField(androidAppIdLabel);
		helpshiftConfig.AndroidAppId = EditorGUILayout.TextField(helpshiftConfig.AndroidAppId);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField(iOSAppIdLabel);
        helpshiftConfig.iOSAppId = EditorGUILayout.TextField(helpshiftConfig.iOSAppId);
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();
		helpshiftConfig.EnableInAppNotification = EditorGUILayout.ToggleLeft(enableInAppLabel, helpshiftConfig.EnableInAppNotification);
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        helpshiftConfig.EnableDialogUIForTablets = EditorGUILayout.ToggleLeft(enableDialogUILabel, helpshiftConfig.EnableDialogUIForTablets);
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.Space();

		EditorGUILayout.BeginHorizontal();
		helpshiftConfig.EnableDefaultFallbackLanguage = EditorGUILayout.ToggleLeft(enableDefaultFallbackLabel, helpshiftConfig.EnableDefaultFallbackLanguage);
		EditorGUILayout.EndHorizontal();
		
		EditorGUILayout.Space();

		EditorGUILayout.LabelField("SDK Configurations");

		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField(enableContactUsLabel);
		EditorGUILayout.EndHorizontal();
		helpshiftConfig.ContactUs = EditorGUILayout.Popup(helpshiftConfig.ContactUs,
		                             new string[]{"Always", "Never", "After viewing FAQs"});

		EditorGUILayout.Space();

		showHelpshiftConfig = EditorGUILayout.Foldout(showHelpshiftConfig, "SDK Configuration flags");
		if (showHelpshiftConfig)
		{
			helpshiftConfig.GotoConversation = EditorGUILayout.ToggleLeft(gotoConvLabel, helpshiftConfig.GotoConversation);
			helpshiftConfig.PresentFullScreenOniPad = EditorGUILayout.ToggleLeft(presentFullScreenLabel, helpshiftConfig.PresentFullScreenOniPad);
			helpshiftConfig.RequireEmail = EditorGUILayout.ToggleLeft(requireEmailLabel, helpshiftConfig.RequireEmail);
			helpshiftConfig.HideNameAndEmail = EditorGUILayout.ToggleLeft(hideNameAndEmailLabel, helpshiftConfig.HideNameAndEmail);
			helpshiftConfig.EnablePrivacy = EditorGUILayout.ToggleLeft(enablePrivacyLabel, helpshiftConfig.EnablePrivacy);
			helpshiftConfig.ShowSearchOnNewConversation = EditorGUILayout.ToggleLeft(showSearchLabel, helpshiftConfig.ShowSearchOnNewConversation);
			helpshiftConfig.ShowConversationResolutionQuestion = EditorGUILayout.ToggleLeft(showConversationResolutionLabel, helpshiftConfig.ShowConversationResolutionQuestion);
		}

		EditorGUILayout.Space();

		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField(conversationPrefillLabel);
		helpshiftConfig.ConversationPrefillText = EditorGUILayout.TextField(helpshiftConfig.ConversationPrefillText);
		EditorGUILayout.EndHorizontal();


	}
}
