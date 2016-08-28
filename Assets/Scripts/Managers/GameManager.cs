using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    public GameObject pauseMenu;

    void Start()
    {
        InputManager.Instance.RegisterKeyDown("pause", pauseGame);
        UnityEngine.Cursor.visible = false;
    }

    public void pauseGame(string key)
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0.0f;
    }
}
