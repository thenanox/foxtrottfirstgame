using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LaserPoint : MonoBehaviour {

    public GameObject laserBeg;
    public GameObject laserMid;
    public GameObject laserEnd;
    public GameObject laserBegEnd;
    public Transform fireTransform;

    public GameObject[] lasersShot;
	
	void FixedUpdate () {
        Vector2 direction = (fireTransform.transform.position - transform.position).normalized;
        Ray2D ray = new Ray2D(fireTransform.transform.position, direction);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 2000f, LayerMask.GetMask("Foreground", "Box"));
        int hitrounded = Mathf.RoundToInt(hit.distance + 0.2f);
        if (hitrounded != lasersShot.Length)
        {
            for(int i = 0; i < lasersShot.Length; i++)
            {
                Destroy(lasersShot[i]);
            }
            if (hit)
            {
                lasersShot = new GameObject[hitrounded];
                if (hitrounded == 0)
                {
                    return;
                }
                if (hitrounded == 1)
                {
                    lasersShot[0] = Instantiate(laserBegEnd, fireTransform.transform.position, fireTransform.transform.rotation, gameObject.transform) as GameObject;
                }
                else
                {
                    int distance = 2;
                    int laserIndex = 0;
                    lasersShot[laserIndex] = Instantiate(laserBeg, fireTransform.transform.position, fireTransform.transform.rotation, gameObject.transform) as GameObject;
                    laserIndex++;
                    Vector2 lastTilePosition = fireTransform.transform.position;
                    while (distance < hitrounded)
                    {
                        lasersShot[laserIndex] = Instantiate(laserMid, lastTilePosition + direction, fireTransform.transform.rotation, gameObject.transform) as GameObject;
                        lastTilePosition += direction;
                        distance++;
                        laserIndex++;
                    }
                    lasersShot[laserIndex] = Instantiate(laserEnd, lastTilePosition + direction, fireTransform.transform.rotation, gameObject.transform) as GameObject;
                }
            }
        }
    }

    void OnDestroy()
    {
        foreach(GameObject laser in lasersShot){
            Destroy(laser);
        }
    }
}
