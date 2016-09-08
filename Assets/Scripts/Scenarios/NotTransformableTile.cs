using UnityEngine;
using System.Collections;
using System;

public class NotTransformableTile : BaseTile {

    void Start()
    {
        GetComponent<SpriteRenderer>().material.color = Color.magenta;
    }
}
