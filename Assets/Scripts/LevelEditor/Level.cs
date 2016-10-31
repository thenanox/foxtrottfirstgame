using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class Level {
    [SerializeField]
    public char[] tiles;
    [SerializeField]
    public char[] elements;
    [SerializeField]
    public string author;
    [SerializeField]
    public string levelDescription;
    [SerializeField]
    public string levelName;

    public Level()
    {
        tiles = "000000000000000000000000000000000000000000000000000000000000000000000000000000000000111111111111111111111111111111111111100001111111111111111111111111111111111111000011111111111111111111111111111111111110000111111111111111111111111111111111111100001111111111111111111111111111111111111000011111111111111111111111111111111111110000111111111111111111111111111111111111100001111111111111111111111111111111111111000011111111111111111111111111111111111110000111111111111111111111111111111111111100001111111111111111111111111111111111111000011111111111111111111111111111111111110000111111111111111111111111111111111111100001111111111111111111111111111111111111000000000000000000000000000000000000000000000000000000000000000000000000000000000000".ToCharArray();
        elements = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxPxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxExxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx".ToCharArray();
        author = "Thenanox";
        levelDescription = "Template Level for editor";
        levelName = "TemplateLevel";
    }

    public Level(string tiles, string elements, string author, string levelDescrition, string levelName)
    {
        this.tiles = tiles.ToCharArray();
        this.elements = elements.ToCharArray();
        this.author = author;
        this.levelDescription = levelDescrition;
        this.levelName = levelName;
    }
}
