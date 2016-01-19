using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class UIManager_Game : MonoBehaviour {

    // Player UI
    public PlayerUI playerUI;

    //Enemy UI
    public EnemyUI enemyUI;

    //Skill UI
    public ActiveSkillUI activeSkillUI;
    public List<GameObject> skillSlotList;
	private Dictionary<string, int> skillDictionary;
	private ToonCharacter_KeyboardMotion keyboardMotion;

    // Use this for initialization
    void Start () {
		keyboardMotion = GameConstants.findPlayerInProject ().GetComponent<ToonCharacter_KeyboardMotion> ();
        enemyUI.enemyPanel.SetActive(false);
        fillActiveSkillUI();
	}

    // Update is called once per frame
    void Update () {
        BasePlayer player = GameConstants.findPlayerInProject().GetComponent<ToonCharacter_PlayerScript>().getBasePlayer();

		// Cheet for testing
        if (Input.GetKeyDown(KeyCode.RightShift)) {
            //player.playerStats.health -= 20;
            player.playerStats.gainExp(25);
        }

        this.updatePlayerData(player);

		this.updateSkillList ();
    }

	void FixedUpdate() {

		if (keyboardMotion.potionCooldown != -1) {
			updateSkillCooldown (GameConstants.potionIdentifier, keyboardMotion.potionCooldown);
		}

	}

	private void updatePlayerData(BasePlayer player){
        playerUI.healthSlider.maxValue	= Convert.ToInt32(player.playerStats.maxHealth);
        playerUI.healthSlider.value 	= Convert.ToInt32(player.playerStats.health);
        playerUI.healthText.text        = Convert.ToInt32(player.playerStats.health) + " / " + Convert.ToInt32(player.playerStats.maxHealth);

        playerUI.expSlider.maxValue 	= (float) player.playerStats.maxExp;
        playerUI.expSlider.value 		= (float) player.playerStats.actualExp;

        playerUI.levelText.text = player.playerStats.level.ToString ();
        if(player.playerStats.level != GameConstants.maxLevel) {
            playerUI.expText.text = Convert.ToInt32(player.playerStats.actualExp) + " / " + Convert.ToInt32(player.playerStats.maxExp) + " EXP";
        } else {
            playerUI.expText.text = "Max. Level";
        }

		if (player.playerStats.skillPoints != 0) {
            playerUI.skillState.enabled = true;
		} else {
            playerUI.skillState.enabled = false;
		}
	}

    public void updateEnemyInfo(GameObject enemy) {

        enemyUI.enemyPanel.SetActive(true);

        BaseEnemy baseEnemy = enemy.GetComponent<BaseEnemy>();
        Debug.Log("Enemy Name: " + baseEnemy.enemyName);

        enemyUI.enemySlider.maxValue = (float)baseEnemy.maxHealth;
        enemyUI.enemySlider.value = (float)baseEnemy.health;
        enemyUI.enemyName.text = baseEnemy.enemyName;
    }

    public void resetEnemy() {
        enemyUI.enemyPanel.SetActive(false);
    }
		
	/* ---------------------------------- SkillUI -----------------------------------------------------*/
    private void fillActiveSkillUI() {

		skillDictionary = new Dictionary<string, int> ();

        // ADD Healing Skill
		GameObject defaultSlot = setupDefaultSlot();
		defaultSlot.transform.SetParent (activeSkillUI.skillPanel.transform);
		skillSlotList.Add (defaultSlot);

        // ADD Active Skills
		List<BaseSkill> activeSkills = GameObject.Find(GameConstants.GameUIs.skillUI).GetComponent<UIManager_SkillSystem>().activeSkillList;

		for (int i = 0; i < activeSkills.Count; i++) {
			BaseSkill skill = activeSkills [i];

			if (skill.level == skill.maxLevel) {
				addSkillSlot (skill, i);
			}
		}
    }

	private void updateSkillList() {
		List<BaseSkill> activeSkills = GameObject.Find(GameConstants.GameUIs.skillUI).GetComponent<UIManager_SkillSystem>().activeSkillList;

		for (int i = 0; i < activeSkills.Count; i++) {
			BaseSkill skill = activeSkills [i];

			if (skill.level == skill.maxLevel && !skillDictionary.ContainsKey(skill.identifier)) {
				addSkillSlot (skill, i);
			}
		}
	}

	private void addSkillSlot(BaseSkill skill, int slotID) {

		GameObject slot = setupSlotWithSkill (skill, slotID);
		slot.transform.SetParent (activeSkillUI.skillPanel.transform);
		skillSlotList.Add (slot);
	}

	private GameObject setupDefaultSlot() {

		GameObject slot = Instantiate (activeSkillUI.skillSlot);
		SkillSlot slotScript = slot.GetComponent<SkillSlot> ();
		slotScript.skill = null;
		slotScript.slotID = 0;

		Image img = slot.transform.FindChild ("SkillItem").gameObject.GetComponent<Image>();
		img.sprite = Resources.Load<Sprite> (GameConstants.spritePath + "Healing");

		Text skillKey = img.transform.FindChild ("Key").gameObject.GetComponent<Text> ();
		skillKey.text = "H";

		Text cooldown = img.transform.FindChild ("Cooldown").gameObject.GetComponent<Text> ();
		cooldown.text = "";

		skillDictionary.Add (GameConstants.potionIdentifier, 0);

		return slot;
	}

	private GameObject setupSlotWithSkill(BaseSkill skill, int slotID) {

		GameObject slot = Instantiate (activeSkillUI.skillSlot);
		SkillSlot slotScript = slot.GetComponent<SkillSlot> ();
		slotScript.skill = skill;
		slotScript.slotID = slotID;

		Image img = slot.transform.FindChild ("SkillItem").gameObject.GetComponent<Image>();
		img.sprite = Resources.Load<Sprite> (GameConstants.spritePath + skill.imageName);

		Text skillKey = img.transform.FindChild ("Key").gameObject.GetComponent<Text> ();
		skillKey.text = (slotID + 1).ToString();

		Text cooldown = img.transform.FindChild ("Cooldown").gameObject.GetComponent<Text> ();
		cooldown.text = "";

		skillDictionary.Add (skill.identifier, slotID + 1);

		return slot;
	}

	public void updateSkillCooldown(string skillIdentifier, int cooldown) {

		GameObject skillSlot = skillSlotList [skillDictionary [skillIdentifier]];

		Image img = skillSlot.transform.FindChild ("SkillItem").gameObject.GetComponent<Image>();
		Text cooldownText = img.transform.FindChild ("Cooldown").gameObject.GetComponent<Text> ();

		if (cooldown != 0) {
			cooldownText.text = cooldown.ToString ();
		} else {
			cooldownText.text = "";
		}
	}
}

[System.Serializable]
public class PlayerUI {
    public Slider healthSlider;
    public Text healthText;

    public Text levelText;
    public Text expText;
    public Slider expSlider;
    public Image skillState;
}

[System.Serializable]
public class EnemyUI {
    public GameObject enemyPanel;
    public Slider enemySlider;
    public Text enemyName;
}

[System.Serializable]
public class ActiveSkillUI {
    public GameObject skillPanel;
    public GameObject skillSlot;
}
