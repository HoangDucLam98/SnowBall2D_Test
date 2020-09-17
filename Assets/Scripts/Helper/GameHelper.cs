using System;
using System.Collections.Generic;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public static class GameHelper
{
    public static DateTime ConvertStringToDateTime(string s)
    {
        s = s.Replace('-', '/');
        s = s.Replace('_', ':');
        return Convert.ToDateTime(s);
    }

#if UNITY_EDITOR
    public static void SetUpDefineSymbolsForGroup(string key, bool enable)
    {
        Debug.Log(enable);
        BuildTargetGroup buildTargetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
        string defines = PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup);
        // Only if not defined already.
        if (defines.Contains(key))
        {
            if (enable)
            {
                Debug.LogWarning("Selected build target (" + EditorUserBuildSettings.activeBuildTarget.ToString() + ") already contains <b>" + key + "</b> <i>Scripting Define Symbol</i>.");
                return;
            }
            else
            {
                PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup, (defines.Replace(key, "")));

                return;
            }
        }
        else
        {
            // Append
            if (enable)
                PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup, (defines + ";" + key));
        }


    }
#endif
}

