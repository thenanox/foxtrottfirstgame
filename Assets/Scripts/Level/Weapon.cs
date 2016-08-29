using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

    private TransformableTile lastTile;
    public GameObject cursor;
    public AudioSource audioWrong;

    void Start()
    {
        cursor = GetComponent<WeaponCursor>().cursor;
        audioWrong = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            TransformTile();
        }

        updateCursor();
    }

    void TransformTile()
    {
        Vector3 localPosition = gameObject.transform.position;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 clickPosition = new Vector3(ray.origin.x, ray.origin.y, 0f);
        Debug.DrawRay(ray.origin, ray.direction, Color.yellow, 10f);

        Collider2D hit = Physics2D.OverlapBox(new Vector2(cursor.transform.position.x, cursor.transform.position.y), new Vector2(0.5f, 0.5f), 0.0f, LayerMask.GetMask("Foreground", "Background"));

        //RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 10f, LayerMask.GetMask("Foreground", "Background"));
        if (hit)
        {
            Collider2D hitbox = Physics2D.OverlapBox(new Vector2(cursor.transform.position.x, cursor.transform.position.y), new Vector2(0.5f, 0.5f), 0.0f, LayerMask.GetMask("Box", "Players"));
            if (lastTile == null && hitbox == null)
            {
                lastTile = hit.transform.gameObject.GetComponent<TransformableTile>();
                lastTile.transformState();
            }
            else if (hit.gameObject.layer == LayerMask.NameToLayer("Foreground") && lastTile.gameObject.layer == LayerMask.NameToLayer("Foreground"))
            {
                if (lastTile != hit.transform.gameObject.GetComponent<TransformableTile>())
                {
                    lastTile.originalState();
                    lastTile = hit.transform.gameObject.GetComponent<TransformableTile>();
                    lastTile.transformState();
                }
                else
                {
                    lastTile.originalState();
                    lastTile = null;
                }
            }
            else if (hit.gameObject.layer == LayerMask.NameToLayer("Background") && !hitbox)
            {
                if (lastTile == hit.transform.gameObject.GetComponent<TransformableTile>())
                {
                    lastTile.originalState();
                    lastTile = null;

                }
                else if (lastTile.gameObject.layer == LayerMask.NameToLayer("Background"))
                {
                    Collider2D hitLastileBox = Physics2D.OverlapBox(new Vector2(lastTile.transform.position.x, lastTile.transform.position.y), new Vector2(0.5f, 0.5f), 0.0f, LayerMask.GetMask("Box", "Players"));
                    if (!hitLastileBox)
                    {
                        lastTile.originalState();
                        lastTile = hit.transform.gameObject.GetComponent<TransformableTile>();
                        lastTile.transformState();
                    } else
                    {
                        audioWrong.Play();
                    }
                }
                else
                {
                    lastTile.originalState();
                    lastTile = hit.transform.gameObject.GetComponent<TransformableTile>();
                    lastTile.transformState();
                }
            }
            else if (lastTile.gameObject.layer == LayerMask.NameToLayer("Background"))
            {
                Collider2D hitLastileBox = Physics2D.OverlapBox(new Vector2(lastTile.transform.position.x, lastTile.transform.position.y), new Vector2(0.5f, 0.5f), 0.0f, LayerMask.GetMask("Box", "Players"));
                if (!hitLastileBox)
                {
                    lastTile.originalState();
                    lastTile = hit.transform.gameObject.GetComponent<TransformableTile>();
                    lastTile.transformState();
                } else
                {
                    audioWrong.Play();
                }
            } else
            {
                audioWrong.Play();
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
}
