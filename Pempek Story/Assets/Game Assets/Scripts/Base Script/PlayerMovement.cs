using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;

    [System.Serializable]
    public enum MovementState
    {
        idle,
        walkToRight,
        walkToLeft,
        walkToDown,
        walkToUp
    }

    public MovementState movementState;

    public Vector2 speed;
    [SerializeField]
    Animator animator;

    [SerializeField]
    float pivotDifferentY;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        movementState = MovementState.idle;
        animator.SetInteger("State", (int)movementState); //state 0 idle animation
    }
	
	void Update () {
		
	}

    void FixedUpdate()
    {
        if (!Player.instance.inSellerMode)
            CharacterMovement();
        else
            CharacterSellingThings();
    }

    void CharacterMovement()
    {
        if (animator.GetInteger("State") == 5 || animator.GetInteger("State") == 6 || animator.GetInteger("State") == 7)
        {
            //still hardcode in here......................
            animator.Play("Girl1Idle (0)"); 
            animator.SetInteger("State", 0); //state 0 idle animation
        }

        //Update z position depend on yposition
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y + pivotDifferentY);

        switch (movementState)
        {
            case MovementState.walkToLeft:
                if (!StopColliderManager.instance.stopLeftColActive)
                    transform.DOMoveX(transform.position.x - speed.x, Time.deltaTime, false);
                break;
            case MovementState.walkToRight:
                if (!StopColliderManager.instance.stopRightColActive)
                    transform.DOMoveX(transform.position.x + speed.x, Time.deltaTime, false);
                break;
            case MovementState.walkToUp:
                if (!StopColliderManager.instance.stopUpColActive)
                    transform.DOMoveY(transform.position.y + speed.y, Time.deltaTime, false);
                break;
            case MovementState.walkToDown:
                if (!StopColliderManager.instance.stopDownColActive)
                    transform.DOMoveY(transform.position.y - speed.y, Time.deltaTime, false);
                break;
        }
        animator.SetInteger("State", (int)movementState);
    }

    void CharacterSellingThings()
    {
        if (QueueManager.instance.isQueueEmpty())
        {
            transform.position = new Vector3(-4.75f, 2f, transform.position.y);
            animator.SetInteger("State", 5); //state 5 greet animation
            animator.Play("Girl1Greet (5)");
        }
        else
        {
            transform.position = new Vector3(-4.75f, 2f, transform.position.y);
            if (Player.instance.state == 6) {
                animator.SetInteger("State", 6); 
                animator.Play("Girl1Cooking (6)");
            }
            else if (Player.instance.state == 7)
            {
                animator.SetInteger("State", 7); 
                animator.Play("Girl1GivingFood (7)");
            }
            else if (Player.instance.state == 0)
            {
                animator.Play("Girl1Idle (0)");
                animator.SetInteger("State", 0); //state 0 idle animation
            }
        }
    }
}