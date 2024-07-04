using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationScript : MonoBehaviour
{
    [SerializeField] GameObject playerRef;
    [SerializeField] Animator animator;
    [SerializeField] SpriteRenderer spriterender;
    //[SerializeField] float moveX;
    private void LateUpdate()
    {
        if (playerRef.GetComponent<PlayerMovement>().movement != new Vector3(0, 0, 0))
        {
            animator.SetBool("IsMoving", true);
            animator.SetFloat("PosX", playerRef.GetComponent<PlayerMovement>().movement.x);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }
        if (playerRef.GetComponent<PlayerMovement>().movement.x > 0)
        {
            spriterender.flipX = false;
        } 
        else if (playerRef.GetComponent<PlayerMovement>().movement.x < 0)
        {
            spriterender.flipX = true;
        }
    }
}
