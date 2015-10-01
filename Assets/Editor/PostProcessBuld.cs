using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;

public class PostProcessBuld {
#if UNITY_EDITOR
    private static string ExportProjectPath = "";
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

    static void DoPostBuildForAndroid()
    {
        // -- replace manifest.xml
        string androidManifestFile = ExportProjectPath + "/unity-app/AndroidManifest.xml";
        File.Copy(ExportProjectPath + "/unity-app/AndroidManifestSource.xml", androidManifestFile, true);
    }

    static void DoPostBuildForIos()
    {
        
    }
#endif
}
