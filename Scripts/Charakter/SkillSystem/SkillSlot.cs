using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {

	public int slotID;
	public BaseSkill skill;

	private UIManager_SkillSystem uiManager;
    private PlayerStats playerStats;
	private SkillToolTip toolTip;

	// Use this for initialization
	void Start () {
		uiManager = GameObject.Find(GameConstants.GameUIs.skillUI).GetComponent<UIManager_SkillSystem>();
		toolTip = GameObject.Find(GameConstants.GameUIs.skillUI).GetComponent<SkillToolTip>();
	}
	
	public void OnPointerEnter(PointerEventData eventData){
		toolTip.Activate(skill);
	}

	public void OnPointerExit(PointerEventData eventData){
		toolTip.Deactivate();
	}

	public void OnPointerClick(PointerEventData eventData){
        playerStats = GameConstants.findPlayerInProject().GetComponent<ToonCharacter_PlayerScript>().getBasePlayer().playerStats;

        if (!checkSkillPoints()) {
            return;
        }

        if (!checkMaxLevel()) {
            return;
        }

        skill.level++;

        switch (skill.passiveSkillType) {
            case Skill.passiveSkillType.WEAPON_LEVEL:
                playerStats.weaponLevel++;
                break;
            case Skill.passiveSkillType.DYNAMIC_STRENGTH:
                playerStats.playerDamageMultiplier = 1 + (skill.level * skill.damageMultiplier);
                break;
            case Skill.passiveSkillType.DYNAMIC_HEALTH:
                playerStats.playerHealthMultiplier = 1 + (skill.level * skill.healthMultiplier);
                break;
            case Skill.passiveSkillType.DYNAMIC_REDUCTION:
                playerStats.playerDamageReductionMultiplier = 0 + (skill.level * skill.damageReductionMultiplier);
                break;
            case Skill.passiveSkillType.STATIC_STRENGTH:
                playerStats.playerStaticDamage = (skill.level * skill.staticDamage);
                break;
            case Skill.passiveSkillType.STATIC_HEALTH:
                playerStats.playerStaticHealth = (skill.level * skill.staticHealth);
                break;
        }

        Text skillText = transform.FindChild("SkillItem").gameObject.GetComponent<Image>().GetComponentInChildren<Text>();
        skillText.text = skill.level + " / " + skill.maxLevel;

        playerStats.skillPoints--;

        playerStats.recalculatePlayerStats();
    }

	private bool checkSkillPoints() {

        if (playerStats.skillPoints == 0) {
            uiManager.showMessageBox("Keine Fähigkeitspunkte verfügbar");
            return false;
        }

		return true;
	}

    private bool checkMaxLevel() {

        if (skill.level == skill.maxLevel) {
            uiManager.showMessageBox("Maximales Fähigkeitenlevel erreicht");
            return false;
        }

        return true;
    }
}
