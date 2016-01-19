using UnityEngine;
using System.Collections;

public class Skeleton_Melee: BaseEnemy {

    public double defaultHealth = 75;
    public double defaultStrength = 1;
    public double defaultExp = 25;

    void Start() {
        killType = KillType.Skeleton;

        enemyType = EnemyType.MELEE;
        difficultType = GetComponent<EnemyDiffcult>().type;


        switch (difficultType) {
            case DiffcultType.NORMAL:
                enemyName = "Skelettkrieger";
                break;
            case DiffcultType.ELITE:
                enemyName = "Elite Skelettkrieger";
                break;
            case DiffcultType.LEGEND:
                enemyName = "Legendärer Skelettkrieger";
                break;
        }
        
        maxHealth = calculateMaxHealth();
        health = maxHealth;
    }
    
    void Update(){
        if(health == maxHealth) {
            maxHealth = calculateMaxHealth();
            health = maxHealth;
        } else {
            double procentHealth = health / maxHealth;
            maxHealth = calculateMaxHealth();
            health = maxHealth * procentHealth;
        }

        damage = calculateMeleeStrength() * GetComponent<EnemyWithWeaponScript>().weapon.GetDamage();

        exp = calculateExp();
    }

    double calculateMaxHealth() {
        return defaultHealth * GameConstants.getPlayerLevelMultiplier() * GetComponent<EnemyDiffcult>().healthMultiplier;
    }

    double calculateMeleeStrength() {
        return defaultStrength * GameConstants.getEnemyDamageMultiplier() * GetComponent<EnemyDiffcult>().damageMultiplier;
    }

    double calculateExp() {
        return defaultExp * GameConstants.getEnemyExpMultiplier() * GetComponent<EnemyDiffcult>().expMultiplier;
    }

}

