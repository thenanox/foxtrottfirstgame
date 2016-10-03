using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour {

    public int numberLevel = 1;
    private LevelTheatre lt;

    void Start()
    {
        lt = GetComponentInParent<LevelTheatre>();
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            lt.ChangeLevel(numberLevel);
        }
    }
}
