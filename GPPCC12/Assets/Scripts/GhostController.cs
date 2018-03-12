using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{

    [SerializeField]
    private GameObject[] ghosts;

    [SerializeField]
    private GameObject playerTagSprite;

    [SerializeField]
    private byte eatableTime = 5;

    [SerializeField]
    private float slowFactor = 0.8f;

    private int currentControlledGhost;

    private void Start()
    {
        //cycle through all ghosts and disable their player control
        foreach (var ghost in ghosts)
        {
            GhostMove ghostMove = ghost.GetComponent<GhostMove>();
            if (ghostMove == null)
            {
                Debug.LogError(ghost.name + " has no GhostMove Script attached!");
                Destroy(this);
            }
            else
            {
                ghostMove.SetControlledTo(false);
            }
        }

		currentControlledGhost = 0;
		SwitchControlledGhost (currentControlledGhost);
    }

    private void Update()
    {
        //On Space we want to switch to the next ghost
        if (Input.GetKeyDown(KeyCode.Space))
        {
            int oldGhost = currentControlledGhost;

            do
            {
                currentControlledGhost = (currentControlledGhost + 1) % (ghosts.Length);

                if (ghosts[currentControlledGhost].GetComponent<GhostMove>().IsMovable())
                {
                    //Switch controlls to the given ghost
                    SwitchControlledGhost(currentControlledGhost);
                    break;
                }
            } while (currentControlledGhost != oldGhost);
        }
    }

    private void SwitchControlledGhost(int _ghostNumber)
    {
        //Sets all ghosts to not player controlled
        foreach (var ghost in ghosts)
        {
            RemovePlayerTag(ghost);
            GhostMove ghostMove = ghost.GetComponent<GhostMove>();
            ghostMove.SetControlledTo(false);
        }

        //Sets the given ghost to be player controlled
        SetPlayerTag(_ghostNumber);
        ghosts[_ghostNumber].GetComponent<GhostMove>().SetControlledTo(true);
    }

    //Instantiates a player tag and adds it as a child to the
    //given ghost index in the ghost array
    private void SetPlayerTag(int _ghostNumber)
    {
        Transform ghostTransform = ghosts[_ghostNumber].transform;

        //Instantiate a playerTag
        GameObject playerTag = Instantiate(playerTagSprite, ghostTransform);
        playerTag.transform.parent = ghostTransform;
    }

    //Removes All childs form the given Object that have the 
    //same tag as a PlayerTag
    private void RemovePlayerTag(GameObject _ghost)
    {
        //cycle through all childs of the given ghost 
        for (int i = 0; i < _ghost.transform.childCount; i++)
        {
            Transform child = _ghost.transform.GetChild(i);

            //Destroys the child if it is a playertag
            if (child.tag == playerTagSprite.tag)
                Destroy(child.gameObject);
        }
    }

    public void PowerPillConsumed()
    {
        foreach (var ghost in ghosts)
        {
			if (ghost.GetComponent<GhostMove>().IsMovable()) 
			{
				StartCoroutine(ghost.GetComponent<Ghost>().SetEatable(eatableTime, slowFactor));
				//coroutine in ghost script 
			}
        }
    }
}
