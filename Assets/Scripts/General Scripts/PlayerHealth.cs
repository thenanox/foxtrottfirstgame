using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {

    public AudioSource source;
    public AudioClip hurt;

    // Use this for initialization
    void Awake () {
        source = GetComponent<AudioSource>();
	}
    
    public void Kill()
    {
        source.PlayOneShot(hurt);

        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        source.PlayOneShot(hurt);

        Invoke("reloadScene", 2.0f);
    }

    void reloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
