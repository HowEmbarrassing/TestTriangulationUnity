using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyCSharp.Assets
{
    using System.IO;
    using UnityEngine;
    using UnityEditor;

    public class OpenFilePanel : EditorWindow
    {
        [MenuItem("Example/Overwrite Texture")]
        static void Apply()
        {
            Texture2D texture = Selection.activeObject as Texture2D;
            if (texture == null)
            {
                EditorUtility.DisplayDialog("Select Texture", "You must select a texture first!", "OK");
                return;
            }

            string path = EditorUtility.OpenFilePanel("Overwrite with png", "", "png");
            if (path.Length != 0)
            {
                var fileContent = File.ReadAllBytes(path);
                texture.LoadImage(fileContent);
            }
        }
    }
}
