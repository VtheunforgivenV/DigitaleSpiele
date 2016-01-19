using UnityEngine;
using System.Collections;

public class ToonCharacter_DetectEnemy : ToonCharacterScript {

	public float radius;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Collider[] colliders = Physics.OverlapSphere (transform.position, radius);
        ArrayList enemyColliders = new ArrayList();

		foreach (Collider col in colliders) {
			if (col && col.tag == GameConstants.enemyTag) {
                enemyColliders.Add(col);
				Debug.Log ("Find Enemy");
			}
		}

        if (enemyColliders.Count > 0) {
            updateGameUI((Collider)enemyColliders[0]);
        } else {
            updateGameUI(null);
        }
	}

    void updateGameUI(Collider collider) {
        UIManager_Game uimanager = GameObject.Find("GameUI").GetComponent<UIManager_Game>();

        if (collider != null) {
            uimanager.updateEnemyInfo(collider.gameObject);
        } else {
            uimanager.resetEnemy();
        }
    }
}
