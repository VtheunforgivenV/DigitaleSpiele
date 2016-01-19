using UnityEngine;
using System.Collections.Generic;

public class ListWeapons : MonoBehaviour {

/** ----------------------------------------------------------------------------- Variables -----------------------------------------------------------------------------------------------------*/
    Dictionary<int, float> weaponSpeed;
    Dictionary<int, IWeapon> weaponDictionary;
    Dictionary<int, Shield> shieldDictionary;

    //small Weapons
    private Weapon wood_sword_small;
    private Weapon wood_sword_big;
    private Weapon sword_1_small;
    private Weapon bow_small;
    private Wand wand_small;
    //middle Weapons
    private Weapon sword_1_middle;
    private Weapon sword_2_small;
    private Weapon mace_1;
    private Weapon axe;
    private Wand wand_middle;
    //big Weapons
    private Weapon sword_1_big;
    private Weapon sword_2_big;
    private Weapon mace_2;
    private Weapon axe_big;
    private Weapon bow_big;
    private Wand wand_big;

    //Shields
    private Shield shield_small;
    private Shield shield_big;

    List<Inventory> weaponList_small;
    List<Inventory> weaponList_middle;
    List<Inventory> weaponList_big;
    List<Inventory> shieldList;
    
/** ----------------------------------------------------------------------------- Methods -----------------------------------------------------------------------------------------------------*/

    void Start(){
        //small Weapons
        wood_sword_small = GameObject.Find("Wood_Sword_Small").GetComponent<Weapon>();
        wood_sword_big = GameObject.Find("Wood_Sword_Big").GetComponent<Weapon>();
        sword_1_small = GameObject.Find("Sword_1_Small").GetComponent<Weapon>();
        bow_small = GameObject.Find("Bow_Small").GetComponent<Weapon>();
        wand_small = GameObject.Find("Wand_Small").GetComponent<Wand>();
        //middle Weapons
        sword_1_middle = GameObject.Find("Sword_1_Middle").GetComponent<Weapon>();
        sword_2_small = GameObject.Find("Sword_2_Small").GetComponent<Weapon>();
        mace_1 = GameObject.Find("Mace_1").GetComponent<Weapon>();
        axe = GameObject.Find("Axe").GetComponent<Weapon>();
        wand_middle = GameObject.Find("Wand_Middle").GetComponent<Wand>();
        //big Weapons
        sword_1_big = GameObject.Find("Sword_1_Big").GetComponent<Weapon>();
        sword_2_big = GameObject.Find("Sword_2_Big").GetComponent<Weapon>();
        mace_2 = GameObject.Find("Mace_2").GetComponent<Weapon>();
        axe_big = GameObject.Find("Axe_Big").GetComponent<Weapon>();
        bow_big = GameObject.Find("Bow_Big").GetComponent<Weapon>();
        wand_big = GameObject.Find("Wand_Big").GetComponent<Wand>();
        //Shields
        shield_small = GameObject.Find("Shield_Small").GetComponent<Shield>();
        shield_big = GameObject.Find("Shield_Big").GetComponent<Shield>();
        //init Lists and Dictionaries
        initListsAndDictionaries();
             
    }

/** -------------------------------- init-Methods ------------------------------*/
    void initListsAndDictionaries() {
        initSmallWeaponList();
        initMiddleWeaponList();
        initBigWeaponList();
        initShieldList();
        initSpeedDictionary();
        initWeaponDictionary();
        initShieldDictionary();
    }

    void initSmallWeaponList(){
        weaponList_small = new List<Inventory>();
        weaponList_small.Add(wood_sword_small);
        weaponList_small.Add(wood_sword_big);
        weaponList_small.Add(sword_1_small);
        weaponList_small.Add(bow_small);
        weaponList_small.Add(wand_small);
    }

    void initMiddleWeaponList(){
        weaponList_middle = new List<Inventory>();
        weaponList_middle.Add(sword_1_middle);
        weaponList_middle.Add(sword_2_small);
        weaponList_middle.Add(mace_1);
        weaponList_middle.Add(axe);
        weaponList_middle.Add(wand_middle);
    }

    void initBigWeaponList(){
        weaponList_big = new List<Inventory>();
        weaponList_big.Add(sword_1_big);
        weaponList_big.Add(sword_2_big);
        weaponList_big.Add(mace_2);
        weaponList_big.Add(axe_big);
        weaponList_big.Add(bow_big);
        weaponList_big.Add(wand_big);
    }

    void initShieldList(){
        shieldList = new List<Inventory>();
        shieldList.Add(shield_small);
        shieldList.Add(shield_big);
    }

    void initSpeedDictionary(){
        if (weaponSpeed == null){
            weaponSpeed = new Dictionary<int, float>();
        }else{
            return;
        }
        weaponSpeed.Add(1, 0.9f);
        weaponSpeed.Add(2, 1.2f);
        weaponSpeed.Add(3, 1.6f);
        weaponSpeed.Add(4, 2.0f);
    }

    void initWeaponDictionary(){
        if (weaponDictionary == null){
            weaponDictionary = new Dictionary<int, IWeapon>();
        }else{
            return;
        }

        int idToAdd = 0;
        foreach (Inventory weapon in weaponList_small){
            idToAdd = weaponList_small.IndexOf(weapon);
            ((Inventory)weapon).SetID(idToAdd);
            weaponDictionary.Add(idToAdd, (IWeapon) weapon);
        }
        foreach (Inventory weapon in weaponList_middle){
            idToAdd = (weaponList_middle.IndexOf(weapon) + weaponList_small.Count);
            ((Inventory)weapon).SetID(idToAdd);
            weaponDictionary.Add(idToAdd, (IWeapon) weapon);
        }
        foreach (Inventory weapon in weaponList_big){
            idToAdd = (weaponList_big.IndexOf(weapon) + weaponList_middle.Count + weaponList_small.Count);
            ((Inventory)weapon).SetID(idToAdd);
            weaponDictionary.Add(idToAdd, (IWeapon) weapon);
        }
    }

    void initShieldDictionary(){
        if (shieldDictionary == null){
            shieldDictionary = new Dictionary<int, Shield>();
        }else{
            return;
        }
        foreach (Shield shield in shieldList){
            shieldDictionary.Add(shieldList.IndexOf(shield), shield);
        }
    }

/** -------------------------------- Calculate-Methods ------------------------*/

    public int getIDForWeapon(Weapon weapon) {
        IWeapon searchWeapon = null;

        //Search in Small
        foreach(IWeapon w in getSmallWeaponList()) {
            if (((Inventory)w).GetName().Equals(weapon.GetName())) {
                searchWeapon = w;
            }
        }
        if(searchWeapon != null) {
            if (getSmallWeaponList().Contains((Inventory)searchWeapon)) {
                return getSmallWeaponList().IndexOf((Inventory)searchWeapon);
            }
        }

        //Search in Middle
        foreach (IWeapon w in getMiddleWeaponList()) {
            if (((Inventory)w).GetName().Equals(weapon.GetName())) {
                searchWeapon = w;
            }
        }
        if (searchWeapon != null) {
            if (getMiddleWeaponList().Contains((Inventory)searchWeapon)) {
                return (getSmallWeaponList().Count + getMiddleWeaponList().IndexOf((Inventory)searchWeapon));
            }
        }

        //Search in Big
        foreach (IWeapon w in getBigWeaponList()) {
            if (((Inventory)w).GetName().Equals(weapon.GetName())) {
                searchWeapon = w;
            }
        }
        if (searchWeapon != null) {
            if (getBigWeaponList().Contains((Inventory)searchWeapon)) {
                return (getSmallWeaponList().Count + getMiddleWeaponList().Count + getBigWeaponList().IndexOf((Inventory)searchWeapon));
            }
        } 

        Debug.Log("Weapon not Found");
        return -1;   
    }

/** -------------------------------- Get-Methods ------------------------------*/
    public Dictionary<int, float> getWeaponSpeed(){ 
        initSpeedDictionary();
        return weaponSpeed;
    }

    public Dictionary<int, IWeapon> getWeapons(){
        initWeaponDictionary();
        return weaponDictionary;
    }

    public Dictionary<int, Shield> getShields(){ 
        initShieldDictionary();
        return shieldDictionary;
    }   

    public int smallWeaponListSize(){
        return this.weaponList_small.Count;
    }

    public int middleWeaponListSize(){
        return this.weaponList_middle.Count;
    }

    public int bigWeaponListSize(){
        return this.weaponList_big.Count;
    }

    public int shieldListSize(){
        return this.shieldList.Count;
    }

    public List<Inventory> getSmallWeaponList(){
        return weaponList_small;
    }

    public List<Inventory> getMiddleWeaponList(){
        return weaponList_middle;
    }

    public List<Inventory> getBigWeaponList(){
        return weaponList_big;
    }
}


/**
    Sword_1_Small:      50      3           67,5            x
    Sword_1_Middle:     70      3           94,5            x
    Sword_1_Big:        100     3           135             x
    Axe:                85      1           93,5            x
    Axe_Big:            130     1           143,5           x
    Mace_1:             85      1           93,5            x
    Mace_2:             130     1           143,5           x
    Wood_Small:         10      4           15              x
    Wood_Big:           20      4           30              x
    Sword_2_Small:      75      2           90              x
    Sword_2_Big:        115     2           138             x
    Bow:                75      2           90              x + bow small
    Pfeile_1            10                  +100            x
    Pfeile_2            20                  +110            x
    Pfeile_3            30                  +120            x
    Wand_Small          30      1           33
    Wand_Middle         70      2           84
    Wand_Big            120     3           162
*/
