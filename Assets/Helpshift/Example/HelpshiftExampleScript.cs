using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.IO;


#if UNITY_IOS || UNITY_ANDROID
using Helpshift;
using HSMiniJSON;
#endif

public class HelpshiftExampleScript : MonoBehaviour {

#if UNITY_IOS || UNITY_ANDROID
	private HelpshiftSdk _support;
	public void updateMetaData (string nothing) {
		Debug.Log("Update metadata ************************************************************");
		Dictionary<string, object> configMap = new Dictionary<string, object>();
		configMap.Add("user-level", "21");
		configMap.Add ("hs-tags", new string [] {"Tag-1"});
		_support.updateMetaData(configMap);
	}

	public void helpshiftSessionBegan (string message) {
		Debug.Log("Session Began ************************************************************");
	}

	public void helpshiftSessionEnded (string message) {
		Debug.Log("Session ended ************************************************************");
	}

	public void alertToRateAppAction (string result) {
		Debug.Log("User action on alert :" + result);
	}

	public void didReceiveNotificationCount(string count) {
		Debug.Log("Notification async count : " + count);
	}

	public void didReceiveInAppNotificationCount(string count) {
		Debug.Log("In-app Notification count : " + count);
	}

	/// <summary>
	/// Conversation delegates
	/// </summary>

	public void newConversationStarted (string message) {
		// your code here
	}

	public void userRepliedToConversation (string newMessage) {
		// your code here
	}

	public void userCompletedCustomerSatisfactionSurvey (string json) {
		Dictionary<string, object> csatInfo = (Dictionary<string, object>)Json.Deserialize(json);
        Debug.Log("Customer satisfaction information : " + csatInfo);
	}

	public void displayAttachmentFile (string path) {
		Debug.Log("Recieved file " + path);
		StreamReader inp_stm = new StreamReader(path);
		
		while(!inp_stm.EndOfStream)
		{
			string inp_ln = inp_stm.ReadLine();
			Debug.Log("String : " + inp_ln);
		}
		
		inp_stm.Close( );  
	}
	// Use this for initialization
	void Start () {
		_support = HelpshiftSdk.getInstance();
		//_support.registerForPush("<gcm-key>");
#if UNITY_IOS
        _support.registerForLocalNotifications();
		_support.registerHelpshiftDeepLinking();
#endif
		_support.install();
        _support.login("identifier", "name", "email");
	}

	public void onShowFAQsClick() {
		Debug.Log("Show FAQs clicked !!");
		_support.showFAQs();
	}
		
	public void onShowConversationClick() {
		Debug.Log("Show Conversation clicked !!");
		_support.showConversation();
	}

	public void onShowFAQSectionClick () {
		GameObject inputFieldGo = GameObject.FindGameObjectWithTag("faq_section_id");
		InputField inputFieldCo = inputFieldGo.GetComponent<InputField>();
		try
		{
			Convert.ToInt16(inputFieldCo.text);
			_support.showFAQSection(inputFieldCo.text);
		}
		catch (FormatException e)
		{
			Debug.Log("Input string is not a sequence of digits : " + e);
		}
	}

	public void onShowFAQClick () {
		GameObject inputFieldGo = GameObject.FindGameObjectWithTag("faq_id");
		InputField inputFieldCo = inputFieldGo.GetComponent<InputField>();
		try
		{
			Convert.ToInt16(inputFieldCo.text);
			_support.showSingleFAQ(inputFieldCo.text);
		}
		catch (FormatException e)
		{
			Debug.Log("Input string is not a sequence of digits : " + e);
		}
	}

	public void onShowReviewReminderClick () {
#if UNITY_IOS
		_support.showAlertToRateAppWithURL("itms-apps://itunes.apple.com/app/id460171653");
#elif UNITY_ANDROID
		_support.showAlertToRateAppWithURL("market://details?id=com.RunnerGames.game.YooNinja_Lite");
#endif
	}
	#endif
}
