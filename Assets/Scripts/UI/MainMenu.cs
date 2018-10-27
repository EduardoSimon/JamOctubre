using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    GameObject other;
    PauseMenu _audioManager;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void Start()
    {
        other = GameObject.FindWithTag("AudioManager");
        _audioManager = other.GetComponent<PauseMenu>();
    }

    public void PlayGame()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
	}

	public void QuitGame()
	{
		Debug.Log ("QUIT");
		Application.Quit ();
	}

    public void Menu()
    {
        SceneManager.LoadScene((int)SceneLoader.SCENES.MenuBueno);
    }

    public void Controls()
    {
        SceneManager.LoadScene("MenuJAMControls");
    }

    public void Options()
    {
        _audioManager.OptionsMainMenu();
    }

}
