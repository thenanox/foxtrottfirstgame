using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class Weapon : MonoBehaviour {

    private TransformableTile lastTile;
    public GameObject cursor;
    public AudioSource audiosource;
    public AudioClip wrongSound;

    private bool activated = false;

    void Start()
    {
        cursor = GetComponent<WeaponCursor>().cursor;
        audiosource = GetComponent<AudioSource>();
        InputManager.Instance.RegisterKeyDown("Exit", Exit);
        InputManager.Instance.registerAxis("Activate", Activate);
    }

    void Update()
    {
        updateCursor();
    }

    void TransformTile()
    {
        Collider2D hit = Physics2D.OverlapBox(new Vector2(cursor.transform.position.x, cursor.transform.position.y), new Vector2(0.5f, 0.5f), 0.0f, LayerMask.GetMask("Foreground", "Background"));
        Collider2D hitbox = Physics2D.OverlapBox(new Vector2(cursor.transform.position.x, cursor.transform.position.y), new Vector2(0.5f, 0.5f), 0.0f, LayerMask.GetMask("Box", "Players"));
        if(hit == null)
        {
            return;
        }
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
                    ParticleSystem parti = hitLastileBox.gameObject.GetComponent<ParticleSystem>();
                    if(parti != null)
                    {
                        parti.Play();
                    }
                }
            }
        }
        else
        {
            audiosource.PlayOneShot(wrongSound);
            if (hitbox)
            {
                ParticleSystem parti = hitbox.gameObject.GetComponent<ParticleSystem>();
                if (parti != null)
                {
                    parti.Play();
                }
            }
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
    
    void Exit(string key)
    {
        SceneManager.LoadScene("Levels/MenuPrincipal");
        GameObject sound = GameObject.Find("MusicManager(Clone)");
        Destroy(sound);
    }

    void Activate(string axe, float value)
    {
        if(value < -0.4f && !activated)
        {
            activated = true;
            TransformTile();
        } else if(value > -0.4f)
        {
            activated = false;
        }
    }
}
