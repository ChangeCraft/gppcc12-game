using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreScript : MonoBehaviour {

    [SerializeField]
    private string playerTag = "Player";

    [SerializeField]
    private int earnedScore;

    [SerializeField]
    private bool isPowerPill = false;

    [SerializeField]
    AudioClip sound;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == playerTag)
        {
            other.GetComponent<Pacman>().DotCollision(earnedScore, sound, isPowerPill);
            if(transform.parent != null)
                transform.parent.GetComponent<Dots>().CheckChildren();
            Destroy(gameObject);
        }
    }
}
