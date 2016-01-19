using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager_ChoosePlayer : MonoBehaviour {

	public Toggle warriorToggle;
	public Toggle archerToggle;
	public Toggle magicanToggle;

	public GameObject warrior;
	public GameObject archer;
	public GameObject magican;

	public Text descriptionText;
	public InputField playerNameInput;

	private BaseCharacterArcher baseArcher = new BaseCharacterArcher ();
	private BaseCharacterWarrior baseWarrior = new BaseCharacterWarrior ();
	private BaseCharacterMage baseMage = new BaseCharacterMage ();

	private PlayerType currentPlayerType = PlayerType.MELEE;

	// Use this for initialization
	void Start () {
		this.descriptionText.text = this.baseWarrior.characterDescription;

		//set start configuration
		this.warriorToggle.GetComponentInChildren<Text>().text = this.baseWarrior.characterClassName;
		this.archerToggle.GetComponentInChildren<Text> ().text = this.baseArcher.characterClassName;
		this.magicanToggle.GetComponentInChildren<Text> ().text = this.baseMage.characterClassName;

		//set visibility of start characters
		this.showGameObject (this.warrior, true);
		this.showGameObject (this.archer, false);
		this.showGameObject (this.magican, false);
	}

	/*
	 * 0 - Warrior
	 * 1 - Archer
	 * 2 - Magican
	 */
	public void togglePlayerType(int type) {

		if (type == (int)PlayerType.MELEE && this.warriorToggle.isOn) {
			this.descriptionText.text = this.baseWarrior.characterDescription;
		}

		if (type == (int)PlayerType.DISTANCE && this.archerToggle.isOn) {
			this.descriptionText.text = this.baseArcher.characterDescription;
		}

		if (type == (int)PlayerType.MAGIC && this.magicanToggle.isOn) {
			this.descriptionText.text = this.baseMage.characterDescription;
		}
			
		this.showGameObject (this.warrior, this.warriorToggle.isOn);
		this.showGameObject (this.archer, this.archerToggle.isOn);
		this.showGameObject (this.magican, this.magicanToggle.isOn);

		this.currentPlayerType = (PlayerType)type;
	}

	private void showGameObject(GameObject obj, bool visible) {
		Renderer[] renderers = obj.GetComponentsInChildren<Renderer> ();
		foreach (Renderer r in renderers) {
			r.enabled = visible;
		}
	}

	public void savePlayer() {
		BasePlayer player = new BasePlayer ();
		player.playerName = this.playerNameInput.text;
		player.playerType = this.currentPlayerType;

		BaseCharacter character = new BaseCharacter ();

		switch (this.currentPlayerType) {
			case PlayerType.MELEE:
				character = this.baseWarrior;
				break;
			case PlayerType.DISTANCE:
				character = this.baseArcher;
				break;
			case PlayerType.MAGIC:
				character = this.baseMage;
				break;
		}

		player.playerStats = new PlayerStats();
		player.playerStats.maxHealth        = character.maxHealth;
		player.playerStats.health           = character.maxHealth;
		player.playerStats.strength         = character.strength;
        player.playerStats.damageReduction = character.damageReduction;
		player.playerStats.initSkillLevel (this.currentPlayerType);

        player.positionX = warrior.transform.position.x;
        player.positionY = warrior.transform.position.y;
        player.positionZ = warrior.transform.position.z;

		player.rotationX	= warrior.transform.rotation.eulerAngles.x;
        player.rotationY 	= warrior.transform.rotation.eulerAngles.y;
        player.rotationZ	= warrior.transform.rotation.eulerAngles.z;

        GameConstants.newGame = true;

        DataManager dataManager = new DataManager ();
		dataManager.saveToFile<BasePlayer> (player, GameConstants.saveFileName);

		NavigationManager navigationManager = GetComponent<NavigationManager> ();
		navigationManager.LoadScene (GameConstants.sceneGame);
	}
}
