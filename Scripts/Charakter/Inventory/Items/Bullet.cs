using UnityEngine;
using System.Collections;
using System;

public class Bullet : MonoBehaviour {

    void Update() {
        if (Vector3.Distance(GameConstants.findPlayerInProject().transform.position, transform.position) > GameConstants.bulletFlyRange) {
            Destroy(this.gameObject);
        }
    }


    void OnCollisionEnter(Collision collision) {
        if (collision.transform.tag != "Enemy") {
            Destroy(this.gameObject);
        } else {
            //Debug.Log("Bullet hit an Enemy");
        }
    }
}