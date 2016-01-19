using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Enemy_DamageEnemyOneTime : ToonCharacterScript{

    public bool isAttacking = false;
    public bool oneHit = false;

/** ----------------------------------------------------------------------------- Methods -----------------------------------------------------------------------------------------------------*/

/** -------------------------------- Standard Methods ------------------------------*/

    void Awake(){
        //Bind Animator
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update () {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack")) {
            isAttacking = true;
        } else {
            isAttacking = false;
            oneHit = false;
        }
    }

/** -------------------------------- Other Methods ------------------------------*/



}

