using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
    
    public void Kill()
    {
        // Turn the tank off.
        gameObject.SetActive(false);
    }

}
