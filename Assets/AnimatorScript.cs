using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorScript : MonoBehaviour
{

    public RuntimeAnimatorController controllerRun;
    public RuntimeAnimatorController controllerJump;
    public RuntimeAnimatorController controllerIdle;
    public RuntimeAnimatorController controllerHit;
    public RuntimeAnimatorController controllerDie;
    public RuntimeAnimatorController controllerAttack;
    public Animator animator;
    // Start is called before the first frame update


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isJumping", movementScript.isInAir);
        animator.SetBool("isRunning", movementScript.isRunning);
        animator.SetBool("isAlive", movementScript.isAlive);

    }
}
