using System.Collections;
using UnityEngine;
using System.Timers;

public class ToonCharacter_KeyboardMotion : ToonCharacterScript {

    public bool activatedPotion = false;

	public int potionCooldown;
	private UIManager_Game uiManagerGame;

	private Timer potionTimer;

/** ----------------------------------------------------------------------------- Methods -----------------------------------------------------------------------------------------------------*/

/** -------------------------------- Standard Methods ------------------------------*/

    void Awake(){
        //Bind Animator
        animator = this.GetComponent<Animator>();
    }

	void Start() {
	
		uiManagerGame = GameObject.Find (GameConstants.gameUI).gameObject.GetComponent<UIManager_Game> ();
	
	}

    void Update () {
        MotionTracking();
        skillTracking();
        setAnimatorVariables(this);
    }

	void OnDestroy() {
		potionTimer.Stop ();
	}

/** -------------------------------- Other Methods ------------------------------*/

    void MotionTracking(){
        //Forward
        forwardSpeed = Input.GetAxis("Vertical");
        //Turn
        turnSpeed = Input.GetAxis("Horizontal");
        
        //Run
        if (Input.GetKey(KeyCode.LeftShift)){
            run = true;
        }else{
            run = false;
        }

        //Sneak
        if (Input.GetKey(KeyCode.C)){
            sneak = true;
        }else{
            sneak = false;
        }
    }

    void skillTracking() {
        if (Input.GetKeyDown(KeyCode.H)) {
            if (!activatedPotion) {
                StartCoroutine(healPlayer());
            }
        }

        if (Input.GetKeyDown(KeyCode.P)) {
            this.gameObject.transform.position = GameObject.Find("PortPoint").transform.position;
        }
    }

    IEnumerator healPlayer() {
        activatedPotion = true;
        GameConstants.getPlayerStats().getHealingFromPotion();

		potionCooldown = 20;
		potionTimer = new Timer ();
		potionTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
		potionTimer.Interval = 1000;
		potionTimer.Enabled = true;
		uiManagerGame.updateSkillCooldown (GameConstants.potionIdentifier, potionCooldown);

		yield return new WaitForSeconds (GameConstants.potionCooldown);

		activatedPotion = false;
    }

	private void OnTimedEvent(object source, ElapsedEventArgs e)
	{
		potionCooldown--;
		Debug.Log (potionCooldown.ToString ());

		if (potionCooldown < 0) {
			(source as Timer).Stop ();
			Debug.Log ("Stop Timer");
		}
	}

}
