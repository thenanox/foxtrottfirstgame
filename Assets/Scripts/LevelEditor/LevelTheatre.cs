using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;
using System;

public class LevelTheatre : MonoBehaviour
{
    public int level;
    public Elements elements;

    private Level currentLevel;
    private List<GameObject> levelMatrix;
    private List<GameObject> levelElements;

    void Start()
    {
        elements = gameObject.GetComponent<Elements>();
        LoadData();
    }

    public void Build()
    {
        levelMatrix = new List<GameObject>();
        levelElements = new List<GameObject>();
        int width = 0;
        for (int i = 0; i < 18; i++)
        {
            for (int j = 0; j < 41; j++)
            {
                if (currentLevel.tiles[j + width] == 'P')
                {
                    levelElements.Add((GameObject)GameObject.Instantiate(elements.playerPrefab, new Vector2(j, i), Quaternion.identity, this.transform));
                    this.levelMatrix.Add((GameObject)GameObject.Instantiate(elements.backgroundPrefab, new Vector2(j, i), Quaternion.identity, this.transform));
                }
                else if (currentLevel.tiles[j + width] == 'E')
                {
                    GameObject go = (GameObject)GameObject.Instantiate(elements.exitDoorPrefab, new Vector2(j, i), Quaternion.identity, this.transform);
                    go.GetComponent<EndLevel>().numberLevel = level + 1;
                    levelElements.Add(go);
                    this.levelMatrix.Add((GameObject)GameObject.Instantiate(elements.backgroundPrefab, new Vector2(j, i), Quaternion.identity, this.transform));
                }
                if (currentLevel.tiles[j + width] == '1')
                {
                    this.levelMatrix.Add((GameObject)GameObject.Instantiate(elements.backgroundPrefab, new Vector2(j, i), Quaternion.identity, this.transform));
                }
                else if (currentLevel.tiles[j + width] == '0')
                {
                    this.levelMatrix.Add((GameObject)GameObject.Instantiate(elements.foregroundPrefab, new Vector2(j, i), Quaternion.identity, this.transform));
                }
            }
            width += 41;
        }
    }

    public void ChangeLevel(int newLevel)
    {
        level = newLevel;
        LoadData();
    }

    public void SaveData()
    {
        if (!Directory.Exists("Assets/Lvl"))
            Directory.CreateDirectory("Assets/Lvl");

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream saveFile = File.Create("Assets/Lvl/level" + level);

        formatter.Serialize(saveFile, currentLevel);
        saveFile.Close();
    }

    public void LoadData()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream saveFile = File.Open("Assets/Lvl/level" + level, FileMode.Open);

        currentLevel = (Level)formatter.Deserialize(saveFile);
        saveFile.Close();
        if (levelMatrix != null)
        {
            foreach(GameObject go in levelMatrix)
            {
                Destroy(go);
            }
            levelMatrix.Clear();
        }
        if (levelElements != null)
        {
            foreach (GameObject go in levelElements)
            {
                Destroy(go);
            }
            levelElements.Clear();
        }
        levelMatrix = new List<GameObject>();
        levelElements = new List<GameObject>();
        Build();
        Transform();
    }

    public void Transformer(GameObject tileToTransform, char value)
    {
        for (int i = 0; i < 738; i++)
        {
            if (levelMatrix[i] == tileToTransform)
            {
                currentLevel.tiles[i] = value;
                Transform();
            }
        }
    }

    public void Transform()
    {
        int width = 0;
        for (int i = 0; i < 18; i++)
        {
            for (int j = 0; j < 41; j++)
            {
                if (i > 0 && i < 17 && j > 0 && j < 40) TransformTile(j + width);
            }
            width += 41;
        }
    }

    public void TransformTile(int tileSelected)
    {
        if (currentLevel.tiles[tileSelected] == '0')
        {
            char[] tiles = new char[8];
            if (currentLevel.tiles[tileSelected + 41] == '1' || currentLevel.tiles[tileSelected + 41] == 'P' || currentLevel.tiles[tileSelected + 41] == 'E')
            {
                tiles[0] = '1';
                tiles[1] = '1';
                tiles[2] = '1';
            }
            else
            {
                tiles[0] = currentLevel.tiles[tileSelected + 41 - 1];
                tiles[1] = currentLevel.tiles[tileSelected + 41];
                tiles[2] = currentLevel.tiles[tileSelected + 41 + 1];
            }
            if (currentLevel.tiles[tileSelected - 1] == '1' || currentLevel.tiles[tileSelected - 1] == 'P' || currentLevel.tiles[tileSelected - 1] == 'E')
            {
                tiles[0] = '1';
                tiles[3] = '1';
                tiles[5] = '1';
            }
            else
            {
                tiles[3] = currentLevel.tiles[tileSelected - 1];
                tiles[5] = currentLevel.tiles[tileSelected - 41 - 1];
            }
            if (currentLevel.tiles[tileSelected + 1] == '1' || currentLevel.tiles[tileSelected + 1] == 'P' || currentLevel.tiles[tileSelected + 1] == 'E')
            {
                tiles[2] = '1';
                tiles[4] = '1';
                tiles[7] = '1';
            }
            else
            {
                tiles[4] = currentLevel.tiles[tileSelected + 1];
                tiles[7] = currentLevel.tiles[tileSelected - 41 + 1];
            }
            if (currentLevel.tiles[tileSelected - 41] == '1' || currentLevel.tiles[tileSelected - 41] == 'P' || currentLevel.tiles[tileSelected - 41] == 'E')
            {
                tiles[5] = '1';
                tiles[6] = '1';
                tiles[7] = '1';
            }
            else
            {
                tiles[6] = currentLevel.tiles[tileSelected - 41];
            }
            for(int i = 0; i < 8;i++) {
                if (tiles[i] == 'P' || tiles[i] == 'E')
                {
                    tiles[i] = '1';
                }
            }
            string fileName = Convert.ToInt32(new string(tiles), 2).ToString();
            SpriteRenderer sr = this.levelMatrix[tileSelected].GetComponent<SpriteRenderer>();
            sr.sprite = (Sprite)Resources.Load("Sprites/Walls/" + fileName, typeof(Sprite));
        }
    }
}