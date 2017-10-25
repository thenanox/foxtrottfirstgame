using UnityEngine;
using System.Collections;

public class TransformableTile : MonoBehaviour {

    private int state = 0;
    private Sprite originalSprite;
    public LevelTheatre lt;
    
	void Start () {
	    if(gameObject.layer == LayerMask.NameToLayer("Background"))
        {
            state = 1;
        }
        originalSprite = GetComponent<SpriteRenderer>().sprite;
        lt = GetComponentInParent<LevelTheatre>();
    }

    public void originalState()
    {
        if (state == 1)
        {
            gameObject.layer = LayerMask.NameToLayer("Background");
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Walls/00");
            lt.Transformer(gameObject, '1');
        }
        else
        {
            gameObject.layer = LayerMask.NameToLayer("Foreground");
            lt.Transformer(gameObject, '0');
        }
    }

    public void transformState()
    {
        if(gameObject.layer == LayerMask.NameToLayer("Background"))
        {
            gameObject.layer = LayerMask.NameToLayer("Foreground");
            lt.Transformer(gameObject, '0');
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Walls/00");
            gameObject.layer = LayerMask.NameToLayer("Background");
            lt.Transformer(gameObject, '1');
        }
    }
}
