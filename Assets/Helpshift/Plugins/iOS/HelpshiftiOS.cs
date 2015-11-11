/*
 * Copyright 2015, Helpshift, Inc.
 * All rights reserved
 */

#if UNITY_IPHONE
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using HSMiniJSON;

namespace Helpshift
{
	public class HelpshiftiOS
	{
		[DllImport ("__Internal")]
		private static extern void hsInstall ();
		[DllImport ("__Internal")]
		private static extern void hsInstallForApiKey (string apiKey, string domainName, string appId);
		[DllImport ("__Internal")]
		private static extern void hsInstallForApiKeyWithOptions (string apiKey, string domainName, string appId, string jsonOptionsDict);

        [DllImport ("__Internal")]
		private static extern void hsShowFAQs();
		[DllImport ("__Internal")]
		private static extern void hsShowFAQsWithOptions(string jsonOptionsDict);
		[DllImport ("__Internal")]
		private static extern void hsShowFAQsWithMeta(string jsonOptionsDict);

		[DllImport ("__Internal")]
		private static extern void hsShowFAQSection (string id);
		[DllImport ("__Internal")]
		private static extern void hsShowFAQSectionWithOptions (string id, string jsonOptionsDict);
		[DllImport ("__Internal")]
		private static extern void hsShowFAQSectionWithMeta (string id, string jsonOptionsDict);

		[DllImport ("__Internal")]
		private static extern void hsShowConversation ();
		[DllImport ("__Internal")]
		private static extern void hsShowConversationWithOptions (string jsonOptionsDict);
		[DllImport ("__Internal")]
		private static extern void hsShowConversationWithMeta (string jsonOptionsDict);

		[DllImport ("__Internal")]
		private static extern void hsShowSingleFAQ (string id);
		[DllImport ("__Internal")]
		private static extern void hsShowSingleFAQWithOptions (string id, string jsonOptionsDict);
		[DllImport ("__Internal")]
		private static extern void hsShowSingleFAQWithMeta (string id, string jsonOptionsDict);

		[DllImport ("__Internal")]
		private static extern void hsSetUserIdentifier(string userIdentifier);
		[DllImport ("__Internal")]
		private static extern void hsSetNameAndEmail(string userName, string email);
		[DllImport ("__Internal")]
		private static extern void hsRegisterDeviceToken(string deviceToken);
		[DllImport ("__Internal")]
		private static extern int  hsGetNotificationCountFromRemote (bool isRemote);
		[DllImport ("__Internal")]
		private static extern void hsLeaveBreadCrumb (string breadCrumb);
		[DllImport ("__Internal")]
		private static extern void hsClearBreadCrumbs ();
		[DllImport ("__Internal")]
		private static extern void hsHandleLocalNotificationForIssue (string issueID);
		[DllImport ("__Internal")]
		private static extern void hsHandleRemoteNotificationForIssue (string issueID);
		[DllImport ("__Internal")]
		private static extern void hsSetMetaData (string meta);
		[DllImport ("__Internal")]
		private static extern void hsPauseDisplayOfInAppNotification (bool pauseInApp);
		[DllImport ("__Internal")]
		private static extern void hsShowAlertToRateAppWithURL (string url);
		[DllImport ("__Internal")]
		private static extern void hsRegisterForRemoteNotifications ();
		[DllImport ("__Internal")]
		private static extern void hsRegisterForLocalNotifications ();
		[DllImport ("__Internal")]
		private static extern void hsLogin (string identifier, string name, string email);
		[DllImport ("__Internal")]
		private static extern void hsLogout ();
		[DllImport ("__Internal")]
		private static extern void hsRegisterHelpshiftDeepLinking ();
		[DllImport ("__Internal")]
		private static extern void hsSetSDKLanguage (string locale);


		public HelpshiftiOS () {
		}

		public void install () {
			hsInstall();
		}
		public void install (string apiKey, string domain, string appId, Dictionary<string, object> configMap) {
			if(configMap == null) {
				hsInstallForApiKey (apiKey, domain, appId);
			} else {
				hsInstallForApiKeyWithOptions (apiKey, domain, appId, Json.Serialize(configMap));
			}
		}
		public void install(string apiKey, string domain, string appId) {
			hsInstallForApiKey (apiKey, domain, appId);
		}

		public int getNotificationCount (bool isRemote) {
			return hsGetNotificationCountFromRemote(isRemote);
		}

		public void setNameAndEmail (string userName, string email) {
			hsSetNameAndEmail(userName, email);
		}

		public void setUserIdentifier (string identifier) {
			hsSetUserIdentifier(identifier);
		}

        public void login(string identifier, string name, string email) {
            hsLogin(identifier, name, email);
        }

		public void logout() {
			hsLogout();
		}

		public void registerDeviceToken (string deviceToken) {
			hsRegisterDeviceToken(deviceToken);
		}

		public void leaveBreadCrumb (string breadCrumb) {
			hsLeaveBreadCrumb(breadCrumb);
		}

		public void clearBreadCrumbs () {
			hsClearBreadCrumbs();
		}

		public void showConversation (Dictionary<string, object> configMap) {
			hsShowConversationWithOptions(Json.Serialize(configMap));
		}
		public void showConversation () {
			hsShowConversation();
		}
		public void showConversationWithMeta (Dictionary<string, object> configMap) {
			hsShowConversationWithMeta(Json.Serialize(configMap));
		}

		public void showFAQs () {
			hsShowFAQs();
		}
		public void showFAQs (Dictionary<string, object> configMap) {
			hsShowFAQsWithOptions(Json.Serialize(configMap));
		}
		public void showFAQsWithMeta (Dictionary<string, object> configMap) {
			hsShowFAQsWithMeta(Json.Serialize(configMap));
		}

		public void showFAQSection (string sectionPublishId) {
			hsShowFAQSection(sectionPublishId);
		}
		public void showFAQSection (string sectionPublishId, Dictionary<string, object> configMap) {
			hsShowFAQSectionWithOptions(sectionPublishId, Json.Serialize(configMap));
		}
		public void showFAQSectionWithMeta (string sectionPublishId, Dictionary<string, object> configMap) {
			hsShowFAQSectionWithMeta(sectionPublishId, Json.Serialize(configMap));
		}

		public void showSingleFAQ (string questionPublishId) {
			hsShowSingleFAQ(questionPublishId);
		}
		public void showSingleFAQ (string questionPublishId, Dictionary<string, object> configMap) {
			hsShowSingleFAQWithOptions(questionPublishId, Json.Serialize(configMap));
		}
		public void showSingleFAQWithMeta (string questionPublishId, Dictionary<string, object> configMap) {
			hsShowSingleFAQWithMeta(questionPublishId, Json.Serialize(configMap));
		}

		public void updateMetaData(Dictionary<string, object> metaData) {
			hsSetMetaData(Json.Serialize(metaData));
		}

		public void handlePushNotification (string issueId) {
			hsHandleRemoteNotificationForIssue(issueId);
		}

		public void handleLocalNotification (string issueId) {
			hsHandleLocalNotificationForIssue(issueId);
		}

		public void pauseDisplayOfInAppNotification (bool pauseInApp) {
			hsPauseDisplayOfInAppNotification(pauseInApp);
		}

		public void showAlertToRateAppWithURL (string url) {
			hsShowAlertToRateAppWithURL(url);
		}

		public void registerForRemoteNotifications () {
			hsRegisterForRemoteNotifications();
		}

		public void registerForLocalNotifications () {
			hsRegisterForLocalNotifications();
		}

		public void registerHelpshiftDeepLinking () {
			hsRegisterHelpshiftDeepLinking();
		}

		public void setSDKLanguage(string locale) {
			hsSetSDKLanguage(locale);
		}
	}

	public class HelpshiftiOSLog {

		private HelpshiftiOSLog () {
		}

		public static int v (String tag, String log) {
			Debug.Log("Verbose::" + tag + "::" + log);
			return 0;
		}

		public static int d (String tag, String log) {
			Debug.Log("Debug::" + tag + "::" + log);
			return 0;
		}

		public static int i (String tag, String log) {
			Debug.Log("Info::" + tag + "::" + log);
			return 0;
		}

		public static int w (String tag, String log) {
			Debug.Log("Warn::" + tag + "::" + log);
			return 0;
		}

		public static int e (String tag, String log) {
			Debug.Log("Error::" + tag + "::" + log);
			return 0;
		}

	}

}
#endif
