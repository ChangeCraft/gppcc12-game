using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {

    [SerializeField]
    private Vector2 teleportPosition;
    private string playerTag = "Player";

    [SerializeField]
    private GameObject dotsPrefab;

    [SerializeField]
    private AudioClip sound;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == playerTag)
        {
            other.transform.position = teleportPosition;
            other.GetComponent<PacmanMove>().SetDestinationToPosition();
            Instantiate(dotsPrefab);
            AudioSource.PlayClipAtPoint(sound, new Vector3(0, 0, 0));
            Destroy(gameObject);
        }
    }
}
