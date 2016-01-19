using UnityEngine;
using System.Collections;

public class Enemy_DetectPlayer : MonoBehaviour {

	public float findPlayerRadius;
    public float stoppingDistance;

    private Vector3 startPosition;
    private Quaternion startRotation;

    private Animator animator;

    private BaseEnemy baseEnemy;
    public bool isDead = false;
    public float waitingForAttack = 0.0f;

    private bool playerGetExp = false;

    void Awake() {
        startPosition = transform.position;
        startRotation = transform.rotation;
        animator = GetComponent<Animator>();
        baseEnemy = this.GetComponent<BaseEnemy>();
    }
	
	void Update () {
		Collider[] colliders = Physics.OverlapSphere (transform.position, findPlayerRadius);
        Collider[] stopColliders = Physics.OverlapSphere(transform.position, stoppingDistance);
        Collider playerCollider = null;
        Collider stopCollider = null;

        foreach (Collider col in colliders) {
			if (col && col.tag == GameConstants.playerTag) {
                playerCollider = col;
			}
		}

        foreach (Collider col in stopColliders) {
            if (col && col.tag == GameConstants.playerTag) {
                stopCollider = col;
            }
        }

        NavMeshAgent navAgent = GetComponent<NavMeshAgent>();
        GameObject player = GameConstants.findPlayerInProject();


        if(baseEnemy.health > 0) {
            if (playerCollider != null && stopCollider == null && player.GetComponent<ToonCharacter_PlayerScript>().getBasePlayer().playerStats.health > 0) {
                //Wenn Gegner den Spieler sieht -> zum Spieler rennen
                this.transform.LookAt(GameConstants.findPlayerInProject().transform);
                Debug.Log("Detect player");
                animator.SetFloat("Walk", 1.0f);
                animator.SetFloat("underAttack", 1.0f);
                animator.SetBool("Attack", false);
                navAgent.enabled = true;
                navAgent.SetDestination(player.transform.position);
            } else {
                if (stopCollider != null && player.GetComponent<ToonCharacter_PlayerScript>().getBasePlayer().playerStats.health > 0) {
                    //Wenn Spieler in Angriffsreichweite, dann attackieren
                    this.transform.LookAt(GameConstants.findPlayerInProject().transform);

                    animator.SetFloat("Walk", 0.0f);
                    navAgent.enabled = false;
                    if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Attack")) {
                        waitingForAttack += Time.deltaTime;
                        if(waitingForAttack > GameConstants.enemyWaitingTime) {
                            //Debug.Log("Attack Player");
                            animator.SetBool("Attack", true);
                            waitingForAttack = 0.0f;
                        } else {
                            Debug.Log("Enemy waiting for Attack");
                        }
                    } else {
                        animator.SetBool("Attack", false);
                    }
                } else {
                    //Wenn Spieler nicht mehr in Angriffsreichweite
                    if (player.GetComponent<ToonCharacter_PlayerScript>().getBasePlayer().playerStats.health <= 0) {
                        animator.SetFloat("underAttack", 0.0f);
                        this.transform.LookAt(startPosition);

                        if (this.transform.position != startPosition) {
                            animator.SetFloat("Walk", 1.0f);
                            navAgent.enabled = true;
                            navAgent.SetDestination(startPosition);
                            
                            if (navAgent.remainingDistance > 0 && navAgent.remainingDistance <= 0.5) {
                                animator.SetFloat("Walk", 0.0f);
                                navAgent.enabled = false;
                                this.transform.LookAt(startRotation.eulerAngles);
                            }
                        }
                    } else {
                        animator.SetFloat("Walk", 0.0f);
                        animator.SetFloat("underAttack", 0.0f);
                        navAgent.enabled = false;
                    }
                }
            }
        } else {
            if (!isDead) {
                animator.SetBool("isDying", true);
                isDead = true;
                if (!playerGetExp) {
                    GameConstants.findPlayerInProject().GetComponent<ToonCharacter_PlayerScript>().getBasePlayer().playerStats.gainExp(this.GetComponent<BaseEnemy>().exp);
                    playerGetExp = true;
                    GameConstants.getPlayerStats().questSystem.refreshQuestSystem(baseEnemy.killType, null);
                }
            } else {
                animator.SetBool("isDying", false);
                animator.SetFloat("underAttack", 0.0f);
                animator.SetFloat("Walk", 0.0f);
                navAgent.enabled = false;
            }

        }
    }
}
