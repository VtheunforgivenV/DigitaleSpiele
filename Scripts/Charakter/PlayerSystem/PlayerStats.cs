using UnityEngine;
using System.Collections.Generic;
using System;

public class PlayerStats {

    public double baseHealth;
    public double baseDamage;
    public double baseDamageReduction;

    public int level;
    public double actualExp;
    public double maxExp;

    public double maxHealth;
    public double health;

    public double strength;
	public double damageReduction;

    public int skillPoints;

	public Dictionary<string, int> skillLevel;

    public double goldAmount;

    public int actualWeapon;
    public List<int> inventoryDictionary;

    public QuestSystem questSystem;

    //Vars SkillSystem
    public WeaponLevel weaponLevel;
    public double playerDamageMultiplier = 1;
    public double playerHealthMultiplier = 1;
    public double playerDamageReductionMultiplier = 1;
    public double playerStaticDamage = 0;
    public double playerStaticHealth = 0;


    // ------------------------------ Constructor ---------------------------------------------------
    public PlayerStats() {
        level = 1;
        actualExp = 0;
        maxExp = GameConstants.startMaxExp;
        skillPoints = 0;
        goldAmount = 0;
        inventoryDictionary = new List<int>();
        actualWeapon = -1;
        questSystem = new QuestSystem();

        baseHealth = maxHealth;
        baseDamage = strength;
        baseDamageReduction = damageReduction;
    }

    // ------------------------------ Level up ------------------------------------------------------
    public void performLevelUp() {
        if(level < GameConstants.maxLevel) {
            level++;
            actualExp -= maxExp;
            maxExp = calculateMaxExp();
            skillPoints++;

            strength = calculateStrength();
            gainHealth();

            if (level == GameConstants.maxLevel) {
                actualExp = maxExp;
            }
        }
    }

    public void recalculatePlayerStats() {
        gainHealth();
        calculateStrength();
        calculateDamageReduction();
    }

    // ------------------------------ EXP ------------------------------------------------------------
    public void gainExp(double exp) {
        actualExp += exp;
        while (actualExp >= maxExp && level != GameConstants.maxLevel) {
            performLevelUp();
        }
    }

    private double calculateMaxExp() {
        double multiplier = 1;
        if (level <= 10) {
            multiplier = 1.5;
        }else if(level > 10 && level < 15) {
            multiplier = 1.3;
        } else {
            multiplier = 1.15;
        }
        return maxExp * multiplier;
    }

    // ----------------------------- Health ---------------------------------------------------------
    private double calculateMaxHealth() {
        return ((baseHealth * ((level-1) * 1.25)) + playerStaticHealth) * playerHealthMultiplier;
    }

    private void gainHealth() {
        double healthPercentage = health / maxHealth;

        maxHealth = calculateMaxHealth();
        health = maxHealth * healthPercentage;
    }

    public void getHealingFromPotion() {
        double healingRate = maxHealth * GameConstants.potionHealingRate;
        health += healingRate;
        if(health > maxHealth) {
            health = maxHealth;
        }
    }

    // ---------------------------- Strength -------------------------------------------------------
    private double calculateStrength() {
        return ((baseDamage * ((level-1) * 1.1)) + playerStaticDamage) * playerDamageMultiplier;
    }

    // ---------------------------- DamageReduction -------------------------------------------------------

    private double calculateDamageReduction() {
        return baseDamageReduction - (baseDamageReduction * playerDamageReductionMultiplier);
    }

    // ---------------------------- Money ----------------------------------------------------------

    public void gainMoney(double moneyGain) {
        goldAmount += moneyGain;
    }

    public void looseMoney(double moneyLoose) {
        goldAmount -= moneyLoose;
    }

    public double getActualGoldAmount() {
        return goldAmount;
    }

	// ---------------------------- Skills ---------------------------------------------------------
	public void initSkillLevel(PlayerType type) {
		List<BaseSkill> skillList = loadSkillList (type);

		skillLevel = new Dictionary<string, int> ();

		foreach (BaseSkill skill in skillList) {
			skillLevel.Add (skill.identifier, skill.level);
		}
	}

    public void updateSkillLevel() {
		List<BaseSkill> skillList = GameObject.Find(GameConstants.GameUIs.skillUI).GetComponent<UIManager_SkillSystem>().skillList;

        skillLevel = new Dictionary<string, int> ();

        foreach (BaseSkill skill in skillList) {
            skillLevel.Add(skill.identifier, skill.level);
        }

    }

	private List<BaseSkill> loadSkillList(PlayerType type) {
		DataManager manager = new DataManager ();

		switch (type) {

			case PlayerType.MELEE:
				return manager.readFile<List<BaseSkill>> (GameConstants.skillList_Melee_Filename);

			case PlayerType.MAGIC:
				return manager.readFile<List<BaseSkill>> (GameConstants.skillList_Mage_Filename);

			case PlayerType.DISTANCE:
				return manager.readFile<List<BaseSkill>> (GameConstants.skillList_Archer_Filename);
		}

		return null;
	}


    public void updateInventoryDictionary() {
        inventoryDictionary = new List<int>();

        Dictionary<int, Inventory> inventory = GameConstants.getPlayerInventory().inventory;
        
        for(int i = 0; i < inventory.Count; i++) {
            inventoryDictionary.Add(inventory[i].GetID());
        }
    }
}

