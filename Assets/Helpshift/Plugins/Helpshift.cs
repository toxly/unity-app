/*
 * Copyright 2015, Helpshift, Inc.
 * All rights reserved
 */
#if UNITY_IOS || UNITY_ANDROID
using UnityEngine;
using System;
using System.Collections.Generic;

namespace Helpshift
{
	/// <summary>
	/// The main class which exposes the Helpshift Sdk API for Unity scripts
	/// </summary>
	public class HelpshiftSdk {
		/// <summary>
		/// Various enums which are used in the Helpshift APIs
		/// </summary>

		// Response when user closes review alert
		public const String HS_RATE_ALERT_CLOSE = "HS_RATE_ALERT_CLOSE";
		// Response when user goes to conversation screen from review alert
		public const String HS_RATE_ALERT_FEEDBACK = "HS_RATE_ALERT_FEEDBACK";
		// Response when user goes to rate the app
		public const String HS_RATE_ALERT_SUCCESS = "HS_RATE_ALERT_SUCCESS";
		// Response when Helpshift is unable to show the review alert
		public const String HS_RATE_ALERT_FAIL = "HS_RATE_ALERT_FAIL";

		// Dictionary key to be used to supply tags with the meta-data
		public const String HSTAGSKEY = "hs-tags";
		// Dictionary key to be used to attach custom meta-data with config dictionaries
		public const String HSCUSTOMMETADATAKEY = "hs-custom-metadata";

		// Dictionary key to be used with withTagsMatching
		public const String HSTAGSMATCHINGKEY = "withTagsMatching";

		// Option value for enableContactUs which always shows the ContactUs button
		public const String CONTACT_US_ALWAYS = "always";
		// Option value for enableContactUs which never shows the ContactUs button
		public const String CONTACT_US_NEVER = "never";
		// Option value for enableContactUs which shows the ContactUs button only after searching through FAQs
		public const String CONTACT_US_AFTER_VIEWING_FAQS = "after_viewing_faqs";

		// Constants which can help detect special message types received in the
		// userRepliedToConversation message handler
		public const String HSUserAcceptedTheSolution = "User accepted the solution";
		public const String HSUserRejectedTheSolution = "User rejected the solution";
		public const String HSUserSentScreenShot = "User sent a screenshot";
		public const String HSUserReviewedTheApp = "User reviewed the app";


		private static HelpshiftSdk instance = null;
#if UNITY_IOS
		private static HelpshiftiOS nativeSdk = null;
#elif UNITY_ANDROID
		private static HelpshiftAndroid nativeSdk = null;
#endif
		private HelpshiftSdk() {
		}


		/// <summary>
		/// Main function which should be used to get the HelpshiftSdk instance.
		/// </summary>
		/// <returns>Singleton HelpshiftSdk instance</returns>
		public static HelpshiftSdk getInstance () {
			if(instance == null) {
				instance = new HelpshiftSdk();
#if UNITY_IOS
				nativeSdk = new HelpshiftiOS();
#elif UNITY_ANDROID
				nativeSdk = new HelpshiftAndroid();
#endif
			}
			return instance;
		}

		/// <summary>
		/// Install the HelpshiftSdk with specified apiKey, domainName, appId and config.
		/// When initializing Helpshift you must pass these three tokens. You initialize Helpshift by adding the API call
		/// ideally in the Start method of your game script.
		/// </summary>
		/// <param name="apiKey">This is your developer API Key</param>
		/// <param name="domainName">This is your domain name without any http:// or forward slashes</param>
		/// <param name="appId">This is the unique ID assigned to your app</param>
		/// <param name="config">This is the dictionary which contains additional configuration options for the HelpshiftSDK</param>
		public void install (string apiKey, string domainName, string appId, Dictionary<string, object> config) {
			nativeSdk.install(apiKey, domainName, appId, config);
		}

		/// <summary>
		/// Install the HelpshiftSdk with specified apiKey, domainName, appId and config.
		/// When initializing Helpshift you must pass these three tokens. You initialize Helpshift by adding the API call
		/// ideally in the Start method of your game script.
		/// </summary>
		/// <param name="apiKey">This is your developer API Key</param>
		/// <param name="domainName">This is your domain name without any http:// or forward slashes</param>
		/// <param name="appId">This is the unique ID assigned to your app</param>
		public void install (string apiKey, string domainName, string appId) {
			install(apiKey, domainName, appId, new Dictionary<string, object>());
		}

		/// <summary>
		/// Install the HelpshiftSdk with Api-Key, Domain-Name and App-Id specified through the GUI inspector.
		/// You initialize Helpshift by adding the following API call in the implementation file for your app delegate,
		/// ideally in the Start method of your game script.
		/// </summary>
		public void install() {
			nativeSdk.install ();
		}

		/// <summary>
		/// Gets the notification count of unread messages for the current conversation.
		/// </summary>
		/// <returns>The notification count.</returns>
		/// <param name="isAsync">If set to <c>true</c> is, result will be returned in the didReceiveNotificationCount
		/// message handler. If set to <c>false</c> return value will have the local unread count.</param>
		public int getNotificationCount (Boolean isAsync) {
			return nativeSdk.getNotificationCount(isAsync);
		}

		/// <summary>
		/// Sets the name and email for the current user
		/// </summary>
		/// <param name="userName">User name.</param>
		/// <param name="email">Email.</param>
		public void setNameAndEmail (string userName, string email) {
			nativeSdk.setNameAndEmail(userName, email);
		}

		/// <summary>
		/// Sets the user identifier for the current user
		/// </summary>
		/// <param name="identifier">Identifier.</param>
		public void setUserIdentifier (string identifier) {
			nativeSdk.setUserIdentifier(identifier);
		}

		/// <summary>
		/// Login the user with specified identifier, name and email.
		/// </summary>
		/// <param name="identifier">Unique user identifier.</param>
		/// <param name="name">Name.</param>
		/// <param name="email">Email.</param>
		public void login (string identifier, string name, string email) {
			nativeSdk.login(identifier, name, email);
		}

		/// <summary>
		/// Logout the current user
		/// </summary>
		public void logout () {
			nativeSdk.logout();
		}

		/// <summary>
		/// Registers the device token with Helpshift.
		/// </summary>
		/// <param name="deviceToken">Device token.</param>
		public void registerDeviceToken (string deviceToken) {
			nativeSdk.registerDeviceToken(deviceToken);
		}

		/// <summary>
		/// Add bread crumb.
		/// </summary>
		/// <param name="breadCrumb">Bread crumb.</param>
		public void leaveBreadCrumb (string breadCrumb) {
			nativeSdk.leaveBreadCrumb(breadCrumb);
		}

		/// <summary>
		/// Clears the bread crumbs.
		/// </summary>
		public void clearBreadCrumbs () {
			nativeSdk.clearBreadCrumbs();
		}

		/// <summary>
		/// Shows the helpshift conversation screen.
		/// </summary>
		/// <param name="configMap">the dictionary which will contain the arguments passed to the
		/// Helpshift session (that will start with this method call).
		/// Supported options are listed here : https://developers.helpshift.com/unity/sdk-configuration-ios/
		/// and https://developers.helpshift.com/unity/sdk-configuration-android/
		public void showConversation (Dictionary<string, object> configMap) {
			nativeSdk.showConversation(configMap);
		}

		/// <summary>
		/// Shows the helpshift conversation screen.
		/// If this API is used, the config options set from the GUI Inspector will be used
		/// </summary>
		public void showConversation () {
			nativeSdk.showConversation();
		}

		/// <summary>
		/// Shows the helpshift conversation screen.
		/// If this API is used, the config options set from the GUI Inspector will be used
		/// </summary>
		/// <param name="configMap">Meta-data dictionary which will be passed
		/// with conversations opened in this session</param>
		public void showConversationWithMeta (Dictionary<string, object> configMap) {
			nativeSdk.showConversationWithMeta(configMap);
		}

		/// <summary>
		/// Shows the helpshift screen with FAQs from a particular section
		/// </summary>
		/// <param name="sectionPublishId">Section publish identifier</param>
		/// <param name="configMap">the dictionary which will contain the arguments passed to the
		/// Helpshift session (that will start with this method call).
		/// Supported options are listed here : https://developers.helpshift.com/unity/sdk-configuration-ios/
		/// and https://developers.helpshift.com/unity/sdk-configuration-android/
		public void showFAQSection (string sectionPublishId, Dictionary<string, object> configMap) {
			nativeSdk.showFAQSection(sectionPublishId, configMap);
		}

		/// <summary>
		/// Shows the helpshift screen with FAQs from a particular section
		/// If this API is used, the config options set from the GUI Inspector will be used
		/// </summary>
		/// <param name="sectionPublishId">Section publish identifier.</param>
		public void showFAQSection (string sectionPublishId) {
			nativeSdk.showFAQSection(sectionPublishId);
		}

		/// <summary>
		/// Shows the helpshift screen with FAQs from a particular section
		/// If this API is used, the config options set from the GUI Inspector will be used
		/// </summary>
		/// <param name="sectionPublishId">Section publish identifier.</param>
		/// <param name="configMap">Meta-data dictionary which will be passed
		/// with conversations opened in this session</param>
		public void showFAQSectionWithMeta (string sectionPublishId, Dictionary<string, object> configMap) {
			nativeSdk.showFAQSectionWithMeta(sectionPublishId, configMap);
		}

		/// <summary>
		/// Shows the helpshift screen with an FAQ with specified identifier
		/// </summary>
		/// <param name="questionPublishId">Question publish identifier.</param>
		/// <param name="configMap">the dictionary which will contain the arguments passed to the
		/// Helpshift session (that will start with this method call).
		/// Supported options are listed here : https://developers.helpshift.com/unity/sdk-configuration-ios/
		/// and https://developers.helpshift.com/unity/sdk-configuration-android/
		public void showSingleFAQ (string questionPublishId, Dictionary<string, object> configMap) {
			nativeSdk.showSingleFAQ(questionPublishId, configMap);
		}

		/// <summary>
		/// Shows the helpshift screen with an FAQ with specified identifier.
		/// If this API is used, the config options set from the GUI Inspector will be used
		/// </summary>
		/// <param name="questionPublishId">Question publish identifier.</param>
		public void showSingleFAQ (string questionPublishId) {
			nativeSdk.showSingleFAQ(questionPublishId);
		}

		/// <summary>
		/// Shows the helpshift screen with an FAQ with specified identifier.
		/// If this API is used, the config options set from the GUI Inspector will be used
		/// </summary>
		/// <param name="questionPublishId">Question publish identifier.</param>
		/// <param name="configMap">Meta-data dictionary which will be passed
		/// with conversations opened in this session</param>
		public void showSingleFAQWithMeta (string questionPublishId, Dictionary<string, object> configMap) {
			nativeSdk.showSingleFAQWithMeta(questionPublishId, configMap);
		}


		/// <summary>
		/// Shows the helpshift screen with all the FAQ sections.
		/// </summary>
		/// <param name="configMap">the dictionary which will contain the arguments passed to the
		/// Helpshift session (that will start with this method call).
		/// Supported options are listed here : https://developers.helpshift.com/unity/sdk-configuration-ios/
		/// and https://developers.helpshift.com/unity/sdk-configuration-android/
		public void showFAQs (Dictionary<string, object> configMap) {
			nativeSdk.showFAQs(configMap);
		}

		/// <summary>
		/// Shows the helpshift screen with all the FAQ sections.
		/// If this API is used, the config options set from the GUI Inspector will be used
		/// </summary>
		public void showFAQs () {
			nativeSdk.showFAQs();
		}

		/// <summary>
		/// Shows the helpshift screen with all the FAQ sections.
		/// If this API is used, the config options set from the GUI Inspector will be used
		/// </summary>
		/// <param name="configMap">Meta-data dictionary which will be passed
		/// with conversations opened in this session</param>
		public void showFAQsWithMeta (Dictionary<string, object> configMap) {
			nativeSdk.showFAQsWithMeta(configMap);
		}

		/// <summary>
		/// Updates the meta data which will be passed on with any conversation started after this call
		/// </summary>
		/// <param name="metaData">Meta data dictionary.</param>
		public void updateMetaData(Dictionary<string, object> metaData) {
			nativeSdk.updateMetaData(metaData);
		}

		/// <summary>
		/// Opens the converation screen in response to the push notification
		/// received for the specified conversation id.
		/// </summary>
		/// <param name="issueId">Issue identifier.</param>
		public void handlePushNotification (string issueId) {
			nativeSdk.handlePushNotification(issueId);
		}

		/// <summary>
		/// Shows the alert to rate app with URL
		/// </summary>
		/// <param name="url">URL.</param>
		public void showAlertToRateAppWithURL (string url) {
			nativeSdk.showAlertToRateAppWithURL(url);
		}

		public void setSDKLanguage(string locale) {
			nativeSdk.setSDKLanguage(locale);
		}
#if UNITY_IOS
		public void handleLocalNotification (string issueId) {
			nativeSdk.handleLocalNotification(issueId);
		}

		public void pauseDisplayOfInAppNotification (bool pauseInApp) {
			nativeSdk.pauseDisplayOfInAppNotification(pauseInApp);
		}

		/// <summary>
		/// Registers for native push notification handling in the Helpshift SDK
		/// </summary>
		/// <param name="nothing">ignored</param>
		public void registerForPush (string nothing) {
			nativeSdk.registerForRemoteNotifications();
		}

		public void registerHelpshiftDeepLinking () {
			nativeSdk.registerHelpshiftDeepLinking();
		}

		public void registerForLocalNotifications () {
			nativeSdk.registerForLocalNotifications();
		}

#endif

#if UNITY_ANDROID
		public void registerSessionDelegates() {
			nativeSdk.registerSessionDelegates();
		}

		/// <summary>
		/// Registers for native push notification handling in the Helpshift SDK
		/// </summary>
		/// <param name="gcmId">Gcm identifier.</param>
		public void registerForPush(string gcmId) {
			nativeSdk.registerForPushWithGcmId(gcmId);
		}
#endif
	}

	/// <summary>
	/// Helpshift logger class.
	/// Logs printed with this API will be passed along with any conversation started
	/// here after.
	/// </summary>
	public class HelpshiftLog {
		public static int v (String tag, String log) {
#if UNITY_IOS
			return HelpshiftiOSLog.v(tag, log);
#elif UNITY_ANDROID
			return HelpshiftAndroidLog.v(tag, log);
#endif
		}

		public static int d (String tag, String log) {
#if UNITY_IOS
			return HelpshiftiOSLog.d(tag, log);
#elif UNITY_ANDROID
			return HelpshiftAndroidLog.d(tag, log);
#endif
		}

		public static int i (String tag, String log) {
#if UNITY_IOS
			return HelpshiftiOSLog.i(tag, log);
#elif UNITY_ANDROID
			return HelpshiftAndroidLog.i(tag, log);
#endif
		}

		public static int w (String tag, String log) {
#if UNITY_IOS
			return HelpshiftiOSLog.w(tag, log);
#elif UNITY_ANDROID
			return HelpshiftAndroidLog.w(tag, log);
#endif
		}

		public static int e (String tag, String log) {
#if UNITY_IOS
			return HelpshiftiOSLog.e(tag, log);
#elif UNITY_ANDROID
			return HelpshiftAndroidLog.e(tag, log);
#endif
		}

	}
}
#endif
