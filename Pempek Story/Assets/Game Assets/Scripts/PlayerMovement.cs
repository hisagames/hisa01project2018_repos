using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour {

    public Vector2 speed;
    [SerializeField]
    Animator animator;

    [SerializeField]
    float pivotDifferentY;

    void Start () {
        animator.SetInteger("State", 0); //state 0 idle animation
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
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y + pivotDifferentY);
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.DOMoveX(transform.position.x - speed.x, Time.deltaTime, false);
            animator.SetInteger("State", 2); //state 2 walk to left animation
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.DOMoveX(transform.position.x + speed.x, Time.deltaTime, false);
            animator.SetInteger("State", 1); //state 1 walk to right animation
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.DOMoveY(transform.position.y + speed.y, Time.deltaTime, false);
            animator.SetInteger("State", 4); //state 2 walk to left animation
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.DOMoveY(transform.position.y - speed.y, Time.deltaTime, false);
            animator.SetInteger("State", 3); //state 1 walk to right animation
        }
        else if(Input.GetKeyUp(KeyCode.LeftArrow)|| Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            animator.SetInteger("State", 0); //state 0 idle animation
        }
    }

    void CharacterSellingThings()
    {
        transform.position = new Vector3(-4.75f, 2f, transform.position.y);
    }
}