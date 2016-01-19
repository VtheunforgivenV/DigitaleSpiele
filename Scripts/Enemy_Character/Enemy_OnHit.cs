using UnityEngine;

public class Enemy_OnHit : MonoBehaviour {
    Weapon playerWeapon;
    Bullet playerBullet;

    void OnCollisionEnter(Collision collision) {
        Debug.Log("Enemy was hit by Collision:" + collision.transform.name);
        playerBullet = collision.gameObject.GetComponent<Bullet>();

        if(playerBullet == null) {
            playerWeapon = (Weapon)GameConstants.findPlayerInProject().GetComponent<ToonCharacter_Inventory>().getPlayerWeapon();
        }

        if (playerBullet != null) {
            this.GetComponent<BaseEnemy>().health -= GameConstants.getPlayerAttackDamage();
            Destroy(collision.gameObject);
        } else if (playerWeapon != null) {
            if (collision.transform.name.Equals(playerWeapon.transform.name)) {
                //Make Enemy Damage from Melee
                if (collision.transform.gameObject.GetComponent<Inventory>() is IWeapon) {
                    if (GameConstants.findPlayerInProject().GetComponent<ToonCharacter_DamageEnemyOneTime>().isAttacking) {
                        if (!GameConstants.findPlayerInProject().GetComponent<ToonCharacter_DamageEnemyOneTime>().oneHit) {
                            this.GetComponent<BaseEnemy>().health -= GameConstants.getPlayerAttackDamage();
                        }

                        //Set Only One Time Damage per Animation
                        GameConstants.findPlayerInProject().GetComponent<ToonCharacter_DamageEnemyOneTime>().oneHit = true;
                    }
                }
            }
        } 
    }
}
