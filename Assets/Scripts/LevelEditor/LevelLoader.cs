using UnityEngine;
using System.Collections;
using System;

public class LevelLoader : MonoBehaviour {

    public Level level;
    public GameObject Background;
    public GameObject Foreground;

    void Start () {
        level = new Level();
        Build();
        TransformUtils.TransformLevel(level);
    }

    public void Build()
    {
        int width = 0;
        int count = 0;
        for (int i = 0; i < 18; i++)
        {
            for (int j = 0; j < 41; j++)
            {
                if (level.tiles[j + width] == '1')
                {
                    level.levelMatrix[count++] = (GameObject)Instantiate(Background, new Vector2(j, i), Quaternion.identity, gameObject.transform);
                }
                else if (level.tiles[j + width] == '0')
                {
                    level.levelMatrix[count++] = (GameObject)Instantiate(Foreground, new Vector2(j, i), Quaternion.identity, gameObject.transform);
                }
            }
            width += 41;
        }
    }

    public void TransformTile(GameObject tileToTransform, char value)
    {
        for (int i = 0; i < 738; i++)
        {
            if (level.levelMatrix[i] == tileToTransform)
            {
                level.tiles[i] = value;
                TransformUtils.TransformLevel(level);
            }
        }
    }

}
