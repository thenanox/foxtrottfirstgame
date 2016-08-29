using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour {

    public GameObject pauseMenu;

    void OnEnable()
    {
        UnityEngine.Cursor.visible = true;
    }

    void OnDisable()
    {
        UnityEngine.Cursor.visible = false;
    }

	public void exitToMainMenu()
    {
        GameObject sound = GameObject.FindWithTag("Sound");

        if (sound)
            Destroy(sound);

        SceneManager.LoadScene(0);
    }

    public void continueGame()
    {
        Time.timeScale = 1.0f;
        pauseMenu.SetActive(false);
    }
}
