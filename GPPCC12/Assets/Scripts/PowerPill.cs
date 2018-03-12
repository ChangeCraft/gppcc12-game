using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPill : MonoBehaviour
{

    [SerializeField]
    private string playerTag = "Player";

    [SerializeField]
    private int earnedScore;

    [SerializeField]
    private GhostController ghostController;

    [SerializeField]
    AudioClip sound;

    private void Start()
    {
        if (ghostController == null)
        {
            Debug.LogError(this.name + " has no ghost Controller assigned, it will be destroyed");
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == playerTag)
        {
            GameObject.Find("UI").GetComponent<GameUI>().AddToScore(earnedScore);
            AudioSource.PlayClipAtPoint(sound, new Vector3(0, 0, 0));
            Destroy(gameObject);

            ghostController.PowerPillConsumed();
        }
    }
}

