using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class Level {
    [SerializeField]
    public char[] tiles;
    [NonSerialized]
    public GameObject[] levelMatrix;
    [SerializeField]
    public string author;
    [SerializeField]
    public string levelDescription;
    [SerializeField]
    public string levelName;
    [SerializeField]
    public int playerX;
    [SerializeField]
    public int playerY;
    [SerializeField]
    public int doorX;
    [SerializeField]
    public int doorY;

    public Level()
    {
        levelMatrix = new GameObject[738];
        tiles = "000000000000000000000000000000000000000000000000000000000000000000000000000000000000111111111111111111111111111111111111100001111111111111111111111111111111111111000011111111111111111111111111111111111110000111111111111111111111111111111111111100001111111111111111111111111111111111111000011111111111111111111111111111111111110000111111111111111111111111111111111111100001111111111111111111111111111111111111000011111111111111111111111111111111111110000111111111111111111111111111111111111100001111111111111111111111111111111111111000011111111111111111111111111111111111110000111111111111111111111111111111111111100001111111111111111111111111111111111111000000000000000000000000000000000000000000000000000000000000000000000000000000000000".ToCharArray();
        author = "Thenanox";
        levelDescription = "Template Level for editor";
        levelName = "TemplateLevel";
        playerX = 2; playerY = 2;
        doorX = 38; playerY = 2;
    }

    public Level(string tiles, string author, string levelDescrition, string levelName)
    {
        levelMatrix = new GameObject[738];
        this.tiles = tiles.ToCharArray();
        this.author = author;
        this.levelDescription = levelDescrition;
        this.levelName = levelName;
    }
}
