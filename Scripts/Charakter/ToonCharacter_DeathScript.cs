using UnityEngine;
using System.Collections;

public class ToonCharacter_DeathScript : ToonCharacterScript {

    bool isPlayerDead = false;
        
    void Start () {
        animator = this.GetComponent<Animator>();
        isDying = false;
	}

	void Update () {
        if (this.GetComponent<ToonCharacter_PlayerScript>().getBasePlayer().playerStats.health == 0) {
            if (!isPlayerDead) {
                isPlayerDead = true;
                isDying = true;
            } else {
                isDying = false;
            }
            setAnimatorVariables(this);
        }
    }

}
