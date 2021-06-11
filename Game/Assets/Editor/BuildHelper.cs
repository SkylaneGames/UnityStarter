using UnityEditor;

namespace Editor
{
    class BuildHelper
    {
        static readonly string[] scenes = { "Assets/Scenes/Splash.unity","Assets/Scenes/Main Menu.unity","Assets/Scenes/Game.unity" };

        private static void PerformBuildWebGL()
        {
            BuildPipeline.BuildPlayer(scenes, "../build", BuildTarget.WebGL, BuildOptions.None);
        }
    
        private static void PerformBuildWin64()
        {
            BuildPipeline.BuildPlayer(scenes, "../build/game.exe", BuildTarget.StandaloneWindows64, BuildOptions.None);
        }

        private static void PerformBuildOsx()
        {
            BuildPipeline.BuildPlayer(scenes, "../build/game.app", BuildTarget.StandaloneOSX, BuildOptions.None);
        }
    
        private static void PerformBuildLinux()
        {
            BuildPipeline.BuildPlayer(scenes, "../build/game.x86_64", BuildTarget.StandaloneLinux64, BuildOptions.None);
        }

        private static void Build(BuildTarget target)
        {
            BuildPipeline.BuildPlayer(scenes, "../build", target, BuildOptions.None);
        }
    }
}