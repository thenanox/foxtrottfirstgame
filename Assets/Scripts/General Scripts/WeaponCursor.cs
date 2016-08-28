using UnityEngine;
using System.Collections;

public class WeaponCursor : MonoBehaviour {

    private bool xPressed = false;
    private bool yPressed = false;

    public float maxDistance = 5.0f;

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
        cursor = GameObject.Instantiate(cursor);
    }

	void Start()
    {
        InputManager.Instance.registerAxis("MoveCursorX", OnCursorX);
        InputManager.Instance.registerAxis("MoveCursorY", OnCursorY);

        InputManager.Instance.RegisterKeyUp("MoveCursorX", releaseX);
        InputManager.Instance.RegisterKeyUp("MoveCursorY", releaseY);

        cursor.transform.position = new Vector3(Mathf.Round(transform.position.x) + 1, Mathf.Round(transform.position.y), 0);
    }

    public void OnCursorX(string key, float value)
    {
        if (!xPressed && value != 0f)
        {
            float cursorDistance = cursor.transform.position.x - Mathf.Round(transform.position.x);

            xPressed = true;
            if(value > 0 && (cursorDistance) < maxDistance)
            {
                cursor.transform.position += new Vector3(1.0f, 0f, 0f);
            }
            else if (value < 0 && (cursorDistance) > -maxDistance && cursor.transform.position.x > 0)
            {
                cursor.transform.position -= new Vector3(1.0f, 0f, 0f);
            }
        }
    }

    public void OnCursorY(string key, float value)
    {
        if (!yPressed && value != 0f)
        {
            float cursorDistance = cursor.transform.position.y - Mathf.Round(transform.position.y);

            yPressed = true;
            if (value > 0 && (cursorDistance) < maxDistance)
            {
                cursor.transform.position += new Vector3(0f, 1.0f, 0f);
            }
            else if (value < 0 && (cursorDistance) > -maxDistance && cursor.transform.position.y > 0)
            {
                cursor.transform.position -= new Vector3(0f, 1.0f, 0f);
            }
        }
    }

    public void releaseX(string key)
    {
        xPressed = false;
    }

    public void releaseY(string key)
    {
        yPressed = false;
    }

    private void paintSprite()
    {

    }

}
