//Author: Emil Villumsen
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderManager : Singleton<SceneLoaderManager> {

    string[] scenes = { "Crossroads", "IntroLevel", "RitualEvent", "GlobalScene" }; 

    void Start()
    {
        foreach (string sceneName in scenes)
        {
            Scene scene = SceneManager.GetSceneByName(sceneName);
            if (!scene.isLoaded)
            {
                SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
            }
        }
    }

}
