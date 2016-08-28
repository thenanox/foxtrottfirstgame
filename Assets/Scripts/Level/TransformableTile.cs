using UnityEngine;
using System.Collections;

public class TransformableTile : BaseTile {

    private bool backgroundBorn = false;
    private Animator animator;

	// Use this for initialization
	void Start () {
	    if(gameObject.layer == LayerMask.NameToLayer("Background"))
        {
            GetComponent<SpriteRenderer>().material.color = Color.grey;
            backgroundBorn = true;
        }
        animator = GetComponent<Animator>();
    }

    public override bool cursorOnTile()
    {
        animator.SetTrigger("Shake");
        return true;
    }

    public void originalState()
    {
        if (backgroundBorn)
        {
            gameObject.layer = LayerMask.NameToLayer("Background");
            GetComponent<SpriteRenderer>().material.color = Color.grey;
        }
        else
        {
            gameObject.layer = LayerMask.NameToLayer("Foreground");
            GetComponent<SpriteRenderer>().material.color = Color.white;
        }
    }

    public void transformState()
    {
        if(gameObject.layer == LayerMask.NameToLayer("Background"))
        {
            gameObject.layer = LayerMask.NameToLayer("Foreground");
            GetComponent<SpriteRenderer>().material.color = Color.white;
        }
        else
        {
            gameObject.layer = LayerMask.NameToLayer("Background");
            GetComponent<SpriteRenderer>().material.color = Color.grey;
        }
    }
}
