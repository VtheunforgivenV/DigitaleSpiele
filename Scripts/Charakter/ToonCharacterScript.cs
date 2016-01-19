using UnityEngine;

public class ToonCharacterScript : MonoBehaviour {

    /** ----------------------------------------------------------------------------- Variables -----------------------------------------------------------------------------------------------------*/

	protected const float XSensitivity = 5f;
	protected const float YSensitivity = 5f;
	protected const int MinimumX = -60;
	protected const int MaximumX = 70;

	protected Animator animator;
	protected float forwardSpeed;
	protected float turnSpeed;
	protected bool run;
	protected bool sneak;
	protected bool attack;
	protected bool isDying;

/** ----------------------------------------------------------------------------- Methods -----------------------------------------------------------------------------------------------------*/

/** -------------------------------- Other Methods ------------------------------*/

    public void setAnimatorVariables(ToonCharacterScript script){
        if(script is ToonCharacter_MouseMotion){
            animator.SetBool("Attack", attack);
        }else if(script is ToonCharacter_KeyboardMotion){
            animator.SetFloat("Walk", forwardSpeed);
            animator.SetBool("Run", run);
            animator.SetFloat("Turn", turnSpeed);
            animator.SetBool("Sneak", sneak);
        }else if(script is ToonCharacter_DeathScript) {
            animator.SetBool("isDying", isDying);
        }
    }

}
