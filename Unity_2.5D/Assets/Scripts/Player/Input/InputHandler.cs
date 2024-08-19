using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public bool isMovingRight;
    public bool isMovingLeft;
   
    private InputActions inputActions;

    public bool isGrounded;
    public bool isJumping;

    [field: SerializeField]
    public int groundLayer = 3; //layermask ground;

    private void Awake()
    {
        isGrounded = false;
    }

    #region INPUTACTION_SETTINGS
    private void OnEnable()
    {
        if(inputActions == null)
        {
            inputActions = new InputActions();    
        }
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }
    #endregion

    #region MOVE_SETTINGS
    public void SetMoveDirection()
    {
        isMovingRight =
            inputActions.Player.MoveRight.ReadValue<float>() > 0f;

        isMovingLeft =
            inputActions.Player.MoveLeft.ReadValue<float>() > 0f;
    }
    #endregion

    #region JUMP_SETTINGS
    public bool IsJumpBttnPressed()
    {
        return inputActions.Player.Jump.ReadValue<float>() > 0f;
    }
    #endregion

    #region DASH_SETTINGS
    public bool isDashBttnPressed()
    {
        return inputActions.Player.Dash.ReadValue<float>() > 0f;
    }

    #endregion
}
