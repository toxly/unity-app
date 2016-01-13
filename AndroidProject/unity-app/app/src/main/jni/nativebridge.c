#include <jni.h>
#include "android/log.h"
#include "GateKeeper.h"
#include <stdio.h>

JNIEXPORT jstring JNICALL
Java_com_snow_plugin_NativeBridge_getCppVersion(JNIEnv *env, jclass type) {
    __android_log_write(ANDROID_LOG_ERROR,LOG_TAG,CPP_VERSION);

//    std::string file1 = "c/c5227223ebfadaba301b345c5f3b1a06.ddx";	// -- ui.assets
//    std::string file2 = "0/08618351e2cbdd72e490bc9c0cdcdb27.ddx";	// -- ../sharedassets1.assets
//    std::string file3 = "a/abdb417ad1ca743de07ab3f195ba46a1.ddx";	// -- ../sharedassets2.assets
//
//    // -- add app version to url
//    std::string needle = "deploy_res";
//    std::string fileUrl = "https://sgbu3d.ucimg.co/ioshd/deploy_res/";
//
//    size_t foundPos = fileUrl.rfind(needle);
//    std::string url1 = "";
//    if (foundPos!=std::string::npos) {
//        url1 = fileUrl.replace(foundPos,1,"/12/");
//        __android_log_write(ANDROID_LOG_ERROR,LOG_TAG,"url1: "+url1);
//    }

    return (*env)->NewStringUTF(env, CPP_VERSION);
}