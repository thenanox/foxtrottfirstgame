using UnityEngine;
using System.Collections;

public class BoxMovementController : MovementController {

	// Use this for initialization
	void Start () {
        controller = GetComponent<Controller2D>();

        _velFriction = _maxVelocity / (_maxVelocity + (0.5f * _acceleration * Time.fixedDeltaTime * 100));
    }
}
