using UnityEngine;
using System.Collections;
using System;

public class NotTransformableTile : BaseTile {
    public override bool cursorOnTile()
    {
        return false;
    }
}
