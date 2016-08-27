using UnityEngine;
using System.Collections;

public class TransformableTile : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public bool isEnabled()
    {
        return gameObject.layer == LayerMask.NameToLayer("Ground");
    }

    public bool isDisabled()
    {
        return gameObject.layer == LayerMask.NameToLayer("Background");
    }

    public void EnableTile()
    {
        gameObject.layer = LayerMask.NameToLayer("Ground");
        GetComponent<SpriteRenderer>().material.color = Color.white;
    }

    public void DisableTile()
    {
        gameObject.layer = LayerMask.NameToLayer("Background");
        GetComponent<SpriteRenderer>().material.color = Color.grey;
    }
}
