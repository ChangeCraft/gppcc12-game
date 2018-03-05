using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanMove : MonoBehaviour
{

    [SerializeField]
    private float speed = 0.4f;
    [SerializeField]
    private LayerMask wallLayerName;

    private int stepSize = 2;

    Vector2 dest = Vector2.zero;
    Vector2 currentDirection = Vector2.zero;
    Vector2 desiredDirection = Vector2.zero;

    void Start()
    {
        dest = transform.position;
    }

    private void Update()
    {
        SetDesiredDirection();

        //AnimationParameters
        GetComponent<Animator>().SetFloat("DirX", currentDirection.x);
        GetComponent<Animator>().SetFloat("DirY", currentDirection.y);
    }

    void FixedUpdate()
    {
        if ((Vector2)transform.position == dest)
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

    bool Valid(Vector2 dir)
    {

        Debug.DrawRay(transform.position, dir, Color.blue);

        // Cast Line from 'Pacman' to a certain direction
        RaycastHit2D _hit = Physics2D.Raycast(transform.position, dir, 2f, wallLayerName);

        return (_hit.collider == null);
    }

    // Sets the desired direction based on player input
    private void SetDesiredDirection()
    {
        if (Input.GetKey(KeyCode.UpArrow))
            desiredDirection = Vector2.up;
        if (Input.GetKey(KeyCode.DownArrow))
            desiredDirection = Vector2.down;
        if (Input.GetKey(KeyCode.RightArrow))
            desiredDirection = Vector2.right;
        if (Input.GetKey(KeyCode.LeftArrow))
            desiredDirection = Vector2.left;
    }

    //Sets the destination to current position
    public void SetDestinationToPosition ()
    {
        dest = transform.position;
    }
}
