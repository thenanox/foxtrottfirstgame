using UnityEngine;
using System.Collections;

public class Controller2D : MonoBehaviour {

    private Rigidbody2D _rigidbody;

    private Vector2 extents;

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
        bool isGoingRight = direction.x > 0;

        float rayDistance = Mathf.Abs(direction.x); ;

        Vector2 rayDir = isGoingRight ? Vector2.right : Vector2.left;

        int layerMask = 1 << LayerMask.NameToLayer("Ground");


        //first raycast
        Vector2 ray = new Vector2(transform.position.x + (extents.x * rayDir.x), transform.position.y + extents.y - 0.05f);

        RaycastHit2D hit = Physics2D.Raycast(ray, rayDir,rayDistance , layerMask);

        if (hit)
        {
            direction.x = hit.point.y - ray.y;
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
        }
        //second raycast
        ray = new Vector2(transform.position.x + (extents.x * rayDir.x), transform.position.y - extents.y + 0.05f);

        hit = Physics2D.Raycast(ray, rayDir, rayDistance, layerMask);

        if (hit)
        {
            direction.y = hit.point.y - ray.y;
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

        float rayDistance = Mathf.Abs(direction.y);

        Vector2 rayDir = isGoingUp ? Vector2.up : Vector2.down;

        int layerMask = 1 << LayerMask.NameToLayer("Ground");


        //first raycast
        Vector2 ray = new Vector2(transform.position.x + extents.x - 0.05f, transform.position.y + (extents.y * rayDir.y));

        RaycastHit2D hit = Physics2D.Raycast(ray, rayDir, rayDistance * Time.fixedDeltaTime, layerMask);

        if (hit)
        {
            direction.y = hit.point.y - ray.y;
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
        //second raycast
        ray = new Vector2(transform.position.x - extents.x + 0.05f, transform.position.y + (extents.y * rayDir.y));

        hit = Physics2D.Raycast(ray, rayDir, rayDistance * Time.fixedDeltaTime, layerMask);

        if (hit)
        {
            direction.y = hit.point.y - ray.y;
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
