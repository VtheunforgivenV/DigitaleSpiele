using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager_MainMenu : MonoBehaviour {

	public Button startGameButton;

	// Use this for initialization
	void Start () {
		Cursor.lockState = CursorLockMode.None;

		DataManager dataManager = new DataManager ();

		Debug.Log ("File Exists: " + dataManager.fileExists (GameConstants.saveFileName));

		if (dataManager.fileExists (GameConstants.saveFileName)) {
			this.startGameButton.interactable = true;

//			BasePlayer player = dataManager.readFile<BasePlayer> (GameConstants.saveFileName);
//			Debug.Log ("PlayerName: " + player.playerName);
		} else {
			this.startGameButton.interactable = false;
		}
	}
}
