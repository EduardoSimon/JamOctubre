using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : Singleton<SceneLoader>
{
    public enum SCENES
    {
        MenuBueno = 0,
        Game = 1,
        Score = 2,
    }

    public SCENES currentScene = SCENES.MenuBueno;
    
    public void LoadScene(SCENES sceneToLoad)
    {
        SceneManager.LoadScene((int)sceneToLoad);
    }

}
