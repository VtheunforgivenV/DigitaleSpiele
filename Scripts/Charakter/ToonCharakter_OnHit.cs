using UnityEngine;

public class ToonCharakter_OnHit : MonoBehaviour {

    PlayerStats playerStats;

    void Update() {
        playerStats = GameConstants.findPlayerInProject().GetComponent<ToonCharacter_PlayerScript>().getBasePlayer().playerStats;
    }

    void OnCollisionEnter(Collision collision) {
        Weapon enemyWeapon = (Weapon)collision.gameObject.GetComponent<Weapon>();

        if (collision.gameObject.GetComponentInParent<ToonCharacter_PlayerScript>()) {
            enemyWeapon = null;
        }

        if (enemyWeapon != null) {
			
            if (collision.transform.name.Equals(enemyWeapon.transform.name)) {
                Debug.Log("By Object: " + collision.transform.name);

                //Make Enemy Damage from Melee
                if (collision.transform.gameObject.GetComponent<Inventory>() is IWeapon) {
                    
                    if (collision.gameObject.GetComponentInParent<Enemy_DamageEnemyOneTime>().isAttacking) {
						
                        if (!collision.gameObject.GetComponentInParent<Enemy_DamageEnemyOneTime>().oneHit) {
                            //Debug.Log("HP before Hit: " + playerStats.health);
                            playerStats.health -= (collision.gameObject.GetComponentInParent<BaseEnemy>().damage * playerStats.damageReduction);

							if (playerStats.health < 0) {
								playerStats.health = 0.0;
							}
                            //Debug.Log("HP after Hit: " + playerStats.health);
                        }

                        //Set Only One Time Damage per Animation
                        collision.gameObject.GetComponentInParent<Enemy_DamageEnemyOneTime>().oneHit = true;
                    }
                }
            }
        }
    }
}
