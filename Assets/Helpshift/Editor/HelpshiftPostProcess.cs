using System;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;

using System.Diagnostics;
using System.Collections;
#if UNITY_IOS || UNITY_ANDROID
using Helpshift;
#endif
public class HelpshiftPostProcess : MonoBehaviour
{
	// Set PostProcess priority to a high number to ensure that the this is started last.
	[PostProcessBuild(900)]
	public static void OnPostprocessBuild(BuildTarget target, string pathToBuildProject)
	{
		HelpshiftConfig.Instance.SaveConfig();
        const string helpshift_plugin_path = "Assets/Helpshift";

        // Current path while executing the script is
        // the project root folder.

		Process myCustomProcess = new Process();
		myCustomProcess.StartInfo.FileName = "python";
		myCustomProcess.StartInfo.Arguments = string.Format(helpshift_plugin_path + "/Editor/HSPostprocessBuildPlayer " + pathToBuildProject + " " + target);
		myCustomProcess.StartInfo.UseShellExecute = false;
		myCustomProcess.StartInfo.RedirectStandardOutput = false;
		myCustomProcess.Start();
		myCustomProcess.WaitForExit();
	}

}
