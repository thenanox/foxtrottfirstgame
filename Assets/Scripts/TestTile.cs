using UnityEngine;
using System.Collections;

public class TestTile : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        bool click = Input.GetMouseButtonDown(0);
        if (click)
        {
            Rigidbody2D rb = gameObject.AddComponent<Rigidbody2D>();
            BoxCollider2D box = gameObject.AddComponent<BoxCollider2D>();
            rb.isKinematic = true;
        }
    }
}
