using UnityEngine;
using System.Collections;

public class BoxMovementController : MonoBehaviour {
    // Components
    private SpriteRenderer spriteRenderer;

    // Velocity/Acceleration values
    public float _maxVelocity = 1.0f;
    public float _acceleration = 2f;
    public float _jumpForce = 5.0f;
    protected float _velFriction;
    public float _gravity = 1.0f;

    // Needs to be public?
    public Vector2 _velocity = Vector2.zero;

    // Collisions
    private bool _isTouchingGround = false;
    private bool _isTouchingCeiling = false;

    private Transform _transform;
    public Controller2D controller;

    // Use this for initialization
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        controller = GetComponent<Controller2D>();

        _velFriction = _maxVelocity / (_maxVelocity + (0.5f * _acceleration * Time.fixedDeltaTime * 100));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _velocity.x *= _velFriction;

        if (_velocity.sqrMagnitude < float.Epsilon)
        {
            _velocity = Vector3.zero;
        }

        _velocity.y -= _gravity * Time.fixedDeltaTime;


        // Vertical cap
        if (_velocity.y > 14.0f)
            _velocity.y = 14.0f;
        if (_velocity.y < -14.0f)
            _velocity.y = -14.0f;

        Vector2 oldPos = transform.position;

        //PERFORM MOVEMENT
        Controller2D.Flags flags = controller.move(_velocity, Time.fixedDeltaTime);
        if (flags.below)
        {
            _velocity.y = 0;
        }

        if (flags.sides)
        {
            _velocity.x = 0;

        }
        if (flags.above)
        {
            _velocity.y = 0;
        }

        Vector2 newPos = transform.position;
        if (oldPos == newPos)
            _velocity = Vector3.zero;
    }

    public void AddExternalForce(Vector2 force, float maxForce = float.MaxValue)
    {
        if (!_isTouchingGround)
            _velocity.y = 0.0f;

        _velocity += force;

        // Should cap the force rather than the velocity
        if (_velocity.y > maxForce)
            _velocity.y = maxForce;
    }
}

