using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {

    public AudioSource source;
    public AudioClip hurt;

    private bool alive = true;

    // Use this for initialization
    void Awake () {
        source = GetComponent<AudioSource>();
	}
    
    public void Kill()
    {
        source.PlayOneShot(hurt, 0.4f);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<MovementController>().enabled = false;
        gameObject.GetComponent<WeaponCursor>().enabled = false;
        gameObject.GetComponent<Weapon>().enabled = false;
        alive = false;
        Invoke("reloadScene", 2.0f);
    }

    public bool IsAlive()
    {
        return alive;
    }

    void reloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
