using UnityEngine;
using System.Collections;
using System;

public class LevelManager : MonoBehaviour {

    public Level level;
    public GameObject BackgroundTile;
    public GameObject ForegroundTile;

    void Start () {
        level = new Level();
        level.tiles = "000000000000000000000000000000000000000000000000000000000000000000000000000000000000111101111111111111111111111111111111100000000011111111111111111111111111111111000011111111100000011111111111111111111110000111111111110011111111111111111111111100001111111111111111111111111111111111111000011111111111101111111111111111111111110000111111111110000001111111111111111111100001111111111011110111111111111111111111000011111111111111111111111111111111111110000111111111111111111111111111111111111100001111111111111100101111111111111111111000011111111111111110111111111111111111110000111111111111111111111111111111111111100001111111111111111111111111111111111111000000000000000000000000000000000000000000000000000000000000000000000000000000000000".ToCharArray();
        level.levelMatrix = new GameObject[738];
        Build(level);
        TransformUtils.TransformLevel(level);
    }

    void Build(Level level)
    {
        int width = 0;
        int count = 0;
        for(int i = 0; i < 18; i++)
        {
            for(int j = 0;j < 41; j++)
            {
                if (level.tiles[j + width] == '1')
                {
                    level.levelMatrix[count++] = (GameObject)Instantiate(BackgroundTile, new Vector2(j, i), Quaternion.identity, gameObject.transform);
                }
                else if (level.tiles[j + width] == '0')
                {
                    level.levelMatrix[count++] = (GameObject)Instantiate(ForegroundTile, new Vector2(j, i), Quaternion.identity, gameObject.transform);
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
