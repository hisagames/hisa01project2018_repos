using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputKeyManager : MonoBehaviour
{
    enum InputState
    {
        None,
        InBagMenu,
        InShelfMenu,
        InFridgeMenu,
        InSleepConfirmationMenu,
        InSaveConfirmationMenu
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
                    PlayerMovement.instance.movementState = PlayerMovement.MovementState.walkToLeft;
                else if (Input.GetKey(KeyCode.RightArrow))
                    PlayerMovement.instance.movementState = PlayerMovement.MovementState.walkToRight;
                else if (Input.GetKey(KeyCode.UpArrow))
                    PlayerMovement.instance.movementState = PlayerMovement.MovementState.walkToUp;
                else if (Input.GetKey(KeyCode.DownArrow))
                    PlayerMovement.instance.movementState = PlayerMovement.MovementState.walkToDown;
                else if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.UpArrow)
                    || Input.GetKeyUp(KeyCode.DownArrow))
                    PlayerMovement.instance.movementState = PlayerMovement.MovementState.idle;
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
                        PopupNoteManager.instance.changeBottomInfo(false);
                    }
                    if (Player.instance.inCollisionKey == "FridgeCollision")
                    {
                        ToolItemManager.instance.uiState = ToolItemManager.UIState.FridgeMenu;
                        ToolItemManager.instance.changeToolItemOpenState(true);
                        inputState = InputState.InFridgeMenu;
                        PlayerMovement.instance.movementState = PlayerMovement.MovementState.idle;
                        PopupNoteManager.instance.changeBottomInfo(false);
                    }
                    if (Player.instance.inCollisionKey == "BedCollision")
                    {
                        SleepManager.instance.changeSleepConfirmationInfo(true);
                        inputState = InputState.InSleepConfirmationMenu;
                        PlayerMovement.instance.movementState = PlayerMovement.MovementState.idle;
                        PopupNoteManager.instance.changeBottomInfo(false);
                    }
                }
                break;
            #endregion

            case InputState.InBagMenu:
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                    ToolItemManager.instance.UpdateToolItemPointer("left");
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                    ToolItemManager.instance.UpdateToolItemPointer("right");
                else if (Input.GetKeyDown(KeyCode.UpArrow))
                    ToolItemManager.instance.UpdateToolItemPointer("up");
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                    ToolItemManager.instance.UpdateToolItemPointer("down");
                else if (Input.GetKeyDown(KeyCode.Z))
                    ToolItemManager.instance.UpdateToolItemChoosenPointer(true);

                if (Input.GetKeyDown(KeyCode.X))
                {
                    bool temp = ToolItemManager.instance.changeToolItemOpenState(false);
                    if (temp)
                    {
                        inputState = InputState.None;
                    }
                }
                break;

            case InputState.InShelfMenu:
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                    ToolItemManager.instance.UpdateToolItemPointer("left");
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                    ToolItemManager.instance.UpdateToolItemPointer("right");
                else if (Input.GetKeyDown(KeyCode.UpArrow))
                    ToolItemManager.instance.UpdateToolItemPointer("up");
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                    ToolItemManager.instance.UpdateToolItemPointer("down");
                else if (Input.GetKeyDown(KeyCode.Z))
                    ToolItemManager.instance.UpdateToolItemChoosenPointer(true);

                if (Input.GetKeyDown(KeyCode.X))
                {
                    bool temp = ToolItemManager.instance.changeToolItemOpenState(false);
                    if (temp)
                    {
                        inputState = InputState.None;
                        PopupNoteManager.instance.changeBottomInfo(true);
                    }
                }
                break;

            case InputState.InFridgeMenu:
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                    ToolItemManager.instance.UpdateToolItemPointer("left");
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                    ToolItemManager.instance.UpdateToolItemPointer("right");
                else if (Input.GetKeyDown(KeyCode.UpArrow))
                    ToolItemManager.instance.UpdateToolItemPointer("up");
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                    ToolItemManager.instance.UpdateToolItemPointer("down");
                else if (Input.GetKeyDown(KeyCode.Z))
                    ToolItemManager.instance.UpdateToolItemChoosenPointer(true);

                if (Input.GetKeyDown(KeyCode.X))
                {
                    bool temp = ToolItemManager.instance.changeToolItemOpenState(false);
                    if (temp)
                    {
                        inputState = InputState.None;
                        PopupNoteManager.instance.changeBottomInfo(true);
                    }
                }
                break;

            case InputState.InSleepConfirmationMenu:
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    SleepManager.instance.updateSleepConfirmationState(!SleepManager.instance.inYesConfirmationState);
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    SleepManager.instance.updateSleepConfirmationState(!SleepManager.instance.inYesConfirmationState);
                }

                if (Input.GetKeyDown(KeyCode.Z))
                {
                    bool temp = SleepManager.instance.inYesConfirmationState;
                    if (!temp) //no
                    {
                        SleepManager.instance.changeSleepConfirmationInfo(false);
                        inputState = InputState.None;
                        PopupNoteManager.instance.changeBottomInfo(true);
                    }
                    else //yes
                    {
                        SleepManager.instance.changeSleepConfirmationInfo(false);
                        inputState = InputState.InSaveConfirmationMenu;
                        SaveManager.instance.changeSaveConfirmationInfo(true);
                    }
                }

                if (Input.GetKeyDown(KeyCode.X))
                {
                    SleepManager.instance.changeSleepConfirmationInfo(false);
                    inputState = InputState.None;
                    PopupNoteManager.instance.changeBottomInfo(true);
                }
                break;

            case InputState.InSaveConfirmationMenu:
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    SaveManager.instance.inYesConfirmationState -= 1;
                    if (SaveManager.instance.inYesConfirmationState < 0)
                        SaveManager.instance.inYesConfirmationState = 2;

                    SaveManager.instance.updateSaveConfirmationState(SaveManager.instance.inYesConfirmationState);
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    SaveManager.instance.inYesConfirmationState = (SaveManager.instance.inYesConfirmationState + 1) % 3;
                    SaveManager.instance.updateSaveConfirmationState(SaveManager.instance.inYesConfirmationState);
                }

                if (Input.GetKeyDown(KeyCode.Z))
                {
                    int temp = SaveManager.instance.inYesConfirmationState;
                    if (temp == 0) //yes
                    {
                        SaveManager.instance.changeSaveConfirmationInfo(false);
                        inputState = InputState.None;
                        // + proses sleep pindah ke next day
                        // + proses save data dan lainnya
                        // + still on progress
                    }
                    else if (temp == 1) //no
                    {
                        SaveManager.instance.changeSaveConfirmationInfo(false);
                        inputState = InputState.None;
                        // + proses sleep pindah ke next day
                        // + still on progress
                    }
                    else if (temp == 2) //don't want sleep
                    {
                        SaveManager.instance.changeSaveConfirmationInfo(false);
                        inputState = InputState.InSleepConfirmationMenu;
                        SleepManager.instance.changeSleepConfirmationInfo(true);
                    }
                }

                if (Input.GetKeyDown(KeyCode.X))
                {
                    SaveManager.instance.changeSaveConfirmationInfo(false);
                    inputState = InputState.InSleepConfirmationMenu;
                    SleepManager.instance.changeSleepConfirmationInfo(true);
                }
                break;
        }
    }
}