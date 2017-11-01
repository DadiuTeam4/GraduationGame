//Author: Emil Villumsen
//Collaborator: Jonathan,
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Events;

public class SceneLoaderManager : Singleton<SceneLoaderManager> 
{

    // Variables to keep track of scenes to load and unload.
    GameScene previousScene;
    GameScene currentScene;
    GameScene nextScene;

    // Clusters of scenes to be loaded at certain points.
    GameScene[] gameStart = { GameScene.GameOpener, GameScene.GlobalScene, GameScene.IntroLevel, GameScene.CrossRoad1 };
    GameScene[] gameEnd = { GameScene.EndScene, GameScene.Credits };

    void Start()
    {
        SceneClusterLoader(gameStart);
        //EventManager.AddListener(CustomEvent.changeScene, sceneLoader(EventArgument));
     
        previousScene = GameScene.GameOpener;
        currentScene = GameScene.IntroLevel;
        nextScene = GameScene.CrossRoad1;


    }

    // Start the game!
    private void SceneClusterLoader(GameScene[] cluster)
    {
        foreach (GameScene sceneName in cluster)
        {
            var sceneToLoad = GameSceneToString(sceneName);
            Scene scene = SceneManager.GetSceneByName(sceneToLoad);
            if (!scene.isLoaded)
            {
                SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Additive);
            }
        }
    }

    // The main scene changing function. Updates scene trackers and loads and unloads scenes.
    private void sceneLoader(GameScene upComingScene)
    {
        // This system is not correct, we need to find a way to keep track of
        // upcoming scenes.
        var sceneToUnload = GameSceneToString(previousScene);

        previousScene = currentScene;
        currentScene = nextScene;
        nextScene = upComingScene;

        var sceneToLoad = GameSceneToString(nextScene);

        Scene scene = SceneManager.GetSceneByName(sceneToLoad);
        if (!scene.isLoaded)
        {
            SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Additive);
        }

        SceneManager.UnloadSceneAsync(sceneToUnload);

    }

    // Takes a GameScene Enum and returns the scene name string.
    private string GameSceneToString(GameScene sceneName)
    {
        return sceneName.ToString("D");
    }


    public enum GameScene
    {
        GameOpener,
        GlobalScene,
        EndScene,
        Credits,
        IntroLevel,
        CrossRoad1,
        RitualEvent

    }

}