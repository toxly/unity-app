#include <jni.h>
#include "android/log.h"
#include "GateKeeper.h"

JNIEXPORT jstring JNICALL
Java_com_snow_plugin_NativeBridge_getCppVersion(JNIEnv *env, jclass type) {
    __android_log_write(ANDROID_LOG_ERROR,LOG_TAG,CPP_VERSION);
    return (*env)->NewStringUTF(env, CPP_VERSION);
}