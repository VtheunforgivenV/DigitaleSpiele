using UnityEngine;
using System;

public class Weapon : MonoBehaviour, IWeapon, Inventory {

/** ----------------------------------------------------------------------------- Variables -----------------------------------------------------------------------------------------------------*/
    int id;
    public String itemName;
    public String type;

    public int value;
    public int weapondamage;
    public int weaponSpeed;
    public Sprite icon;

/** ----------------------------------------------------------------------------- Methods -----------------------------------------------------------------------------------------------------*/
    void Awake(){
        SetVisibility(false);
        setCollider(false);
    }

    public int GetDamage(){
        return weapondamage;
    }

    public int GetSpeed(){
        return weaponSpeed;
    }

    public void SetVisibility(bool visible){
        GetComponent<Renderer>().enabled = visible;
        setCollider(visible);
    }

    private void setCollider(bool active) {
        if (GetComponent<BoxCollider>() != null) {
            GetComponent<BoxCollider>().enabled = active;
        }
    }

    public Sprite GetImage(){
        return icon;
    }
    public String GetName(){
        return itemName;
    }

    String Inventory.GetType(){
        return type;
    }

    public String GetDescription(){
        return "Waffenschaden: " + weapondamage + "\n" + "Waffengeschwindigkeit: " + weaponSpeed;
    }

    public int GetValue(){
        return value;
    }

    public int GetID(){
        return id;
    }

    public void SetID(int id){
        this.id = id;
    }
}
