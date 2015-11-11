    /*
     * Copyright 2015, Helpshift, Inc.
     * All rights reserved
     */

    #if UNITY_ANDROID
    using UnityEngine;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using HSMiniJSON;
    using System.Linq;

    namespace Helpshift
    {
        public class HelpshiftAndroid {

            private AndroidJavaClass jc;
            private AndroidJavaObject currentActivity, application;
            private AndroidJavaObject hsPlugin;

            private AndroidJavaObject convertToJavaHashMap (Dictionary<string, object> configD) {
                AndroidJavaObject config_Hashmap = new AndroidJavaObject("java.util.HashMap");
                if(configD != null) {
                    Dictionary<string, object> configDict = (from kv in configD where kv.Value != null select kv).ToDictionary(kv => kv.Key, kv => kv.Value);
                    IntPtr method_Put = AndroidJNIHelper.GetMethodID(config_Hashmap.GetRawClass(), "put",
                                                                     "(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;");
                    object[] args = new object[2];
                    args[0] = args[1] = null;
                    foreach(KeyValuePair<string, object> kvp in configDict) {
                        using(AndroidJavaObject k = new AndroidJavaObject("java.lang.String", kvp.Key))
                        {
                            args[0] = k;
                            if(kvp.Value != null && kvp.Value.Equals("yes") || kvp.Value.Equals("no")) {
                                string value = kvp.Value.Equals("yes") ? "true" : "false";
                                args[1] = new AndroidJavaObject("java.lang.Boolean", value);
                            } else if (kvp.Value != null) {
                                if(kvp.Value.GetType().ToString() == "System.String") {
                                    args[1] = new AndroidJavaObject("java.lang.String", kvp.Value);
                                } else if (kvp.Value.GetType().ToString() == "System.String[]") {
                                    string[] tagsArray = (string[]) kvp.Value;
                                    AndroidJavaObject tags_ArrayList = new AndroidJavaObject("java.util.ArrayList");
                                    IntPtr method_add = AndroidJNIHelper.GetMethodID(tags_ArrayList.GetRawClass(), "add",
                                                                                    "(Ljava/lang/String;)Z");
                                    object[] tags_args = new object[1];
                                    foreach(string tag in tagsArray) {
                                    if(tag != null) {
                                        tags_args[0] = new AndroidJavaObject("java.lang.String", tag);
                                        AndroidJNI.CallBooleanMethod(tags_ArrayList.GetRawObject(),
                                                                    method_add, AndroidJNIHelper.CreateJNIArgArray(tags_args));
                                        }
                                    }
                                    args[1] = new AndroidJavaObject("java.util.ArrayList", tags_ArrayList);
                                } else {
                                    if(kvp.Key == HelpshiftSdk.HSCUSTOMMETADATAKEY) {
                                        Dictionary<string, object> metaMap = (Dictionary<string, object>) kvp.Value;
                                        AndroidJavaObject meta_Hashmap = new AndroidJavaObject("java.util.HashMap");
                                        IntPtr method_MetaPut = AndroidJNIHelper.GetMethodID(meta_Hashmap.GetRawClass(), "put",
                                                                                             "(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;");
                                        object[] meta_args = new object[2];
                                        meta_args[0] = meta_args[1] = null;
                                        foreach(KeyValuePair<string, object> metaKvp in metaMap) {
                                            meta_args[0] = new AndroidJavaObject("java.lang.String", metaKvp.Key);
                                            if(metaKvp.Value.GetType().ToString() == "System.String") {
                                                meta_args[1] = new AndroidJavaObject("java.lang.String", metaKvp.Value);
                                            } else if(metaKvp.Value.GetType().ToString() == "System.Int32") {
                                                meta_args[1] = new AndroidJavaObject("java.lang.Integer", metaKvp.Value);
                                            } else if(metaKvp.Value.GetType().ToString() == "System.Double") {
                                                meta_args[1] = new AndroidJavaObject("java.lang.Double", metaKvp.Value);
                                            } else if (metaKvp.Key == HelpshiftSdk.HSTAGSKEY && metaKvp.Value.GetType().ToString() == "System.String[]") {
                                                string[] tagsArray = (string[]) metaKvp.Value;
                                                AndroidJavaObject tags_ArrayList = new AndroidJavaObject("java.util.ArrayList");
                                                IntPtr method_add = AndroidJNIHelper.GetMethodID(tags_ArrayList.GetRawClass(), "add",
                                                                                                 "(Ljava/lang/String;)Z");
                                                object[] tags_args = new object[1];
                                                foreach(string tag in tagsArray) {
                                                    if(tag != null) {
                                                        tags_args[0] = new AndroidJavaObject("java.lang.String", tag);
                                                        AndroidJNI.CallBooleanMethod(tags_ArrayList.GetRawObject(),
                                                                                     method_add, AndroidJNIHelper.CreateJNIArgArray(tags_args));
                                                    }
                                                }
                                                meta_args[1] = new AndroidJavaObject("java.util.ArrayList", tags_ArrayList);
                                            }
                                            if(meta_args[1] != null) {
                                                AndroidJNI.CallObjectMethod(meta_Hashmap.GetRawObject(),
                                                                            method_MetaPut, AndroidJNIHelper.CreateJNIArgArray(meta_args));
                                            }
                                        }
                                        args[1] = new AndroidJavaObject("java.util.HashMap", meta_Hashmap);
                                    }
                                    if(kvp.Key == HelpshiftSdk.HSTAGSMATCHINGKEY) {
                                        Dictionary<string, object> tagsMatchingMap = (Dictionary<string, object>) kvp.Value;
                                        AndroidJavaObject tagsMatching_Hashmap = new AndroidJavaObject("java.util.HashMap");
                                        IntPtr method_MetaPut = AndroidJNIHelper.GetMethodID(tagsMatching_Hashmap.GetRawClass(), "put",
                                                                                             "(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;");
                                        object[] tagsMatching_args = new object[2];
                                        tagsMatching_args[0] = tagsMatching_args[1] = null;
                                        foreach(KeyValuePair<string, object> tagsMatchKvp in tagsMatchingMap) {
                                            tagsMatching_args[0] = new AndroidJavaObject("java.lang.String", tagsMatchKvp.Key);
                                            if (tagsMatchKvp.Key == "operator" && tagsMatchKvp.Value.GetType().ToString()  == "System.String") {
                                                tagsMatching_args[1] = new AndroidJavaObject("java.lang.String", tagsMatchKvp.Value);
                                            } else if (tagsMatchKvp.Key == "tags" && tagsMatchKvp.Value.GetType().ToString() == "System.String[]") {
                                                string[] tagsArray = (string[]) tagsMatchKvp.Value;
                                                AndroidJavaObject tags_ArrayList = new AndroidJavaObject("java.util.ArrayList");
                                                IntPtr method_add = AndroidJNIHelper.GetMethodID(tags_ArrayList.GetRawClass(), "add",
                                                                                                 "(Ljava/lang/String;)Z");
                                                object[] tags_args = new object[1];
                                                foreach(string tag in tagsArray) {
                                                    if(tag != null) {
                                                        tags_args[0] = new AndroidJavaObject("java.lang.String", tag);
                                                        AndroidJNI.CallBooleanMethod(tags_ArrayList.GetRawObject(),
                                                                                     method_add, AndroidJNIHelper.CreateJNIArgArray(tags_args));
                                                    }
                                                }
                                                tagsMatching_args[1] = new AndroidJavaObject("java.util.ArrayList", tags_ArrayList);
                                            }
                                            if(tagsMatching_args[1] != null) {
                                                AndroidJNI.CallObjectMethod(tagsMatching_Hashmap.GetRawObject(),
                                                                            method_MetaPut, AndroidJNIHelper.CreateJNIArgArray(tagsMatching_args));
                                            }
                                        }
                                        args[1] = new AndroidJavaObject("java.util.HashMap", tagsMatching_Hashmap);
                                    }
                                }
                            }
                            if(args[1] != null) {
                                AndroidJNI.CallObjectMethod(config_Hashmap.GetRawObject(),
                                                            method_Put, AndroidJNIHelper.CreateJNIArgArray(args));
                            }
                        }
                    }
                }
                return config_Hashmap;
            }

            private AndroidJavaObject convertMetadataToJavaHashMap (Dictionary<string, object> metaMap) {
                AndroidJavaObject meta_Hashmap = new AndroidJavaObject("java.util.HashMap");
                if(metaMap != null) {
                    IntPtr method_MetaPut = AndroidJNIHelper.GetMethodID(meta_Hashmap.GetRawClass(), "put",
                                                                         "(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;");
                    object[] meta_args = new object[2];
                    meta_args[0] = meta_args[1] = null;

                    foreach(KeyValuePair<string, object> metaKvp in metaMap) {
                        meta_args[0] = new AndroidJavaObject("java.lang.String", metaKvp.Key);
                        if(metaKvp.Value.GetType().ToString() == "System.String") {
                            if (metaKvp.Value != null && metaKvp.Value.Equals("yes") || metaKvp.Value.Equals("no")) {
                                string value = metaKvp.Value.Equals("yes") ? "true" : "false";
                                meta_args[1] = new AndroidJavaObject("java.lang.Boolean", value);
                            } else {
                                meta_args[1] = new AndroidJavaObject("java.lang.String", metaKvp.Value);
                            }
                        } else if (metaKvp.Key == HelpshiftSdk.HSTAGSKEY && metaKvp.Value.GetType().ToString() == "System.String[]"){
                            string[] tagsArray = (string[]) metaKvp.Value;
                            AndroidJavaObject tags_ArrayList = new AndroidJavaObject("java.util.ArrayList");
                            IntPtr method_add = AndroidJNIHelper.GetMethodID(tags_ArrayList.GetRawClass(), "add",
                                                                             "(Ljava/lang/String;)Z");
                            object[] tags_args = new object[1];
                            foreach(string tag in tagsArray) {
                                if(tag != null) {
                                    tags_args[0] = new AndroidJavaObject("java.lang.String", tag);
                                    AndroidJNI.CallBooleanMethod(tags_ArrayList.GetRawObject(),
                                                                 method_add, AndroidJNIHelper.CreateJNIArgArray(tags_args));
                                }
                            }
                            meta_args[1] = new AndroidJavaObject("java.util.ArrayList", tags_ArrayList);
                        }
                        if(meta_args[1] != null) {
                            AndroidJNI.CallObjectMethod(meta_Hashmap.GetRawObject(),
                                                        method_MetaPut, AndroidJNIHelper.CreateJNIArgArray(meta_args));
                        }
                    }
                }
                Debug.Log("Returning the Hashmap : " + meta_Hashmap);
                return meta_Hashmap;
            }

            void hsApiCall(string api, params object[] args) {
                hsPlugin.CallStatic (api, args);
            }

            void hsApiCall(string api) {
                hsPlugin.CallStatic (api);
            }

            public HelpshiftAndroid () {
                if(Application.platform == RuntimePlatform.Android) {
                    this.jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                    this.currentActivity = jc.GetStatic<AndroidJavaObject>("currentActivity");
                    this.application = this.currentActivity.Call<AndroidJavaObject>("getApplication");
                    this.hsPlugin = new AndroidJavaClass("com.helpshift.Helpshift");
                }
            }

            public void install (string apiKey, string domain, string appId, Dictionary<string, object> configMap) {
                hsApiCall("install", new object[] {this.application, apiKey, domain, appId, convertToJavaHashMap(configMap)});
            }

            public void install () {
                hsApiCall("install", new object[] {this.application});
            }

            public int getNotificationCount (Boolean isAsync) {
                return this.hsPlugin.CallStatic<int> ("getNotificationCount", isAsync);
            }

            public void setNameAndEmail (string userName, string email) {
                hsApiCall("setNameAndEmail", new object[] {userName, email});
            }

            public void setUserIdentifier (string identifier) {
                hsApiCall("setUserIdentifier", identifier);
            }

            public void registerDeviceToken (string deviceToken) {
                hsApiCall("registerDeviceToken", new object [] {this.currentActivity, deviceToken});
            }

            public void leaveBreadCrumb (string breadCrumb) {
                hsApiCall("leaveBreadCrumb", breadCrumb);
            }

            public void clearBreadCrumbs () {
                hsApiCall("clearBreadCrumbs");
            }

            public void login (string identifier, string userName, string email) {
                hsApiCall("login", new object[] {identifier, userName, email});
            }

            public void logout() {
                hsApiCall("logout");
            }

            public void showConversation (Dictionary<string, object> configMap) {
                hsApiCall("showConversation", new object [] {this.currentActivity, convertToJavaHashMap(configMap)});
            }

            public void showFAQSection (string sectionPublishId, Dictionary<string, object> configMap) {
                hsApiCall("showFAQSection", new object[] {this.currentActivity, sectionPublishId, convertToJavaHashMap(configMap)});
            }

            public void showSingleFAQ (string questionPublishId, Dictionary<string, object> configMap) {
                hsApiCall("showSingleFAQ", new object[] {this.currentActivity, questionPublishId, convertToJavaHashMap(configMap)});
            }

            public void showFAQs (Dictionary<string, object> configMap) {
                hsApiCall("showFAQs", new object [] { this.currentActivity, convertToJavaHashMap(configMap)});
            }

            public void showConversation () {
                hsApiCall("showConversation");
            }

            public void showFAQSection (string sectionPublishId) {
                hsApiCall("showFAQSection", new object[] {sectionPublishId});
            }

            public void showSingleFAQ (string questionPublishId) {
                hsApiCall("showSingleFAQ", new object[] {questionPublishId});
            }

            public void showFAQs () {
                hsApiCall("showFAQs");
            }

            public void showConversationWithMeta (Dictionary<string, object> configMap) {
                hsApiCall("showConversationWithMeta", convertMetadataToJavaHashMap(configMap));
            }

            public void showFAQSectionWithMeta (string sectionPublishId, Dictionary<string, object> configMap) {
                hsApiCall("showFAQSectionWithMeta", new object[] {sectionPublishId, convertMetadataToJavaHashMap(configMap)});
            }

            public void showSingleFAQWithMeta (string questionPublishId, Dictionary<string, object> configMap) {
                hsApiCall("showSingleFAQWithMeta", new object[] {questionPublishId, convertMetadataToJavaHashMap(configMap)});
            }

            public void showFAQsWithMeta (Dictionary<string, object> configMap) {
                hsApiCall("showFAQsWithMeta", convertMetadataToJavaHashMap(configMap));
            }

            public void updateMetaData(Dictionary<string, object> metaData) {
                hsApiCall("setMetaData", Json.Serialize(metaData));
            }

            public void handlePushNotification(string issueId) {
                hsApiCall("handlePush", new object[] {this.currentActivity, issueId});
            }

            public void showAlertToRateAppWithURL (string url) {
                hsApiCall("showAlertToRateApp", url);
            }

            public void registerSessionDelegates() {
                hsApiCall("registerSessionDelegates");
            }

            public void registerForPushWithGcmId(string gcmId) {
                hsApiCall("registerGcmKey", new object[] {gcmId, this.currentActivity});
            }

            public void setSDKLanguage(string locale) {
                hsApiCall("setSDKLanguage", new object[] {locale});
            }
        }

        public class HelpshiftAndroidLog {
            private static AndroidJavaClass logger = null;
            private HelpshiftAndroidLog () {
            }

            private static void initLogger () {
                if(HelpshiftAndroidLog.logger == null) {
                    HelpshiftAndroidLog.logger = new AndroidJavaClass("com.helpshift.Log");
                }
            }
            public static int v (String tag, String log) {
                initLogger();
                return HelpshiftAndroidLog.logger.CallStatic<int> ("v", new object[] {tag, log});
            }

            public static int d (String tag, String log) {
                initLogger();
                return HelpshiftAndroidLog.logger.CallStatic<int> ("d", new object[] {tag, log});
            }

            public static int i (String tag, String log) {
                initLogger();
                return HelpshiftAndroidLog.logger.CallStatic<int> ("i", new object[] {tag, log});
            }

            public static int w (String tag, String log) {
                initLogger();
                return HelpshiftAndroidLog.logger.CallStatic<int> ("w", new object[] {tag, log});
            }

            public static int e (String tag, String log) {
                initLogger();
                return HelpshiftAndroidLog.logger.CallStatic<int> ("e", new object[] {tag, log});
            }

        }
    }
    #endif
