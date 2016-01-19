using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager_GameMenu : UIManager_BaseGame {

	// Start inherit from BaseGame
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape)) {
			this.activateMenu (!this.info.GetComponentInParent<Canvas>().isActiveAndEnabled);
        }
	}

	public void saveGame() {
        GameObject playerGameObj = GameConstants.findPlayerInProject();
        BasePlayer playerObj = playerGameObj.GetComponent<ToonCharacter_PlayerScript>().getBasePlayer();

        playerObj.playerStats.updateSkillLevel();

        playerObj.playerStats.updateInventoryDictionary();

        //set position
        playerObj.positionX = playerGameObj.transform.position.x;
        playerObj.positionY = playerGameObj.transform.position.y;
        playerObj.positionZ = playerGameObj.transform.position.z;

        Vector3 rotation = playerGameObj.transform.eulerAngles;
        playerObj.rotationX = rotation.x;
        playerObj.rotationY = rotation.y;
        playerObj.rotationZ = rotation.z;

        DataManager dataManager = new DataManager ();
		dataManager.saveToFile<BasePlayer> (playerObj, GameConstants.saveFileName);

		this.showMessageBox ("Das Spiel wurde gespeichert");
	}

	public void respawn() {
		respawnPlayer ();
	}
}
