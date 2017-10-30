using UnityEngine;

using System.Collections;

using UnityEditor;

using System;

using System.Collections.Generic;



public class BuildScript : MonoBehaviour

{
    private static string[] scenesNames = GetSceneNames();

    [MenuItem("MyTools/Jenkins build test")]
    public static void PerformBuild()

    {

        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();

        buildPlayerOptions.scenes = scenesNames;

        buildPlayerOptions.locationPathName = "Builds/gameTest.apk";

        buildPlayerOptions.target = BuildTarget.Android;

        buildPlayerOptions.options = BuildOptions.None;

        BuildPipeline.BuildPlayer(buildPlayerOptions);

    }

    private static string[] GetSceneNames()
    {
        EditorBuildSettingsScene[] buildScenes = EditorBuildSettings.scenes;
        string[] names = new string[buildScenes.Length];
        int index = 0;
        IEnumerator enumerator = buildScenes.GetEnumerator();
        while (enumerator.MoveNext())
        {
            EditorBuildSettingsScene scene = (EditorBuildSettingsScene)enumerator.Current;
            names[index] = scene.path;
            index++;
        }
        return names;
    }
}