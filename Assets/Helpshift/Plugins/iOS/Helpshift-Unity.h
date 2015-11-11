#include "Helpshift.h"
extern "C" UIViewController *UnityGetGLViewController();
extern void UnitySendMessage(const char *, const char *, const char *);

/* Initialize helpshift support
 *
 *  When initializing Helpshift you must pass these three tokens. You initialize Helpshift by adding the following lines in the implementation file for your app right after app startup
 */

extern "C" void hsInstallForApiKey(const char *apiKey, const char *domainName, const char *appId);
extern "C" void hsInstall();

/* Initialize helpshift support
 *  When initializing Helpshift you must pass these three tokens along with a json encoded dictionary of additional configuration options.
 *  Currently we support the "enableInAppNotification" as the only available option. Possible values are <"YES"/"NO">. If you set the flag to "YES", the
 *  helpshift SDK will show notifications similar to the banner notifications supported by Apple Push notifications. These notifications will alert the
 *  user of any updates to reported issues. If you set the flag to "NO", the in-app notifications will be disabled.
 */

extern "C" void hsInstallForApiKeyWithOptions(const char *apiKey, const char *domainName, const char *appId, const char *optionsDictionaryString);

/** Show the helpshift conversation screen (with Optional Arguments)
 *
 * To show the Helpshift conversation screen with optional arguments you will need to pass the name of the viewcontroller on which the conversation screen will show up and an options dictionary. If you do not want to pass any options then just pass nil which will take on the default options.
 *
 * @param viewController viewController on which the helpshift report issue screen will show up.
 * @param optionsDictionary the dictionary which will contain the arguments passed to the Helpshift conversation session (that will start with this method call).
 *
 * Please check the docs for available options.
 *
 * @available Available in SDK version 4.0.0 or later
 */

extern "C" void hsShowConversationWithOptions(const char *optionsDictionaryString);
extern "C" void hsShowConversationWithMeta(const char *optionsDictionaryString);
extern "C" void hsShowConversation();

/** Show the helpshift screen with faqs from a particular section
 *
 * To show the Helpshift screen for showing a particular faq section you need to pass the publish-id of the faq section and the name of the viewcontroller on which the faq section screen will show up. For example from inside a viewcontroller you can call the Helpshift faq section screen by passing the argument “self” for the viewController parameter. If you do not want to pass any options then just pass nil which will take on the default options.
 *
 * @param faqSectionPublishID the publish id associated with the faq section which is shown in the FAQ page on the admin side (__yourcompanyname__.helpshift.com/admin/faq/).
 * @param viewController viewController on which the helpshift faq section screen will show up.
 * @param optionsDictionary the dictionary which will contain the arguments passed to the Helpshift session (that will start with this method call).
 *
 * @available Available in SDK version 2.0.0 or later
 */

extern "C" void hsShowFAQSectionWithOptions(const char *faqSectionPublishID, const char *optionsDictionaryString);
extern "C" void hsShowFAQSectionWithMeta(const char *faqSectionPublishID, const char *optionsDictionaryString);
extern "C" void hsShowFAQSection(const char *sectionPublishID);

/** Show the helpshift screen with a single faq
 *
 * To show the Helpshift screen for showing a single faq you need to pass the publish-id of the faq and the name of the viewcontroller on which the faq screen will show up. For example from inside a viewcontroller you can call the Helpshift faq section screen by passing the argument “self” for the viewController parameter. If you do not want to pass any options then just pass nil which will take on the default options.
 *
 * @param faqPublishID the publish id associated with the faq which is shown when you expand a single FAQ (__yourcompanyname__.helpshift.com/admin/faq/)
 * @param viewController viewController on which the helpshift faq section screen will show up.
 * @param optionsDictionary the dictionary which will contain the arguments passed to the Helpshift session (that will start with this method call).
 *
 * @available Available in SDK version 4.0.0 or later
 */

extern "C" void hsShowSingleFAQWithOptions(const char *faqPublishID, const char *optionsDictionaryString);
extern "C" void hsShowSingleFAQWithMeta(const char *faqPublishID, const char *optionsDictionaryString);
extern "C" void hsShowSingleFAQ(const char *faqPublishID);

/** Show the support screen with only the faqs (with Optional Arguments)
 *
 * To show the Helpshift screen with only the faq sections with search with optional arguments, you can use this api. This screen will not show the issues reported by the user. If you do not want to pass any options then just pass nil which will take on the default options.
 *
 * @param viewController viewController on which the helpshift faqs screen will show up.
 * @param optionsDictionary the dictionary which will contain the arguments passed to the Helpshift faqs screen session (that will start with this method call).
 *
 * Please check the docs for available options.
 *
 * @available Available in SDK version 2.0.0 or later
 */
extern "C" void hsShowFAQsWithOptions(const char *optionsDictionaryString);
extern "C" void hsShowFAQsWithMeta(const char *optionsDictionaryString);
extern "C" void hsShowFAQs();

/*
 * Set the unique udentifier for your users.
 */
extern "C" void hsSetUserIdentifier(const char *userIdentifier);

/*
 * Set the user-name for your users
 */
extern "C" void hsSetNameAndEmail(const char *userName, const char *email);

/*
 * Add extra debug information regarding user-actions.
 * You can add additional debugging statements to your code, and see exactly what the user was doing right before they reported the issue.
 */
extern "C" void hsLeaveBreadCrumb(const char *breadCrumb);

/*
 * Clears Breadcrumbs list.
 *
 * Breadcrumbs list stores upto 100 latest actions. You'll receive those in every Issue.
 * If for some reason you want to clear previous messages, you can use this method.
 */
extern "C" void hsClearBreadCrumbs(void);

/*
 * Get the notification count for replies to reported issues.
 *
 * If you want to show your user notifications for replies on the issues posted, you can get the notification count asynchronously by implementing the
 * didReceiveNotificationCount on the "Helpshift" game object.
 * If you want to get the notification counts synchronously, just set the value of the argument to false. You will get the notification count as the return value from the function.
 * If on the other hand you want the count to be returned asynchronously, set the argument to true and implement the didReceiveNotificationCount function on a "Helpshift" game object somewhere in your unity game application.
 *
 * Typical signature for the didReceiveNotificationCount method will be
 * public void didReceiveNotificationCount (string count);
 */
extern "C" int hsGetNotificationCountFromRemote(bool isRemote);

/*
 * Register the deviceToken to enable push notifications
 *
 * To enable push notifications in the Helpshift iOS SDK, set the Push Notifications’ deviceToken using this method.
 */

extern "C" void hsRegisterDeviceToken(const char *deviceToken);

/*
 * Forward the push notification for the Helpshift lib to handle
 *
 * To show support on Notification opened, call handleRemoteNotificationForIssue when you receive a push notification with the value of
 * the “origin” field is “helpshift”. Pass the issue_id received from the notification dictionary to this api to launch the appropriate issue.
 *
 */
extern "C" void hsHandleRemoteNotificationForIssue(const char *issue_id);

/*
 * Forward the local notification for the Helpshift lib to handle
 *
 * To show support on Notification opened, call handleLocalNotificationForIssue when you receive a local notification with the value of
 * the “origin” field is “helpshift” in the userInfo dictionary of the notification object.
 * Pass the issue_id received from the notification dictionary to this api to launch the appropriate issue.
 *
 */

extern "C" void hsHandleLocalNotificationForIssue(const char *issue_id);

/*
 * Set the metaData which will be sent along with the reported issue
 *
 * This api is meant to be called from the updateMetaData message handler in the Unity script.
 *
 */
extern "C" void hsSetMetaData(const char *metaDictionaryString);

/* To pause and restart the display of inapp notification
 *
 * When this method is called with boolean value YES, inapp notifications are paused and not displayed.
 * To restart displaying inapp notifications pass the boolean value NO.
 *
 * @param pauseInApp the boolean value to pause/restart inapp nofitications
 *
 */

extern "C" void hsPauseDisplayOfInAppNotification(bool pauseInApp);

/* Show alert for app rating
 *
 * To manually show an alert for app rating, you need automated reviews disabled in admin.
 * Also, if there is an ongoing conversation, the review alert will not show up.
 *
 * @param url the app store URL for your app
 */

extern "C" void hsShowAlertToRateAppWithURL(const char *url);

extern "C" void hsRegisterForRemoteNotifications();

extern "C" void hsLogin(const char *identifier, const char *name, const char *email);

extern "C" void hsLogout();

extern "C" void hsRegisterHelpshiftDeepLinking();

extern "C" void hsRegisterForLocalNotifications();

extern "C" void hsSetSDKLanguage(const char *locale);