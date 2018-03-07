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

    public void EnableEatable ()
    {
        eatable = true;
        animator.runtimeAnimatorController = ghostEatableAnimCtrl;
    }

    public void DisableEatable ()
    {
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
                Destroy(co.gameObject);
                //Call some die function etc
            } else
            {
                ghostMove.CanMove(false);
                gameObject.transform.position = ghostWaitPosition;
                ghostMove.SetDestinationToPosition();
                ghostMove.ResetMovement();
                animator.runtimeAnimatorController = ghostAnimStd;
            }
        }

    }
}

