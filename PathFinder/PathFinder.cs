using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;


namespace FuguFirecracker.Utility
{
    public static class PathFinder
    {
        private static readonly char[] Seperators = {'/', '\\'};

        public static T LoadAsset<T>(string topLevelDir, string dir, string file) where T : Object
        {
            return AssetDatabase.LoadAssetAtPath<T>(FindInSubDirectory(topLevelDir, dir, file));
        }

        /// <summary>
        /// Finds a file in chosen Directory relative to a TopLevel Directory
        /// </summary>
        /// <param name="topLevelDir"></param>
        /// <param name="dir"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public static string FindInSubDirectory(string topLevelDir, string dir, string file)
        {
            var p1 = Directory.GetDirectories(Application.dataPath, topLevelDir, SearchOption.AllDirectories);
            var p2 = Directory.GetDirectories(p1[0], dir, SearchOption.AllDirectories);

            var sb = new StringBuilder(p2[0]);
            sb.Replace(Application.dataPath, "Assets");
            sb.AppendFormat("{0}{1}", '\\', file);
            return sb.ToString();
        }

        /// <summary>
        /// Find a file. Do include filename and ext
        /// </summary>
        /// <param name="relativePath"></param>
        /// <returns></returns>
        public static string Find(string relativePath)
        {
            var dirs = relativePath.Split(Seperators);
            var p1 = Directory.GetDirectories(Application.dataPath, dirs[0], SearchOption.AllDirectories);
            var p2 = Directory.GetDirectories(p1[0], dirs[dirs.Length - 2], SearchOption.AllDirectories);

            if (p2.Length == 0) { p2 = p1; }

            var sb = new StringBuilder(p2[0]);
            sb.Replace(Application.dataPath, "Assets");
            sb.AppendFormat("{0}{1}", '\\', dirs[dirs.Length - 1]);

            return sb.ToString();
        }
    }
}
