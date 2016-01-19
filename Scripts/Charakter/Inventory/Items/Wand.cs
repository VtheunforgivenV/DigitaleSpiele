using System;
using UnityEngine;

public class Wand : MonoBehaviour, IWeapon, Inventory{

    /** ----------------------------------------------------------------------------- Variables -----------------------------------------------------------------------------------------------------*/
    int id;
    public String itemName;
    private String type = "Zauberer";

    public int value;
    public int weapondamage;
    public int weaponSpeed;

    public GameObject orb;
    public GameObject wand;

    public Sprite icon;

    /** ----------------------------------------------------------------------------- Methods -----------------------------------------------------------------------------------------------------*/
    void Start(){
        SetVisibility(false);
    }

    public int GetDamage(){
        return weapondamage;
    }

    public int GetSpeed(){
        return weaponSpeed;
    }

    public void SetVisibility(bool visible){
        foreach(Renderer renderer in this.GetComponentsInChildren<Renderer>()) {
            renderer.enabled = visible;
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
        return "Zauberschaden: " + weapondamage + "\n" + "Zaubergeschwindigkeit: " + weaponSpeed;
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
