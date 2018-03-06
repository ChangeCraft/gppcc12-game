using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour {

    [SerializeField]
    private GameObject[] ghosts;

    private int currentControlledGhost = 0;

    private void Start()
    {
        //cycle through all ghosts and disable their player control
        foreach (var ghost in ghosts)
        {
            GhostMove ghostMove = ghost.GetComponent<GhostMove>();
            if(ghostMove == null)
            {
                Debug.LogError(ghost.name + " has no GhostMove Script attached!");
                Destroy(this);
            } else
            {
                ghostMove.SetControlledTo(false);
            }
        }
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            currentControlledGhost++;

            if (currentControlledGhost > ghosts.Length)
                currentControlledGhost = 0;

            SwitchControlledGhostTo(currentControlledGhost);
        }
    }

    private void SwitchControlledGhostTo (int ghostNumber)
    {
        foreach (var ghost in ghosts)
        {
            GhostMove ghostMove = ghost.GetComponent<GhostMove>();
            ghostMove.SetControlledTo(false);
        }

        if (ghostNumber != 0)
        {
            //Subtraction done becaues the first array value has an index of 0
            //and 0 is already preserved for "no ghost"
            ghostNumber--;

            ghosts[ghostNumber].GetComponent<GhostMove>().SetControlledTo(true);
        }
    }

}
