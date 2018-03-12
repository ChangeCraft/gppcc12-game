using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(PacmanMove))]
public class Pacman : MonoBehaviour {

    [SerializeField]
    private int lifes;

    [SerializeField]
    private GameUI gameUI; 

    private int currentLives;

    private PacmanMove pacmanMove;

	void Start ()
    {
        currentLives = lifes;
        pacmanMove = GetComponent<PacmanMove>();
	}

    public void PacmanDie()
    {
        if (currentLives <= 0)
        {
            // Call GameOver Screen
            gameUI.EnableGameOverUI();
            Destroy(gameObject);
        } else
        {
            currentLives--;
            transform.position = pacmanMove.spawnPosition;
            pacmanMove.SetDestinationToPosition();
        }
    }
}
