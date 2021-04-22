using UnityEditor;

class BuildHelper
{
    static string[] scenes = { "Assets/Scenes/Splash.unity","Assets/Scenes/Main Menu.unity","Assets/Scenes/Game.unity" };
    
    static void PerformBuildWebGL()
    {
        BuildPipeline.BuildPlayer(scenes, "../build", BuildTarget.WebGL, BuildOptions.None);
    }
    
    static void PerformBuildWin64()
    {
        BuildPipeline.BuildPlayer(scenes, "../build/game.exe", BuildTarget.StandaloneWindows64, BuildOptions.None);
    }
    
    static void PerformBuildOSX()
    {
        BuildPipeline.BuildPlayer(scenes, "../build/game.app", BuildTarget.StandaloneOSX, BuildOptions.None);
    }
    
    static void PerformBuildLinux()
    {
        BuildPipeline.BuildPlayer(scenes, "../build/game.x86_64", BuildTarget.StandaloneLinux64, BuildOptions.None);
    }

    static void Build(BuildTarget target)
    {
        BuildPipeline.BuildPlayer(scenes, "../build", target, BuildOptions.None);
    }
}