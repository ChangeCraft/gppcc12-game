using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMove : MonoBehaviour
{

    [SerializeField]
    private float speed = 0.4f;

    [SerializeField]
    private LayerMask wallLayerName;

    private bool isMovable;
    private bool isControlled;

    private int stepSize = 2;

    Vector2 dest = Vector2.zero;
    Vector2 currentDirection = Vector2.zero;
    Vector2 desiredDirection = Vector2.zero;

    void Start()
    {
        isMovable = false;
        isControlled = false;

        dest = transform.position;
    }

    private void Update()
    {
        if(isMovable)
        {
            if (isControlled)
                SetDesiredDirection();

            //AnimationParameters
            GetComponent<Animator>().SetFloat("DirX", currentDirection.x);
            GetComponent<Animator>().SetFloat("DirY", currentDirection.y);
        }
    }

    void FixedUpdate()
    {
        if ((Vector2)transform.position == dest)
        {
            //If the ghost is not controlled by the player set a random 
            //direction when a new one is required (if old destination is reached)
            if (!isControlled && isMovable)
                SetRndDesiredDirection();

            //ghosts are not allowed to turn 180°
            if (desiredDirection != -currentDirection)
            {
                //If the desired direction is up and there is no wall
                //set the current direction to up
                if (desiredDirection == Vector2.up && Valid(Vector2.up))
                    currentDirection = desiredDirection;

                //same as above but right
                if (desiredDirection == Vector2.right && Valid(Vector2.right))
                    currentDirection = desiredDirection;

                //same as above but down
                if (desiredDirection == Vector2.down && Valid(-Vector2.up))
                    currentDirection = desiredDirection;

                //same as above but left
                if (desiredDirection == Vector2.left && Valid(-Vector2.right))
                    currentDirection = desiredDirection;
            }

            //If the curren direction is up and there is no wall
            //set the destination to up
            if (currentDirection == Vector2.up && Valid(Vector2.up))
                dest = (Vector2)transform.position + Vector2.up * stepSize;

            //same as above but right
            if (currentDirection == Vector2.right && Valid(Vector2.right))
                dest = (Vector2)transform.position + Vector2.right * stepSize;

            //same as above but down
            if (currentDirection == Vector2.down && Valid(-Vector2.up))
                dest = (Vector2)transform.position - Vector2.up * stepSize;

            //same as above but left
            if (currentDirection == Vector2.left && Valid(-Vector2.right))
                dest = (Vector2)transform.position - Vector2.right * stepSize;

        }

        Vector2 p = Vector2.MoveTowards(transform.position, dest, speed);
        GetComponent<Rigidbody2D>().MovePosition(p);
    }

    //Looks if the choosen direction is free to go
    private bool Valid(Vector2 dir)
    {
        Debug.DrawRay(transform.position, dir, Color.blue);

        // Cast Line from 'Pacman' to a certain direction
        RaycastHit2D _hit = Physics2D.Raycast(transform.position, dir, 2f, wallLayerName);

        return (_hit.collider == null);
    }

    // Sets the desired direction based on player input
    private void SetDesiredDirection()
    {
        if (Input.GetKey(KeyCode.W))
            desiredDirection = Vector2.up;
        if (Input.GetKey(KeyCode.S))
            desiredDirection = Vector2.down;
        if (Input.GetKey(KeyCode.D))
            desiredDirection = Vector2.right;
        if (Input.GetKey(KeyCode.A))
            desiredDirection = Vector2.left;
    }

    //Chooses a random Direction
    private void SetRndDesiredDirection()
    {
        float rnd = Random.value;
        if (rnd <= 0.25f)
        {
            desiredDirection = Vector2.up;
        }
        else if (rnd <= 0.5f)
        {
            desiredDirection = Vector2.down;
        }
        else if (rnd <= 0.75f)
        {
            desiredDirection = Vector2.right;
        }
        else
        {
            desiredDirection = Vector2.left;
        }
    }

    //Sets the destination to current position
    public void SetDestinationToPosition()
    {
        dest = transform.position;
    }

    //Sets the ghosts state to player controlled
    public void SetControlledTo(bool control)
    {
        isControlled = control;
    }

    public bool IsControlled ()
    {
        return isControlled;
    }

    public void SetSpeed (float _speed)
    {
        speed = _speed;
    }

    public float GetSpeed ()
    {
        return speed;
    }

    public void ResetMovement ()
    {
        desiredDirection = Vector2.zero;
        currentDirection = Vector2.zero;
    }

    public void SetMoveableTo (bool _isMoveable)
    {
        isMovable = _isMoveable;
    }

    public bool IsMovable ()
    {
        return isMovable;
    }
}
