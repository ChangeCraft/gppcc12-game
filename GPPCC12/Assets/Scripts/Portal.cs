using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {

    [SerializeField]
    private Vector2 teleportPosition;
    private string playerTag = "Player";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == playerTag)
        {
            other.transform.position = teleportPosition;
            other.GetComponent<PacmanMove>().SetDestinationToPosition();
        }
    }
}
