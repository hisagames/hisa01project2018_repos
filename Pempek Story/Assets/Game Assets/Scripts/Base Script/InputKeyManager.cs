using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputKeyManager : MonoBehaviour
{
    enum InputState
    {
        None,
        InBagMenu,
        InShelfMenu
    }

    InputState inputState;

    [SerializeField]
    UIManager bagManager;

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
                    ToolItemManager.instance.uiState = ToolItemManager.UIState.BagMenu;
                    ToolItemManager.instance.changeToolItemOpenState(true);
                    inputState = InputState.InBagMenu;
                    PlayerMovement.instance.movementState = PlayerMovement.MovementState.idle;
                }

                if (Input.GetKeyDown(KeyCode.Z))
                {
                    if (Player.instance.inCollisionKey == "ShelfCollision")
                    {
                        ToolItemManager.instance.uiState = ToolItemManager.UIState.ShelfMenu;
                        ToolItemManager.instance.changeToolItemOpenState(true);
                        inputState = InputState.InShelfMenu;
                        PlayerMovement.instance.movementState = PlayerMovement.MovementState.idle;
                    }
                }
                break;
            #endregion

            case InputState.InBagMenu:
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    ToolItemManager.instance.UpdateToolItemPointer("left");
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    ToolItemManager.instance.UpdateToolItemPointer("right");
                }
                else if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    ToolItemManager.instance.UpdateToolItemPointer("up");
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    ToolItemManager.instance.UpdateToolItemPointer("down");
                }
                else if (Input.GetKeyDown(KeyCode.Z))
                {
                    ToolItemManager.instance.UpdateToolItemChoosenPointer(true);
                }

                if (Input.GetKeyDown(KeyCode.X))
                {
                    bool temp = ToolItemManager.instance.changeToolItemOpenState(false);
                    if (temp)
                        inputState = InputState.None;
                }
                break;

            case InputState.InShelfMenu:
                //MASIH TERDAPAT 2 TASK YANG PERLU DI FIX KAN
                //-Saat movement pointer di shelf menu masih mengarah pada object item, fix kan.
                //-Menu Shelf menu hanya terbuka saat player menekan tombol z sambil trigger pada shelf menu

                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    ToolItemManager.instance.UpdateToolItemPointer("left");
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    ToolItemManager.instance.UpdateToolItemPointer("right");
                }
                else if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    ToolItemManager.instance.UpdateToolItemPointer("up");
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    ToolItemManager.instance.UpdateToolItemPointer("down");
                }
                else if (Input.GetKeyDown(KeyCode.Z))
                {
                    ToolItemManager.instance.UpdateToolItemChoosenPointer(true);
                }

                if (Input.GetKeyDown(KeyCode.X))
                {
                    bool temp = ToolItemManager.instance.changeToolItemOpenState(false);
                    if (temp)
                        inputState = InputState.None;
                }
                break;
        }
    }
}