using UnityEngine;
using System.Collections;
using System;

public static class TransformUtils {

    public static void TransformLevel(Level level)
    {
        int width = 0;
        for (int i = 0; i < 18; i++)
        {
            for (int j = 0; j < 41; j++)
            {
                if (i > 0 && i < 17 && j > 0 && j < 40) TransformUtils.TransformTile(level, j + width);
            }
            width += 41;
        }
    }

    public static void TransformTile(Level level, int tileSelected)
    {
        if (level.tiles[tileSelected] == '0')
        {
            char[] tiles = new char[8];
            if (level.tiles[tileSelected + 41] == '1')
            {
                tiles[0] = '1';
                tiles[1] = '1';
                tiles[2] = '1';
            }
            else
            {
                tiles[0] = level.tiles[tileSelected + 41 - 1];
                tiles[1] = level.tiles[tileSelected + 41];
                tiles[2] = level.tiles[tileSelected + 41 + 1];
            }
            if (level.tiles[tileSelected - 1] == '1')
            {
                tiles[0] = '1';
                tiles[3] = '1';
                tiles[5] = '1';
            }
            else
            {
                tiles[3] = level.tiles[tileSelected - 1];
                tiles[5] = level.tiles[tileSelected - 41 - 1];
            }
            if (level.tiles[tileSelected + 1] == '1')
            {
                tiles[2] = '1';
                tiles[4] = '1';
                tiles[7] = '1';
            }
            else
            {
                tiles[4] = level.tiles[tileSelected + 1];
                tiles[7] = level.tiles[tileSelected - 41 + 1];
            }
            if (level.tiles[tileSelected - 41] == '1')
            {
                tiles[5] = '1';
                tiles[6] = '1';
                tiles[7] = '1';
            }
            else
            {
                tiles[6] = level.tiles[tileSelected - 41];
            }
            string fileName = Convert.ToInt32(new string(tiles), 2).ToString();
            SpriteRenderer sr = level.levelMatrix[tileSelected].GetComponent<SpriteRenderer>();
            sr.sprite = (Sprite)Resources.Load("Sprites/Walls/" + fileName, typeof(Sprite));
        }
    }
}
