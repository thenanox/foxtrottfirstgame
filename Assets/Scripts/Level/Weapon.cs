using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

    private TransformableTile lastTile;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 localPosition = gameObject.transform.position;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 clickPosition = new Vector3(ray.origin.x, ray.origin.y, 0f);
            float distance = Vector3.Distance(localPosition, clickPosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 10f, LayerMask.GetMask("Foreground","Background"));
            if (hit && distance <= 5)
            {
                if(lastTile == hit.transform.gameObject.GetComponent<TransformableTile>())
                {
                    lastTile.transformState();
                }
                else
                {
                    if (lastTile != null)
                    {
                        lastTile.originalState();
                    }
                    if(lastTile != hit.transform.gameObject.GetComponent<TransformableTile>())
                    {
                        lastTile = hit.transform.gameObject.GetComponent<TransformableTile>();
                        lastTile.transformState();
                    }
                }
            }
        }
    }
}
