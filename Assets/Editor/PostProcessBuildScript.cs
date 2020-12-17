using UnityEditor;
using UnityEditor.Callbacks;
using System.IO;


public class PostProcessBuildScript
{

    [PostProcessBuild(1)]
    public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject)
    {
        var path = Path.Combine(pathToBuiltProject, "Build/UnityLoader.js");
        var text = File.ReadAllText(path);
        text = text.Replace("UnityLoader.SystemInfo.mobile", "false");
        File.WriteAllText(path, text);
    }

}
