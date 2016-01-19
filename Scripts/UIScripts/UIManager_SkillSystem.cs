using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class UIManager_SkillSystem : UIManager_BaseGame {

	private GameObject skillPanel;
	private GameObject activeSkillPanel;
	private GameObject passiveSkillPanel;

	public Text skillPointsText;
	public GameObject skillSlot;

	public List<BaseSkill> skillList;

	public List<BaseSkill> activeSkillList;
	public List<GameObject> activeSkillSlots;

	public List<BaseSkill> passiveSkillList;
	public List<GameObject> passiveSkillSlots;

	new void Start() {

		base.Start ();
        // was denn nur ??? 

		this.loadSkillList ();
		fillPanel ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Tab)) {
			this.activateMenu (!this.info.GetComponentInParent<Canvas>().isActiveAndEnabled);
		}

		BasePlayer baseplayer = GameConstants.findPlayerInProject ().GetComponent<ToonCharacter_PlayerScript> ().getBasePlayer ();
		skillPointsText.text = "Fähigkeitspunkte: " + baseplayer.playerStats.skillPoints;
	}

	private void loadSkillList() {
		DataManager manager = new DataManager ();

		BasePlayer baseplayer = GameConstants.findPlayerInProject ().GetComponent<ToonCharacter_PlayerScript> ().getBasePlayer ();

		switch(baseplayer.playerType) {

		case PlayerType.MELEE:
			skillList = manager.readFile<List<BaseSkill>> (GameConstants.skillList_Melee_Filename);
			break;

		case PlayerType.DISTANCE:
			skillList = manager.readFile<List<BaseSkill>> (GameConstants.skillList_Archer_Filename);
			break;

		case PlayerType.MAGIC:
			skillList = manager.readFile<List<BaseSkill>> (GameConstants.skillList_Mage_Filename);
			break;
		}

		activeSkillList = new List<BaseSkill> ();
		passiveSkillList = new List<BaseSkill> ();

        Dictionary<string, int> skillLevel = baseplayer.playerStats.skillLevel;

		// extract active and passive Skills
		foreach (BaseSkill skill in skillList) {

            if (skillLevel.ContainsKey(skill.identifier)) {
                skill.level = skillLevel[skill.identifier];
            }

			if (skill.type == Skill.type.AKTIVE) {
				activeSkillList.Add (skill);
			} else {
				passiveSkillList.Add (skill);
			}
		}
	}

	private void fillPanel() {
		skillPanel = GameObject.Find (panel_name);

		// set active Skills 
		activeSkillPanel = skillPanel.transform.FindChild ("activeSkill_Panel").gameObject;

		for (int i = 0; i < activeSkillList.Count; i++) {
			
			BaseSkill skill = activeSkillList [i];

			GameObject slot = setupSlotWithSkill (skill, i);

			slot.transform.SetParent (activeSkillPanel.transform);
			activeSkillSlots.Add (slot);
		}

		// set passive Skills
		passiveSkillPanel = skillPanel.transform.FindChild("passiveSkill_Panel").gameObject;

		for (int i = 0; i < passiveSkillList.Count; i++) {
			BaseSkill skill = passiveSkillList [i];

			GameObject slot = setupSlotWithSkill (skill, i);

			slot.transform.SetParent (passiveSkillPanel.transform);
			passiveSkillSlots.Add (slot);
		}
	}

	private GameObject setupSlotWithSkill(BaseSkill skill, int slotID) {
		
		GameObject slot = Instantiate (skillSlot);
		SkillSlot slotScript = slot.GetComponent<SkillSlot> ();
		slotScript.skill = skill;
		slotScript.slotID = slotID;

		Image img = slot.transform.FindChild ("SkillItem").gameObject.GetComponent<Image>();
		img.sprite = Resources.Load<Sprite> (GameConstants.spritePath + skill.imageName);

		Text skillText = img.GetComponentInChildren<Text>();
		skillText.text = skill.level + " / " + skill.maxLevel;

		return slot;
	}
		
}
