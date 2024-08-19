using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [field: SerializeField]
    private InputHandler inputHandler;
    [field: SerializeField]
    private AnimatorHandler animatorHandler;
    [field: SerializeField]
    private Image hpImage;

    private GameObject playerObject;
    private Rigidbody playerRb;
    
    [Header("Health Settings")]
    [field: SerializeField]
    private float maxHP = 10f;
    [field: SerializeField]
    private float currHP;

    [Header("Energy Settings")]
    [field: SerializeField]
    private float maxEnergy = 10f;
    [field: SerializeField]
    private float currEnergy;

    [Header("Input Settings")]
    [field: SerializeField]
    private float movementSpeed = 10f;
    [field: SerializeField]
    private float jumpForce = 7f;
    [field: SerializeField]
    private int maxJumpsAmount = 2;
    private int currJumpsAmount;
    [field: SerializeField]
    private float dashForce = 20f;
    [field: SerializeField]
    private float dashTime = 0.25f;


    void Awake()
    {
        //inputHandler = GetComponent<InputHandler>();
        //animatorHandler = GetComponent<AnimatorHandler>();

        playerObject = gameObject;
        playerRb = GetComponent<Rigidbody>();

        currJumpsAmount = 0;
        currHP = maxHP;
        currEnergy = 0;
    }

    void Update()
    {
        inputHandler.SetMoveDirection();
        HandleMove();
    }

    private void FixedUpdate()
    {
        HandleDash();
        HandleJump();
    }

    #region MOVE_SETTINGS
    private void HandleMove()
    {
        bool isMoving = inputHandler.isMovingRight ||
                inputHandler.isMovingLeft;
        
        if (inputHandler.isMovingRight && inputHandler.isMovingLeft)
        {
            animatorHandler.UpdateAnimatorValues(!isMoving);
            return;
        }
        
        if(inputHandler.isMovingRight)
        {     
            playerObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else if(inputHandler.isMovingLeft)
        {
            playerObject.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        else
        {
            animatorHandler.UpdateAnimatorValues(isMoving);
            return;
        }

        playerObject.transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
        animatorHandler.UpdateAnimatorValues(isMoving);
    }
    #endregion

    #region JUMP_SETTINGS
    private void HandleJump()
    {
        if (inputHandler.isGrounded && inputHandler.IsJumpBttnPressed())
        {

            inputHandler.isGrounded = false;
            // animatorHandler.UpdateAnimatorValues(true);
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            currJumpsAmount = maxJumpsAmount;
            inputHandler.isJumping = true;
        }
        if(inputHandler.IsJumpBttnPressed() && inputHandler.isJumping)
        {
            if (currJumpsAmount > 0)
            {
                playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                --currJumpsAmount;
            }
            else
            {
                inputHandler.isJumping = false;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == inputHandler.groundLayer)
        {
            inputHandler.isGrounded = true;
            inputHandler.isJumping = false;
        }
    }
    #endregion

    #region DASH_SETTINGS
    private void HandleDash()
    {
        if (inputHandler.isDashBttnPressed())
        {
            Vector3 forceToApply = new Vector3(0, 0, dashForce);

            if(inputHandler.isMovingLeft)
            {
                forceToApply = -forceToApply;
            }

            playerObject.transform.position += forceToApply;
            //playerRb.AddForce(forceToApply, ForceMode.VelocityChange);
        }        
    }
    #endregion

    #region HP_SETTINGS
    private void UpdateHp()
    {
        hpImage.fillAmount = currHP / 100;
    }

    #endregion
}
