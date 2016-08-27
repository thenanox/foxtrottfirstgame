using UnityEngine;
using System.Collections;

public class MovementController : MonoBehaviour
{

    // Ideally the component that controls the input sets
    // the direction on this component or the ai
    public InputManager input;

    // Velocity/Acceleration values
    public float _maxVelocity = 1.0f;
    public float _acceleration = 2f;
    public float _jumpForce = 5.0f;
    private float _velFriction;

    public float _gravity = 1.0f;

    // I assume for testing purposes
    public bool jumpAllowed = true;

    // Needs to be public?
    public Vector2 _velocity;

    // Collisions
    private bool _isTouchingGround = false;
    private bool _isTouchingCeiling = false;

    // Jump
    public float _jumpSpeed = 8.4f;
    public float _jumpMaxTime = 0.8f;
    private bool _canJump = false;
    private bool _jumpPressed = false;
    private Vector3 _externalForce = Vector3.zero;
    public float _externalForceTime = 0.2f;

    private Transform _transform;

    public Controller2D controller;

    Vector3 inputmovement = Vector3.zero;

    [HideInInspector]
    public float _componentInitTimeStamp = 0.0f;

    // Use this for initialization
    void Start()
    {
        InputManager.Instance.registerAxis("Horizontal", OnInputXAxis);
        InputManager.Instance.RegisterKeyDown("jump", OnJumpPressed);
        // @todo
        // Assign manager by callback. Note that the manager
        // has to implement IMovementCommands interface for this
        // to work as expected.
        input = GetComponent<InputManager>();
        _velocity = Vector3.zero;
        _isTouchingGround = true;

        controller = GetComponent<Controller2D>();

        _velFriction = _maxVelocity / (_maxVelocity + (0.5f * _acceleration * Time.fixedDeltaTime * 100));
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (inputmovement != Vector3.zero)
        {
            float horizontalMomentum = inputmovement.x * _acceleration;
            _velocity.x += horizontalMomentum;
        }

        //if needed, perform jump
        jump();
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

        Debug.Log(_velocity);
        
        //PERFORM MOVEMENT
        Controller2D.Flags flags = controller.move(_velocity, Time.fixedDeltaTime);

        if(flags.below)
        {
            _velocity.y = 0;
            _isTouchingGround = true;
        }
        else
        {
            _isTouchingGround = false;
        }

        if (flags.sides)
        {
            _velocity.x = 0;
        }
        if(flags.above)
        {
            _velocity.y = 0;
            _isTouchingCeiling = true;
        }
        else
        {
            _isTouchingCeiling = false;
        }

        Vector2 newPos = transform.position;
        if (oldPos == newPos)
            _velocity = Vector3.zero;
    }

    void jump()
    {
        //comprobamos si tiene una tile encima
        if (_canJump && _isTouchingGround && !_isTouchingCeiling)
        {
            _canJump = false;
            _velocity.y += _jumpForce;
            _isTouchingGround = false;
        }
        

    }

    public Vector3 getVelocity()
    {
        return _velocity;
    }

    public void AddExternalForce(Vector2 force, float maxForce = float.MaxValue)
    {
        if (!_isTouchingGround)
            _velocity.y = 0.0f;

        _velocity += force;

        // Should cap the force rather than the velocity
        if (_velocity.y > maxForce)
            _velocity.y = maxForce;

        _isTouchingGround = false;
    }

    public void AddExternalAcceleration(Vector2 vAcceleration)
    {
        _velocity += vAcceleration * Time.deltaTime;
    }

    public void OnInputXAxis(string axe, float value)
    {
        inputmovement.x = value;

    }

    public void OnJumpPressed(string key)
    {
        if(_isTouchingGround)
            _canJump = true;
    }
}
