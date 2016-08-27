using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour {

    public GameObject pauseMenu;

	public void exitToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void continueGame()
    {
        Time.timeScale = 1.0f;
        pauseMenu.SetActive(false);
    }
}
