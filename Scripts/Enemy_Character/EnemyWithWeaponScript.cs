using System;
using UnityEngine;

public class EnemyWithWeaponScript: MonoBehaviour {

    public Weapon weapon;
    public Animator animator;
    private ListWeapons listWeapons;

    void Awake() {
        animator = GetComponent<Animator>();
    }

    void Start() {
        //GetList of Weapons
        listWeapons = GameObject.Find("RolePlayItems").GetComponent<ListWeapons>();
        initWeapon();
    }

    private void initWeapon() {
        Weapon[] weapons = this.GetComponentsInChildren<Weapon>();
        foreach (Weapon w in weapons) {
            if (w.name.Equals("Enemy_Weapon")) {
                weapon = w;
            }
        }
        //Set Weapon Visible
        if (weapon != null) {
            weapon.SetVisibility(true);
        }

        //Set Weapon Speed in Animator
        if(animator != null) {
            float speed = 1.0f;
            int id = listWeapons.getIDForWeapon(weapon);
            if(id != -1) {
                listWeapons.getWeaponSpeed().TryGetValue(listWeapons.getWeapons()[id].GetSpeed(), out speed);
            }
            animator.SetFloat("AttackSpeed", speed);
        }
    }

    void Update() {

    }
}

