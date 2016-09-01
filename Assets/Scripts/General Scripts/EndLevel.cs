using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour {

    public int numberLevel = 1;

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            if (numberLevel == 6)
            {
                GameObject sound = GameObject.Find("MusicManager(Clone)");
                Destroy(sound);
                SceneManager.LoadScene("Levels/MenuPrincipalEnd");
            }
            else SceneManager.LoadScene("Levels/Level" + numberLevel);
        }
    }
}
