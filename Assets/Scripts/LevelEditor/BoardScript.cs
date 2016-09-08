using UnityEngine;
using System.Collections;

public class BoardScript : MonoBehaviour {

    public GameObject WallBackground;
    public GameObject WallCorner;
    public GameObject Wall;

    public int startFilesOffset = 0;
    public int endFilesOffset = 17;
    public int startColumnsOffset = 0;
    public int endColumnsOffset = 30;

    void Start () {
        FillForeground();
        FillBackground();
	}

    void FillForeground()
    {
        Instantiate(WallCorner, new Vector2(startColumnsOffset, startFilesOffset), transform.rotation, transform);
        for (int i = startColumnsOffset + 1; i < endColumnsOffset; i++)
        {
            Instantiate(Wall, new Vector2(i, startFilesOffset), transform.rotation, transform);
        }
        Instantiate(WallCorner, new Vector2(endColumnsOffset, startFilesOffset), Quaternion.Euler(0f,0f,90f), transform);
        for (int i = startFilesOffset + 1; i < endFilesOffset; i++)
        {
            Instantiate(Wall, new Vector2(endColumnsOffset, i), Quaternion.Euler(0f, 0f, 90f), transform);
        }
        Instantiate(WallCorner, new Vector2(endColumnsOffset, endFilesOffset), Quaternion.Euler(0f, 0f, 180f), transform);
        for (int i = endColumnsOffset - 1; i > startColumnsOffset; i--)
        {
            Instantiate(Wall, new Vector2(i, endFilesOffset), Quaternion.Euler(0f, 0f, 180f), transform);
        }
        Instantiate(WallCorner, new Vector2(startColumnsOffset, endFilesOffset), Quaternion.Euler(0f, 0f, 270f), transform);
        for (int i = endFilesOffset - 1; i > startFilesOffset; i--)
        {
            Instantiate(Wall, new Vector2(startColumnsOffset, i), Quaternion.Euler(0f, 0f, 270f), transform);
        }
    }

    void FillBackground()
    {
        for (int i = startColumnsOffset + 1; i < endColumnsOffset; i++)
        {
            for (int j = startFilesOffset + 1; j < endFilesOffset; j++)
            {
                Instantiate(WallBackground, new Vector2(i, j), transform.rotation, transform);
            }
        }
    }
}
