using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour {

    public int numberLevel = 1;

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            if(numberLevel == 6) SceneManager.LoadScene("Standard assets/Scenes/MenuPrincipalEnd");
            else SceneManager.LoadScene("Levels/Level" + numberLevel);
        }
    }
}
