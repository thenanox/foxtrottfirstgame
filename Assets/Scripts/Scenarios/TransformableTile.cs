using UnityEngine;
using System.Collections;

public class TransformableTile : BaseTile {

    private bool backgroundBorn = false;
    private Sprite originalSprite;
    private LevelManager levelManager;
    
	// Use this for initialization
	void Start () {
	    if(gameObject.layer == LayerMask.NameToLayer("Background"))
        {
            backgroundBorn = true;
        }
        originalSprite = GetComponent<SpriteRenderer>().sprite;
        levelManager = gameObject.GetComponentInParent<LevelManager>();
    }

    public void originalState()
    {
        if (backgroundBorn)
        {
            gameObject.layer = LayerMask.NameToLayer("Background");
            levelManager.TransformTile(gameObject, '1');
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Walls/00");
        }
        else
        {
            gameObject.layer = LayerMask.NameToLayer("Foreground");
            levelManager.TransformTile(gameObject, '0');
        }
    }

    public void transformState()
    {
        if(gameObject.layer == LayerMask.NameToLayer("Background"))
        {
            gameObject.layer = LayerMask.NameToLayer("Foreground");
            levelManager.TransformTile(gameObject, '0');
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Walls/00");
            gameObject.layer = LayerMask.NameToLayer("Background");
            levelManager.TransformTile(gameObject, '1');
        }
    }
}
