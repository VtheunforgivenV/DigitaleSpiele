using UnityEngine;
using System.Collections;
using System;

public class GameConstants {

	//Filenames
	public const string saveFileName = "save.json";
	public const string skillList_Melee_Filename = "SkillList_Melee.json";
	public const string skillList_Archer_Filename = "SkillList_Archer.json";
	public const string skillList_Mage_Filename = "SkillList_Mage.json";

    public const string spritePath = "Icons/";

	//Scenenames
	public const string sceneMainMenu 		= "Hauptmenu_Szene";
	public const string sceneChossePlayer 	= "Choose_Player_Szene";
	public const string sceneGame			= "Spiel_Szene";
    
	// Tags
	public const string playerTag		= "Player";
	public const string playerParentTag	= "PlayerParent";
	public const string enemyTag		= "Enemy";
	public const string environmentTag 	= "Environment";
	public const string weaponTag		= "Weapon";
	public const string chestTag		= "Chest";

	// UIManager bzw Canvas

	public struct GameUIs {
		public const string skillUI	    = "SkillUI";
		public const string menuUI      = "MenuUI";
	}

	public const string gameUI      = "GameUI";
    public const string inventoryUI = "InventoryUI";
	public const string respawnUI   = "RespawnUI";


	// Important GameObjects
	public const string rolePlayItems = "RolePlayItems";

    public static Boolean newGame = false;

    public const int maxLevel = 20;
    public const int startMaxExp = 100;

    public const float enemyWaitingTime = 1.0f;

    public const int bulletFlyTime = 50;
    public const float bulletFlyRange = 100.0f;

	public const string potionIdentifier = "Healing";
    public const float potionCooldown = 20.0f;
    public const double potionHealingRate = 0.6;

    public const double traderValueMultiplier = 3;

    // --------------------------- Methods Player---------------------------

    public static GameObject findPlayerInProject() {
        GameObject playerObject = null;
		GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(playerTag);
        for (int i = 0; i < gameObjects.Length; i++) {
            if (gameObjects[i].name.StartsWith("PlayerCharacter_") && !gameObjects[i].name.EndsWith("(Clone)")) {
                playerObject = gameObjects[i];
            }
        }
        return playerObject;
    }

    public static ToonCharacter_Inventory getPlayerInventory() {
        return findPlayerInProject().GetComponent<ToonCharacter_Inventory>();
    }

    public static PlayerStats getPlayerStats() {
        return findPlayerInProject().GetComponent<ToonCharacter_PlayerScript>().getBasePlayer().playerStats;
    }

    public static int getPlayerLevel() {
        return findPlayerInProject().GetComponent<ToonCharacter_PlayerScript>().getBasePlayer().playerStats.level;
    }
	
		public static ListWeapons getListWeapons() {
		return GameObject.Find (rolePlayItems).GetComponent<ListWeapons> ();
	}

    public static double getPlayerLevelMultiplier() {
        double multiplier = 1;
        multiplier += 0.05 * (getPlayerLevel() - 1);
        return multiplier;
    }

    public static double getEnemyDamageMultiplier() {
        double multiplier = 1;
        multiplier += 0.1 * (getPlayerLevel() - 1);
        return multiplier;
    }

    public static double getEnemyExpMultiplier() {
        double multiplier = 1;
        multiplier += 0.1 * (getPlayerLevel() - 1);
        return multiplier;
    }

    public static double getPlayerAttackDamage() {
        return (getPlayerStats().strength + getPlayerInventory().getPlayerWeapon().GetDamage());
    }

    public static float getAnimationLength(Animator animator, string stateName, string animationClipname) {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(stateName)) {
            return animator.GetCurrentAnimatorStateInfo(0).length;
        } else {
            RuntimeAnimatorController ac = animator.runtimeAnimatorController;
            for (int i = 0; i < ac.animationClips.Length; i++) {
                if (ac.animationClips[i].name == animationClipname) {
                    return ac.animationClips[i].length;
                }
            }
        }
        return 1;
    }

    public static bool mapStringToClass(string text) {
        switch (text) {
            case "Nahkämpfer":
                if (findPlayerInProject().GetComponent<ToonCharacter_PlayerScript>().getBasePlayer().playerType == PlayerType.MELEE) {
                    return true;
                } else {
                    return false;
                }
            case "Fernkämpfer":
                if (findPlayerInProject().GetComponent<ToonCharacter_PlayerScript>().getBasePlayer().playerType == PlayerType.DISTANCE) {
                    return true;
                } else {
                    return false;
                }
            case "Zauberer":
                if (findPlayerInProject().GetComponent<ToonCharacter_PlayerScript>().getBasePlayer().playerType.Equals(PlayerType.MAGIC)) {
                    return true;
                } else {
                    return false;
                }
            default:
                return true;
        }
    }
}

// ------------- Enums -------------------------
public enum PlayerType{
	MELEE, DISTANCE, MAGIC
}

public struct Skill {
    public enum type {
        AKTIVE, PASSIVE
    }

    public enum passiveSkillType {
        DYNAMIC_HEALTH = 0,
        DYNAMIC_STRENGTH = 1,
        DYNAMIC_REDUCTION = 2,
        WEAPON_LEVEL = 3,
        STATIC_HEALTH = 4,
        STATIC_STRENGTH = 5,
        NONE = 6
    }
}

public enum WeaponLevel {
	SMALL, MIDDLE, BIG
}

public enum EnemyType {
    MELEE, DISTANCE, MAGIC
}

public enum DiffcultType {
    NORMAL, ELITE, LEGEND
}