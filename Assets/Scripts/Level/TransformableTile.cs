using UnityEngine;
using System.Collections;

public class TransformableTile : BaseTile {

    private bool backgroundBorn = false;
    private bool transformed = false;
    public Animator animator;


	// Use this for initialization
	void Start () {
	    if(gameObject.layer == LayerMask.NameToLayer("Background"))
        {
            GetComponent<SpriteRenderer>().material.color = Color.grey;
            backgroundBorn = true;
        }
        animator = GetComponent<Animator>();
    }

    public void originalState()
    {
        if (backgroundBorn)
        {
            gameObject.layer = LayerMask.NameToLayer("Background");
            GetComponent<SpriteRenderer>().material.color = Color.grey;
            animator.SetTrigger("Unshake");
        }
        else
        {
            gameObject.layer = LayerMask.NameToLayer("Foreground");
            GetComponent<SpriteRenderer>().material.color = Color.white;
            animator.SetTrigger("Unshake");
        }
    }

    public void transformState()
    {
        if(gameObject.layer == LayerMask.NameToLayer("Background"))
        {
            gameObject.layer = LayerMask.NameToLayer("Foreground");
            GetComponent<SpriteRenderer>().material.color = Color.white;
            animator.SetTrigger("Shake");
        }
        else
        {
            gameObject.layer = LayerMask.NameToLayer("Background");
            GetComponent<SpriteRenderer>().material.color = Color.grey;
            animator.SetTrigger("Shake");
        }
    }
}
