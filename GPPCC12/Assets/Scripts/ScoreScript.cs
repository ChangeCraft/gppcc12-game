using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreScript : MonoBehaviour {

    [SerializeField]
    private string playerTag = "Player";

    [SerializeField]
    private int earnedScore;

    [SerializeField]
    AudioClip sound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == playerTag)
        {
            GameObject.Find("UI").GetComponent<GameUI>().AddToScore(earnedScore);
            AudioSource.PlayClipAtPoint(sound, new Vector3(0, 0, 0));
            Destroy(gameObject);
        }
    }
}
