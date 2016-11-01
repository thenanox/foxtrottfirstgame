using UnityEngine;
using System.Collections;

public class MovementController : MonoBehaviour
{

    // Ideally the component that controls the input sets
    // the direction on this component or the ai
    public InputManager input;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    // Velocity/Acceleration values
    public float _maxVelocity = 1.0f;
    public float _acceleration = 2f;
    public float _jumpForce = 5.0f;
    protected float _velFriction;

    public float _gravity = 1.0f;

    // I assume for testing purposes
    public bool jumpAllowed = true;

    // Needs to be public?
    public Vector2 _velocity = Vector2.zero;

    public AudioClip contact;
    public AudioSource audioSource;

    // Collisions
    private bool _isTouchingGround = false;
    private bool _isTouchingCeiling = false;

    // Jump
    public float _jumpSpeed = 8.4f;
    public float _jumpMaxTime = 0.8f;
    private bool _canJump = false;

    public Controller2D controller;

    Vector3 inputmovement = Vector3.zero;


    // Use this for initialization
    void Start()
    {
        InputManager.Instance.RegisterAxis("Horizontal", OnInputXAxis);
        InputManager.Instance.RegisterKeyDown("Jump", OnJumpPressed);

        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        // @todo
        // Assign manager by callback. Note that the manager
        // has to implement IMovementCommands interface for this
        // to work as expected.
        input = GetComponent<InputManager>();

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
        
        //PERFORM MOVEMENT
        Controller2D.Flags flags = controller.move(_velocity, Time.fixedDeltaTime);

        if(animator != null)
        {
            animator.SetFloat("velocity", Mathf.Abs(_velocity.x));
            if (_velocity.x > 0)
                spriteRenderer.flipX = false;
            else if (_velocity.x < 0)
                spriteRenderer.flipX = true;
        }

        if (flags.below)
        {
            _velocity.y = 0;
            if(!_isTouchingGround)
                audioSource.PlayOneShot(contact);
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
            if (!_isTouchingCeiling)
                audioSource.PlayOneShot(contact);
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
            animator.SetTrigger("jump");
        }
    }

    public Vector3 getVelocity()
    {
        return _velocity;
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
