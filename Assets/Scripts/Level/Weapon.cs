using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class Weapon : MonoBehaviour {

    private TransformableTile lastTile;
    public GameObject cursor;
    public AudioSource audiosource;
    public AudioClip wrongSound;

    void Start()
    {
        cursor = GetComponent<WeaponCursor>().cursor;
        audiosource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Levels/MenuPrincipal");
            GameObject sound = GameObject.Find("MusicManager(Clone)");
            Destroy(sound);
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            TransformTile();
        }

        updateCursor();
    }

    void TransformTile()
    {
        Collider2D hit = Physics2D.OverlapBox(new Vector2(cursor.transform.position.x, cursor.transform.position.y), new Vector2(0.5f, 0.5f), 0.0f, LayerMask.GetMask("Foreground", "Background"));
        Collider2D hitbox = Physics2D.OverlapBox(new Vector2(cursor.transform.position.x, cursor.transform.position.y), new Vector2(0.5f, 0.5f), 0.0f, LayerMask.GetMask("Box", "Players"));
        TransformableTile transTile = hit.transform.gameObject.GetComponent<TransformableTile>();
        if (hit && hitbox == null && transTile != null)
        {
            if (lastTile == null)
            {
                lastTile = transTile;
                lastTile.transformState();
            }
            else if (lastTile == transTile)
            {
                lastTile.originalState();
                lastTile = null;
            }
            else
            {
                Collider2D hitLastileBox = Physics2D.OverlapBox(new Vector2(lastTile.transform.position.x, lastTile.transform.position.y), new Vector2(0.5f, 0.5f), 0.0f, LayerMask.GetMask("Box", "Players"));
                if (hitLastileBox == null)
                {
                    lastTile.originalState();
                    lastTile = transTile;
                    lastTile.transformState();
                }
                else
                {
                    audiosource.PlayOneShot(wrongSound);
                }
            }
        }
        else
        {
            audiosource.PlayOneShot(wrongSound);
        }
    }

    public void updateCursor()
    {

        float asd = Mathf.Abs(cursor.transform.position.x - Mathf.Round(transform.position.x));
        while (Mathf.Abs(cursor.transform.position.x - Mathf.Round(transform.position.x)) > GetComponent<WeaponCursor>().distance)
        {
            if (cursor.transform.position.x - Mathf.Round(transform.position.x) < 0)
            {
                cursor.transform.Translate(Vector3.right);
            }
            else
            {
                cursor.transform.Translate(Vector3.left);
            }
            
        }

        while (Mathf.Abs(cursor.transform.position.y - Mathf.Round(transform.position.y)) > GetComponent<WeaponCursor>().distance)
        {
            if (cursor.transform.position.y - Mathf.Round(transform.position.y) < 0)
            {
                cursor.transform.Translate(Vector3.up);
            }
            else
            {
                cursor.transform.Translate(Vector3.down);
            }
        }
    }
}
