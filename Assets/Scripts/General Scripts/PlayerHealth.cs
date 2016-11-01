using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {

    public AudioSource source;
    public AudioClip hurt;
    public LevelTheatre lt;

    private bool alive = true;

    // Use this for initialization
    void Awake () {
        source = GetComponent<AudioSource>();
        lt = GetComponentInParent<LevelTheatre>();
	}
    
    public void Kill()
    {
        source.PlayOneShot(hurt, 0.4f);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<MovementController>().enabled = false;
        gameObject.GetComponent<WeaponCursor>().Disable();
        gameObject.GetComponent<Weapon>().Disable();
        alive = false;
        StartCoroutine(ReloadLevel());
    }

    IEnumerator ReloadLevel()
    {
        yield return new WaitForSeconds(2);
        lt.LoadData();
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
