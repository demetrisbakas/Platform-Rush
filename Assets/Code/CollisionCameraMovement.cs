using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//[RequireComponent(typeof(Controller2D))]

public class CollisionCameraMovement : Controller2D
{
    public LayerMask passengerMask;
    //public Vector3 move;

	void Start () 
    {
        base.Start();
	}
	
	void Update ()
    {
        UpdateRaycastOrigins();

        //Vector3 velocity = move * Time.deltaTime;
        //
        Vector3 velocity;
        velocity.x = Player.GetMoveSpeed() * Time.deltaTime;
        velocity.z = 0;
        velocity.y = 0;
        //

        MovePassengers(velocity);
        //transform.Translate(velocity);
	}

    void MovePassengers(Vector3 velocity)
    {
        HashSet<Transform> movedPassengers = new HashSet<Transform>();

        float directionX = Mathf.Sign(velocity.x);
        float directionY = Mathf.Sign(velocity.y);

        // Horizontally moving platform
        float rayLength = Mathf.Abs(velocity.x) + skinWidth;

        for (int i = 0; i < horizontalRayCount * 15; i++)
        {
            Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
            rayOrigin += Vector2.up * (horizontalRaySpacing * i / 20);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, passengerMask);

            Debug.DrawRay(rayOrigin, Vector2.right * directionX, Color.red, rayLength);

            if (hit)
            {
                if (!movedPassengers.Contains(hit.transform))
                {
                    movedPassengers.Add(hit.transform);
                    float pushX = velocity.x - (hit.distance - skinWidth) * directionX;
                    float pushY;
                    if (collisions.climbingSlope)
                    {
                        pushY = GetClimbVelocityY();
                    }
                    else if (collisions.descendingSlope)
                    {
                        pushY = GetDescendVelocityY();
                    }
                    else
                        pushY = 0;

                    hit.transform.Translate(new Vector3(pushX, pushY));
                }
            }
        }
    }
}
