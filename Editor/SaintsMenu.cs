using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace SaintsDraw.Editor
{
    public static class SaintsMenu
    {

#if !SAINTSDRAW
        [InitializeOnLoadMethod]
        public static void AddInstalledMarco() => AddCompileDefine("SAINTSDRAW");
#endif

        #region Gizmos
#if SAINTSDRAW_GIZMOS_DISABLE
        [MenuItem("Window/Saints/Draw/Enable Gizmos")]
        public static void UIToolkit() => RemoveCompileDefine("SAINTSDRAW_GIZMOS_DISABLE");
#else
        [MenuItem("Window/Saints/Draw/Disable Gizmos")]
        public static void UIToolkit() => AddCompileDefine("SAINTSDRAW_GIZMOS_DISABLE");
#endif

        #endregion

        // ReSharper disable once UnusedMember.Local
        private static void AddCompileDefine(string newDefineCompileConstant, IEnumerable<BuildTargetGroup> targetGroups = null)
        {
            IEnumerable<BuildTargetGroup> targets = targetGroups ?? Enum.GetValues(typeof(BuildTargetGroup)).Cast<BuildTargetGroup>();

            foreach (BuildTargetGroup grp in targets.Where(each => each != BuildTargetGroup.Unknown))
            {
                string defines;
                try
                {
                    defines = PlayerSettings.GetScriptingDefineSymbolsForGroup(grp);
                }
                catch (ArgumentException)
                {
                    continue;
                }
                if (!defines.Contains(newDefineCompileConstant))
                {
                    if (defines.Length > 0)
                        defines += ";";

                    defines += newDefineCompileConstant;
                    try
                    {
                        PlayerSettings.SetScriptingDefineSymbolsForGroup(grp, defines);
                    }
                    catch (Exception e)
                    {
                        Debug.LogException(e);
                    }
                }
            }
        }

        // ReSharper disable once UnusedMember.Local
        private static void RemoveCompileDefine(string defineCompileConstant, IEnumerable<BuildTargetGroup> targetGroups = null)
        {
            IEnumerable<BuildTargetGroup> targets = targetGroups ?? Enum.GetValues(typeof(BuildTargetGroup)).Cast<BuildTargetGroup>();

            foreach (BuildTargetGroup grp in targets.Where(each => each != BuildTargetGroup.Unknown))
            {
                string defines;
                try
                {
                    defines = PlayerSettings.GetScriptingDefineSymbolsForGroup(grp);
                }
                catch (ArgumentException)
                {
                    continue;
                }

                string result = string.Join(";", defines
                    .Split(';')
                    .Select(each => each.Trim())
                    .Where(each => each != defineCompileConstant));

                // Debug.Log(result);

                try
                {
                    PlayerSettings.SetScriptingDefineSymbolsForGroup(grp, result);
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                }
            }
        }
    }
}
