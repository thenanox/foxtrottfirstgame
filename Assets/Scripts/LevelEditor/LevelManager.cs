using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;
using System;

public class LevelManager : MonoBehaviour
{
    public InputField number;
    public Elements elements;

    private Level currentLevel;
    private List<GameObject> levelMatrix;
    private List<GameObject> levelElements;

    void Start()
    {
        elements = gameObject.GetComponent<Elements>();
        currentLevel = new Level();
        Build();
        Transform();
    }

    public void AddPart(GameObject part, Vector3 location)
    {
        RemovePart(location);
        PlaceObject(part, location);
    }
    
    public void RemovePart(Vector3 location)
    {
        var count = 0;
        for (int i = 0; i < levelMatrix.Count; i++)
        {
            GameObject part = levelMatrix[i];
            if (part != null && part.gameObject.transform.position == location)
            {
                Debug.Log("Part Location: " + part.gameObject.transform.position + ", location:" + location);
                currentLevel.tiles[i] = 'x';
                Destroy(part);
                continue;
            }
            count++;
        }
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
                    levelElements.Add((GameObject)GameObject.Instantiate(elements.exitDoorPrefab, new Vector2(j, i), Quaternion.identity, this.transform));
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

    private void PlaceObject(GameObject part, Vector3 location)
    {
        int currentPosition = (int)(location.x + location.y * 41f);
        if (part == elements.playerPrefab)
        {
            foreach (GameObject target in GameObject.FindGameObjectsWithTag("Player"))
            {
                GameObject.Destroy(target);
            }
            for(int i = 0; i < currentLevel.tiles.Length; i++)
            {
                if(currentLevel.tiles[i] == 'P')
                {
                    currentLevel.tiles[i] = '1';
                }
            }
            currentLevel.tiles[currentPosition] = 'P';
            levelElements.Add((GameObject)GameObject.Instantiate(elements.playerPrefab, location, Quaternion.identity, GameObject.Find("LevelEditor").transform));
            levelMatrix[currentPosition] = ((GameObject)GameObject.Instantiate(elements.backgroundPrefab, location, Quaternion.identity, GameObject.Find("LevelEditor").transform));
        }
        else if (part == elements.exitDoorPrefab)
        {
            foreach (GameObject target in GameObject.FindGameObjectsWithTag("Exit"))
            {
                GameObject.Destroy(target);
            }
            for (int i = 0; i < currentLevel.tiles.Length; i++)
            {
                if (currentLevel.tiles[i] == 'E')
                {
                    currentLevel.tiles[i] = '1';
                }
            }
            currentLevel.tiles[currentPosition] = 'E';
            levelElements.Add((GameObject)GameObject.Instantiate(elements.exitDoorPrefab, location, Quaternion.identity, GameObject.Find("LevelEditor").transform));
            levelMatrix[currentPosition] = ((GameObject)GameObject.Instantiate(elements.backgroundPrefab, location, Quaternion.identity, GameObject.Find("LevelEditor").transform));
        }
        else if (part == elements.laserPrefab1)
        {
            currentLevel.tiles[currentPosition] = '3';
            levelMatrix[currentPosition] = ((GameObject)GameObject.Instantiate(elements.laserPrefab1, location, Quaternion.identity, GameObject.Find("LevelEditor").transform));
        }
        else if (part == elements.laserPrefab2)
        {
            currentLevel.tiles[currentPosition] = '4';
            levelMatrix[currentPosition] = ((GameObject)GameObject.Instantiate(elements.laserPrefab2, location, Quaternion.Euler(new Vector3(0, 0, 90)), GameObject.Find("LevelEditor").transform));
        }
        else if (part == elements.laserPrefab3)
        {
            currentLevel.tiles[currentPosition] = '5';
            levelMatrix[currentPosition] = ((GameObject)GameObject.Instantiate(elements.laserPrefab3, location, Quaternion.Euler(new Vector3(0, 0, 180)), GameObject.Find("LevelEditor").transform));
        }
        else if (part == elements.laserPrefab4)
        {
            currentLevel.tiles[currentPosition] = '6';
            levelMatrix[currentPosition] = ((GameObject)GameObject.Instantiate(elements.laserPrefab4, location, Quaternion.Euler(new Vector3(0, 0, 270)), GameObject.Find("LevelEditor").transform));
        }
        else if (part == elements.boxPrefab)
        {
            currentLevel.tiles[currentPosition] = 'B';
            levelMatrix[currentPosition] = ((GameObject)GameObject.Instantiate(elements.boxPrefab, location, Quaternion.identity, GameObject.Find("LevelEditor").transform));
        }
        else if (part == elements.backgroundPrefab)
        {
            currentLevel.tiles[currentPosition] = '1';
            levelMatrix[currentPosition] = ((GameObject)GameObject.Instantiate(elements.backgroundPrefab, location, Quaternion.identity, GameObject.Find("LevelEditor").transform));
        }
        else if (part == elements.foregroundPrefab)
        {
            currentLevel.tiles[currentPosition] = '0';
            levelMatrix[currentPosition] = ((GameObject)GameObject.Instantiate(elements.foregroundPrefab, location, Quaternion.identity, GameObject.Find("LevelEditor").transform));
        }
        Transform();
    }

    public void SaveData()
    {
        if (!Directory.Exists("Assets/Lvl"))
            Directory.CreateDirectory("Assets/Lvl");

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream saveFile = File.Create("Assets/Lvl/level" + number.text);

        formatter.Serialize(saveFile, currentLevel);
        saveFile.Close();
    }

    public void LoadData()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream saveFile = File.Open("Assets/Lvl/level" + number.text, FileMode.Open);

        currentLevel = (Level)formatter.Deserialize(saveFile);
        saveFile.Close();
        if (levelMatrix != null)
        {
            foreach (GameObject go in levelMatrix)
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
            if (currentLevel.tiles[tileSelected + 41] != '0')
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
            if (currentLevel.tiles[tileSelected - 1] != '0')
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
            if (currentLevel.tiles[tileSelected + 1] != '0')
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
            if (currentLevel.tiles[tileSelected - 41] != '0')
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