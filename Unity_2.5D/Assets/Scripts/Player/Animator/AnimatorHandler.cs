using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorHandler : MonoBehaviour
{
    [field: SerializeField]
    private Animator playerAnimator;
    private int isRunning;
    private int isGrounded;

    public void Initialize()
    {
        isRunning = Animator.StringToHash("IsRunning");
        isGrounded = Animator.StringToHash("IsGrounded");
    }
    
    public void UpdateAnimatorValues(bool isRunning)
    {
        playerAnimator.SetBool("IsRunning", isRunning);
        //playerAnimator.SetBool("IsGrounded", isGrounded);
    }
}
