using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;

public class PostProcessBuld {
#if UNITY_EDITOR
    private static string ExportProjectPath = "";
    private static string AppFolderName = "unity-app";
    //url http://docs.unity3d.com/ScriptReference/Callbacks.PostProcessBuildAttribute.html
    [PostProcessBuildAttribute(1)]
    static void OnPostProcessBuild(BuildTarget target, string exportPath)
    {
        Debug.Log("OnPostProcessBuild for target " + target + ", with path: " + exportPath);
        ExportProjectPath = exportPath;

        if (target == BuildTarget.Android)
        {
            DoPostBuildForAndroid();
        } else if (target == BuildTarget.iPhone)
        {
            DoPostBuildForIos();
        }
    }

    // -- the export is eclipse project, change to android studio project
    static void DoPostBuildForAndroid()
    {
        string AppPath = ExportProjectPath + "/" + AppFolderName + "/";

        // -- move assets
        Debug.Log("Move assets to app/src/main/assets");
        if (Directory.Exists(AppPath + "app/src/main/assets"))
        {
            Directory.Delete(AppPath + "app/src/main/assets", true);
        }
        Directory.Move(AppPath + "assets", AppPath + "app/src/main/assets");

        // -- move libs
        if (File.Exists(AppPath + "app/libs/unity-classes.jar"))
        {
            File.Delete(AppPath + "app/libs/unity-classes.jar");
        }
        if (!Directory.Exists(AppPath + "app/libs"))
        {
            Directory.CreateDirectory(AppPath + "app/libs/");
        }
        File.Move(AppPath + "libs/unity-classes.jar", AppPath + "app/libs/unity-classes.jar");

        // -- move jni libs
        Debug.Log("Move jni libs to app/src/main/jniLibs");
        if (Directory.Exists(AppPath + "app/src/main/jniLibs"))
        {
            Directory.Delete(AppPath + "app/src/main/jniLibs", true);
        }
        Directory.Move(AppPath + "libs", AppPath + "app/src/main/jniLibs");

        // -- delete res
        Debug.Log("Delete res");
        Directory.Delete(AppPath + "res", true);

        // -- delete src
        Debug.Log("Delete src");
        Directory.Delete(AppPath + "src", true);
    }

    static void DoPostBuildForIos()
    {
        
    }
#endif
}
