using UnityEngine;
using System.Collections;

public class UIManager_Respawn : UIManager_BaseGame {

	private bool 	isDying = false;
	private double 	waitingTime;

	// Update is called once per frame
	void Update () {
		BasePlayer player = GameConstants.findPlayerInProject ().GetComponent<ToonCharacter_PlayerScript>().getBasePlayer();

		if (player.playerStats.health == 0) {
			isDying = true;

			if (isDying) {
				waitingTime += Time.deltaTime;
			}
				
			if (waitingTime >= 2.0) {
				this.activateMenu (true);
			}
		}
	}

	public void respawn() {
		respawnPlayer ();
	}
}
