using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;

public class LevelMgr : MonoBehaviour
{
    public Level currentLevel;

    public GameObject Background;
    public GameObject Foreground;

    public InputField number;

    void Start()
    {
        currentLevel = new Level();
        Build();
    }

    public void Build()
    {
        int width = 0;
        int count = 0;
        for (int i = 0; i < 18; i++)
        {
            for (int j = 0; j < 41; j++)
            {
                if (currentLevel.tiles[j + width] == '1')
                {
                    currentLevel.levelMatrix[count++] = (GameObject)Instantiate(Background, new Vector2(j, i), Quaternion.identity, gameObject.transform);
                }
                else if (currentLevel.tiles[j + width] == '0')
                {
                    currentLevel.levelMatrix[count++] = (GameObject)Instantiate(Foreground, new Vector2(j, i), Quaternion.identity, gameObject.transform);
                }
            }
            width += 41;
        }
    }

    public void AddPart(GameObject part, Vector3 location, bool isSpawnPart)
    {
        RemovePart(location);
        PlaceObject(part, location);
    }
    
    public void RemovePart(Vector3 location)
    {
        var count = 0;
        for (int i = 0; i < currentLevel.levelMatrix.Length; i++)
        {
            GameObject part = currentLevel.levelMatrix[i];
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

    private void PlaceObject(GameObject part, Vector3 location)
    {
        int currentPosition = (int)(location.x + location.y * 41f);
        if(part.layer == LayerMask.NameToLayer("Background"))
        {
            currentLevel.tiles[currentPosition] = '1';
        } else
        {
            currentLevel.tiles[currentPosition] = '0';
        }
        currentLevel.levelMatrix[currentPosition] = (GameObject)Instantiate(part, location, Quaternion.identity, gameObject.transform);
    }

    public void SaveData()
    {
        if (!Directory.Exists("Assets/Saves"))
            Directory.CreateDirectory("Assets/Saves");

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream saveFile = File.Create("Assets/Saves/level" + number.text);

        formatter.Serialize(saveFile, currentLevel);

        saveFile.Close();
    }

    public void LoadData()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream saveFile = File.Open("Assets/Saves/level" + number.text, FileMode.Open);

        currentLevel = (Level)formatter.Deserialize(saveFile);
        currentLevel.levelMatrix = new GameObject[738];
        saveFile.Close();

        Build();
    }
}