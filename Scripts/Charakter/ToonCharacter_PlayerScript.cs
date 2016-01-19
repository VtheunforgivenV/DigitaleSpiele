using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class ToonCharacter_PlayerScript : MonoBehaviour{

    private BasePlayer basePlayer;


/** ----------------------------------------------------------------------------- Methods -----------------------------------------------------------------------------------------------------*/

/** -------------------------------- Standard Methods ------------------------------*/

    void Start(){
        if (basePlayer == null) {
            Debug.Log("BasePlayer is null");
        }
    }
    
/** -------------------------------- Get/Set Methods ------------------------------*/

    public BasePlayer getBasePlayer() {
        return basePlayer;
    }

    public void setBasePlayer(BasePlayer basePlayer) {
        this.basePlayer = basePlayer;
    }

}

