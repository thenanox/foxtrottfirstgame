using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneSwitcher : MonoBehaviour {

    public GameObject loadingImage;
    public Slider bar;
    private AsyncOperation async;

    void OnEnable()
    {
        UnityEngine.Cursor.visible = true;
    }

    public IEnumerator loadLevel(int level)
    {
        async = SceneManager.LoadSceneAsync(level);
        while (!async.isDone)
        {
            bar.value = async.progress;
            yield return null;
        }
    }

    public void exitGame()
    {
        Application.Quit();
    }

    public void loadScene(int level)
    {
        AudioManager.Instance.StartGame();
        loadingImage.SetActive(true);
        StartCoroutine(loadLevel(level));
    }
}
