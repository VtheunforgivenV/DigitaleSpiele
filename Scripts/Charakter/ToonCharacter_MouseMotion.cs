using System.Collections;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class ToonCharacter_MouseMotion : ToonCharacterScript{

    public GameObject normalBullet;
    public bool spawningBullet = false;

/** ----------------------------------------------------------------------------- Methods -----------------------------------------------------------------------------------------------------*/

/** -------------------------------- Standard Methods ------------------------------*/

    void Awake(){
        //Bind Animator
        animator = this.GetComponent<Animator>();
        //Lock Cursor to Middle
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Start(){}

    // Update is called once per frame
    void Update () {
        if (Time.timeScale != 0) {
            cameraRotation();
            mouseTracking();
            setAnimatorVariables(this);
        }
    }

/** -------------------------------- Other Methods ------------------------------*/

    void mouseTracking(){
        if (Input.GetMouseButton(0)){
            if(this.GetComponent<ToonCharacter_Inventory>().getPlayerWeapon() != null) {
                //Left-Click
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Attack")) {
                    attack = true;
                    if(this.gameObject.GetComponent<ToonCharacter_PlayerScript>().getBasePlayer().playerType != PlayerType.MELEE) {
                        if(!spawningBullet) {
                            StartCoroutine(spawnbullet());
                            spawningBullet = true;
                        }
                    }
                } else {
                    attack = false;
                }
            }

        }else{
            attack = false;
        }

        if (Input.GetMouseButton(1)){
            //Right-Click
        }else if (Input.GetMouseButton(2)){
            //Middle-Click
        }
    }
    
    public void cameraRotation() {
        float yRot = CrossPlatformInputManager.GetAxis("Mouse X") * XSensitivity;
        float xRot = CrossPlatformInputManager.GetAxis("Mouse Y") * YSensitivity;

        this.gameObject.transform.localRotation *= Quaternion.Euler(0f, yRot, 0f);
        Camera.main.transform.localRotation *= Quaternion.Euler(-xRot, 0f, 0f);

        Camera.main.transform.localRotation = ClampRotationAroundXAxis(Camera.main.transform.localRotation);

        this.gameObject.transform.localRotation = this.gameObject.transform.localRotation;
        Camera.main.transform.localRotation = Camera.main.transform.localRotation;
     
    }


    Quaternion ClampRotationAroundXAxis(Quaternion q) {
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;

        float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);
        angleX = Mathf.Clamp(angleX, MinimumX, MaximumX);

        q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

        return q;
    }

    IEnumerator spawnbullet() {
        float attackLength = GameConstants.getAnimationLength(animator, "Attack", "Attack1");
        yield return new WaitForSeconds(attackLength);

        GameObject bulletSpawnPoint = this.gameObject.GetComponentInChildren<BulletSpawnPoint>().gameObject;
        GameObject bulletObject = Instantiate(normalBullet);
        bulletObject.transform.position = bulletSpawnPoint.gameObject.transform.position;

        Vector3 target;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity)) {
                target = hit.point - bulletSpawnPoint.transform.position;
                bulletObject.GetComponent<Rigidbody>().AddForce(target.normalized * GameConstants.bulletFlyTime, ForceMode.VelocityChange);
        }

        spawningBullet = false;
    }



}

