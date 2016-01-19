using UnityEngine;
using System.Collections;

public class LoadPlayer : MonoBehaviour {
    
    private GameObject playerObject = null;

    public GameObject prefabObjectMelee;
    public GameObject prefabObjectArcher;
    public GameObject prefabObjectMage;

	// Use this for initialization
	void Start () {
		this.loadPlayer();
	}

	public void loadPlayer () {
		DataManager dataManager = new DataManager ();
        BasePlayer basePlayer = dataManager.readFile<BasePlayer> (GameConstants.saveFileName);
        
        switch (basePlayer.playerType) {
            case PlayerType.MELEE:
                playerObject = instantiatePlayerObject(prefabObjectMelee, basePlayer);
                break;
            case PlayerType.DISTANCE:
                playerObject = instantiatePlayerObject(prefabObjectArcher, basePlayer);
                break;
            case PlayerType.MAGIC:
                playerObject = instantiatePlayerObject(prefabObjectMage, basePlayer);
                break;
        }
        playerObject.transform.SetParent(this.gameObject.transform);
        playerObject.GetComponent<ToonCharacter_PlayerScript>().setBasePlayer(basePlayer);
        playerObject.name = playerObject.name.Split('(')[0];
    }

    private GameObject instantiatePlayerObject(GameObject prefab, BasePlayer basePlayer) {
        if (GameConstants.newGame) {
            return (GameObject)Instantiate(prefab, this.transform.position, this.transform.rotation);
        } else {
               return (GameObject)Instantiate(prefab, new Vector3((float)basePlayer.positionX,
                    (float)basePlayer.positionY, (float)basePlayer.positionZ),
                    Quaternion.Euler((float)basePlayer.rotationX, (float)basePlayer.rotationY, (float)basePlayer.rotationZ));
        }
    }

}
