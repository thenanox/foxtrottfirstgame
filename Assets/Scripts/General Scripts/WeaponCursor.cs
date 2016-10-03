using UnityEngine;
using System.Collections;

public class WeaponCursor : MonoBehaviour {

    private float accX = 0.0f;
    private float accY = 0.0f;

    public float maxDistance = 4.0f;
    public float cursorSensitivity = 9.0f;

    public GameObject cursor;

    public float distance
    {
        get
        {
            return maxDistance;
        }
    }

    void Awake() 
    {
        Transform tr = GetComponentInParent<LevelTheatre>().gameObject.transform;
        cursor = (GameObject)Instantiate(cursor, tr);
    }

	void Start()
    {
        InputManager.Instance.registerAxis("MoveCursorX", OnCursorX);
        InputManager.Instance.registerAxis("MoveCursorY", OnCursorY);

        cursor.transform.position = new Vector3(Mathf.Round(transform.position.x) + 1, Mathf.Round(transform.position.y), 0);
    }

    public void OnCursorX(string key, float value)
    {
        if (value != 0f)
        {
            float cursorDistance = cursor.transform.position.x - Mathf.Round(transform.position.x);
            if(value < 0 && accX > 0 || value > 0 && accX < 0)
            {
                accX = value;
            }
            else
            {
                accX += value;
            } 
            if(accX > cursorSensitivity && (cursorDistance) < maxDistance)
            {
                cursor.transform.position += new Vector3(1.0f, 0f, 0f);
                accX = 0;
                accY = 0;
            }
            else if (accX < -cursorSensitivity && (cursorDistance) > -maxDistance && cursor.transform.position.x > 0)
            {
                cursor.transform.position -= new Vector3(1.0f, 0f, 0f);
                accX = 0;
                accY = 0;
            }
        }
    }

    public void OnCursorY(string key, float value)
    {
        if (value != 0f)
        {
            float cursorDistance = cursor.transform.position.y - Mathf.Round(transform.position.y);
            if (value < 0 && accY > 0 || value > 0 && accY < 0)
            {
                accY = value;
            }
            else
            {
                accY += value;
            }
            if (accY > cursorSensitivity && (cursorDistance) < maxDistance)
            {
                cursor.transform.position += new Vector3(0f, 1.0f, 0f);
                accX = 0;
                accY = 0;
            }
            else if (accY < -cursorSensitivity && (cursorDistance) > -maxDistance && cursor.transform.position.y > 0)
            {
                cursor.transform.position -= new Vector3(0f, 1.0f, 0f);
                accX = 0;
                accY = 0;
            }
        }
    }
}
