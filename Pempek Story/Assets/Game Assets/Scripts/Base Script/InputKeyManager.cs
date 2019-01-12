using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputKeyManager : MonoBehaviour
{
    enum InputState
    {
        None,
        InBagMenu
    }

    InputState inputState;

    void Start()
    {
        
    }

    void Update()
    {
        switch (inputState)
        {
            case InputState.None:
                #region PlayerMovement
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    PlayerMovement.instance.movementState = PlayerMovement.MovementState.walkToLeft;
                }
                else if (Input.GetKey(KeyCode.RightArrow))
                {
                    PlayerMovement.instance.movementState = PlayerMovement.MovementState.walkToRight;
                }
                else if (Input.GetKey(KeyCode.UpArrow))
                {
                    PlayerMovement.instance.movementState = PlayerMovement.MovementState.walkToUp;
                }
                else if (Input.GetKey(KeyCode.DownArrow))
                {
                    PlayerMovement.instance.movementState = PlayerMovement.MovementState.walkToDown;
                }
                else if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow))
                {
                    PlayerMovement.instance.movementState = PlayerMovement.MovementState.idle;
                }
                #endregion

                #region Go To Other Input State
                if (Input.GetKeyDown(KeyCode.S))
                {
                    BagManager.instance.changeBagOpenState(true);
                    inputState = InputState.InBagMenu;
                }
                break;
                #endregion

            case InputState.InBagMenu:
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    BagManager.instance.UpdateBagPointer("left");
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    BagManager.instance.UpdateBagPointer("right");
                }
                else if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    BagManager.instance.UpdateBagPointer("up");
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    BagManager.instance.UpdateBagPointer("down");
                }

                if (Input.GetKeyDown(KeyCode.X))
                {
                    BagManager.instance.changeBagOpenState(false);
                    inputState = InputState.None;
                }
                break;
        }
    }
}