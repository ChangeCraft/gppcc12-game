using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(PacmanMove))]
public class Pacman : MonoBehaviour {

    [SerializeField]
    private int lifes;

    [SerializeField]
    private LiveUI liveUI;

    [SerializeField]
    private GameUI gameUI; 

    private int currentLives;

    private PacmanMove pacmanMove;

	void Start ()
    {
        currentLives = lifes;
        pacmanMove = GetComponent<PacmanMove>();

        if(liveUI == null)
        {
            Debug.LogError("No LiveUi assigned" + name);
            Destroy(this);
        } else
        {
            liveUI.SetLives(lifes);
        }
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
            liveUI.SubtractALife();
            transform.position = pacmanMove.spawnPosition;
            pacmanMove.SetDestinationToPosition();
        }
    }
}
