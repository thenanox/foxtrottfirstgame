using UnityEngine;
using System.Collections;

public class Controller2D : MonoBehaviour {

    private Rigidbody2D _rigidbody;

    private Vector2 extents;

    public Vector2 pushForce = new Vector2(3f, 0f);

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        Vector3 ext = GetComponent<BoxCollider2D>().bounds.extents;
        extents = new Vector2(ext.x, ext.y);
    }

    public class Flags
    {
        public bool above = false;
        public bool below = false;
        public bool sides = false;
    }
    
    public Flags move(Vector2 direction, float delta)
    {
        Flags result = new Flags();

        moveHorizontally(ref direction, ref result);
        moveVertically(ref direction, ref result);

        transform.Translate(direction * delta);

        return result;
    }

    public void moveHorizontally(ref Vector2 direction, ref Flags result)
    {
        if (direction.x < float.Epsilon && direction.x > float.Epsilon )
            return;

        bool isGoingRight = direction.x > 0;

        float rayDistance = Mathf.Abs(direction.x); ;

        Vector2 rayDir = isGoingRight ? Vector2.right : Vector2.left;

        //first raycast
        Vector2 ray = new Vector2(transform.position.x + (extents.x * rayDir.x), transform.position.y + extents.y - 0.05f);
        RaycastHit2D hit;
        if (gameObject.layer != LayerMask.NameToLayer("Box"))
        {
            hit = Physics2D.Raycast(ray, rayDir, rayDistance * Time.fixedDeltaTime, LayerMask.GetMask("Foreground", "Box"));
        }
        else
            hit = Physics2D.Raycast(ray, rayDir, rayDistance * Time.fixedDeltaTime, LayerMask.GetMask("Foreground"));

        Debug.DrawRay(ray, rayDir * rayDistance * Time.fixedDeltaTime, Color.red, 0.1f);
        if (hit)
        {
            direction.x = (hit.point.x - ray.x) / Time.deltaTime;
            rayDistance = Mathf.Abs(direction.y);
            result.sides = true;
            if (isGoingRight)
            {
                direction.x += extents.x;
            }
            else
            {
                direction.x -= extents.x;
            }


            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Box"))
            {
                hit.rigidbody.GetComponent<BoxMovementController>().AddExternalForce(pushForce);
            }
        }
        //second raycast
        ray = new Vector2(transform.position.x + (extents.x * rayDir.x), transform.position.y - extents.y + 0.05f);

        //Debug.DrawRay(ray, rayDir * rayDistance * Time.fixedDeltaTime, Color.red, 5.0f);

        
        if (gameObject.layer != LayerMask.NameToLayer("Box"))
        {
            hit = Physics2D.Raycast(ray, rayDir, rayDistance * Time.fixedDeltaTime, LayerMask.GetMask("Foreground", "Box"));
        }
        else
            hit = Physics2D.Raycast(ray, rayDir, rayDistance * Time.fixedDeltaTime, LayerMask.GetMask("Foreground"));

        Debug.DrawRay(ray, rayDir * rayDistance * Time.fixedDeltaTime, Color.red, 0.1f);
        if (hit)
        {
            direction.x = (hit.point.x - ray.x) / Time.deltaTime;
            rayDistance = Mathf.Abs(direction.y);
            result.sides = true;
            if (isGoingRight)
            {
                direction.x -= extents.x;
            }
            else
            {
                direction.x += extents.x;
            }
        }
    }

    public void moveVertically(ref Vector2 direction, ref Flags result)
    {
        bool isGoingUp = direction.y > 0;

        float rayDistance = Mathf.Abs(direction.y) * Time.fixedDeltaTime;

        Vector2 rayDir = isGoingUp ? Vector2.up : Vector2.down;

        int layerMask = 1 << LayerMask.NameToLayer("Ground") | 1 << LayerMask.NameToLayer("Box");
        //first raycast
        Vector2 ray = new Vector2(transform.position.x + extents.x - 0.05f, transform.position.y + (extents.y * rayDir.y));

        RaycastHit2D hit;
        if (gameObject.layer != LayerMask.NameToLayer("Box"))
        {
            hit = Physics2D.Raycast(ray, rayDir, rayDistance, LayerMask.GetMask("Foreground", "Box"));
        }
        else
            hit = Physics2D.Raycast(ray, rayDir, rayDistance, LayerMask.GetMask("Foreground"));

        Debug.DrawRay(ray, rayDir * rayDistance , Color.red, 0.1f);

        if (hit)
        {
            direction.y = (hit.point.y - ray.y)/Time.deltaTime;
            rayDistance = Mathf.Abs(direction.y) * Time.fixedDeltaTime;

            // remember to remove the skinWidth from our deltaMovement
            if (isGoingUp)
            {
                result.above = true;
            }
            else
            {
                result.below = true;
            }
        }
        //second raycast
        ray = new Vector2(transform.position.x - extents.x + 0.05f, transform.position.y + (extents.y * rayDir.y));

        if (gameObject.layer != LayerMask.NameToLayer("Box"))
        {
            hit = Physics2D.Raycast(ray, rayDir, rayDistance, LayerMask.GetMask("Foreground", "Box"));
        }
        else
            hit = Physics2D.Raycast(ray, rayDir, rayDistance, LayerMask.GetMask("Foreground"));

        if (hit)
        {
            direction.y = (hit.point.y - ray.y) / Time.deltaTime;
            rayDistance = Mathf.Abs(direction.y);

            // remember to remove the skinWidth from our deltaMovement
            if (isGoingUp)
            {
                result.above = true;
            }
            else
            {
                result.below = true;
            }
        }
    }
}
