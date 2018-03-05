using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreScript : MonoBehaviour {

    [SerializeField]
    private string playerTag = "Player";
    [SerializeField]
    private int earnedScore;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == playerTag)
        {
            GameObject.Find("UI").GetComponent<GameUI>().AddToScore(earnedScore);
            Destroy(gameObject);
        }
    }
}
