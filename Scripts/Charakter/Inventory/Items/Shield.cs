using UnityEngine;
using System;

public class Shield : MonoBehaviour, IShield, Inventory {

    /** ----------------------------------------------------------------------------- Variables -----------------------------------------------------------------------------------------------------*/
    int id;
    public String itemName;
    private String type = "Nahkämpfer";

    public int value;
    public int damageReduction;
    public Sprite icon;


/** ----------------------------------------------------------------------------- Methods -----------------------------------------------------------------------------------------------------*/
    void Start(){
        SetVisibility(false);
        if (GetComponent<BoxCollider>() != null) {
            GetComponent<BoxCollider>().enabled = false;
        }
    }

    public int GetDamageReduction(){
        return damageReduction;
    }

    public void SetVisibility(bool visible){
        GetComponent<Renderer>().enabled = visible;
        if(GetComponent<BoxCollider>() != null) {
            GetComponent<BoxCollider>().enabled = visible;
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
        return "Schadensreduktion: " + damageReduction;
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
