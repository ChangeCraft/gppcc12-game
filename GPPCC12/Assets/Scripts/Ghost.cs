using System.Collections;
using UnityEngine;
using UnityEngine.Animations;

[RequireComponent (typeof(GhostMove))]
public class Ghost : MonoBehaviour {

    [SerializeField]
    private AnimatorOverrideController ghostEatableAnimCtrl;
    [SerializeField]
    private RuntimeAnimatorController ghostAnimStd;

    Animator animator;

    GhostMove ghostMove;

    [SerializeField]
    float ghostRespawnTime = 5f;
    [SerializeField]
    Vector2 ghostRespawnPosition;

    [SerializeField]
    Vector2 ghostWaitPosition;

    private bool eatable;

    [SerializeField]
    private string playerTag = "Player";

    private void Start()
    {
        ghostMove = GetComponent<GhostMove>();

        animator = GetComponent<Animator>();
        animator.runtimeAnimatorController = ghostAnimStd;

        eatable = false;
    }

	public IEnumerator SetEatable (float _eatableTime, float _speedMultiplicator)
    {
        ghostMove.SetSpeed(ghostMove.GetSpeed() * _speedMultiplicator);
        eatable = true;
        animator.runtimeAnimatorController = ghostEatableAnimCtrl;

		float startTime = Time.time;
		while (Time.time < startTime + _eatableTime) 
		{
			if (eatable == false) 
			{
				eatable = true;
				animator.runtimeAnimatorController = ghostEatableAnimCtrl;
			}
			yield return null;
		}

        ghostMove.SetSpeed(ghostMove.GetSpeed() / _speedMultiplicator);
        eatable = false;
        animator.runtimeAnimatorController = ghostAnimStd;
    }

    public bool IsEatable ()
    {
        return eatable;
    }

    //Kill fuc**n MacPan
    void OnTriggerEnter2D(Collider2D co)
    {
        if (co.tag == playerTag)
        {
            if (!eatable)
            {
                co.GetComponent<Pacman>().PacmanDie();
                //Call some die function etc
            } else
            {
                ghostMove.SetMoveableTo(false);
                gameObject.transform.position = ghostWaitPosition;
                ghostMove.SetDestinationToPosition();
                ghostMove.ResetMovement();
                animator.runtimeAnimatorController = ghostAnimStd;
                StartCoroutine(Respawn(ghostRespawnTime));
            }
        }

    }

    public IEnumerator Respawn (float _respawnTime)
    {
        float _startTime = Time.time;

        while(Time.time <= _startTime + _respawnTime)
        {
            yield return null;
        }

        gameObject.transform.position = ghostRespawnPosition;
        ghostMove.SetDestinationToPosition();
        ghostMove.ResetMovement();
        animator.runtimeAnimatorController = ghostAnimStd;
        ghostMove.SetMoveableTo(true);
    }
}

