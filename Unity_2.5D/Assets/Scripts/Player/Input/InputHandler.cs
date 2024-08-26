using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{   
    private InputActions inputActions;

    [Header("Movement Stuff")]
    public bool isMovingRight;
    public bool isMovingLeft;

    [Header("Jump Stuff")]
    [field: SerializeField]
    public int groundLayer = 3; //layermask ground;
    public bool isGrounded;
    public bool isJumping;

    private void Awake()
    {
        isGrounded = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == groundLayer)
        {
            isGrounded = true;
            isJumping = false;
        }
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
