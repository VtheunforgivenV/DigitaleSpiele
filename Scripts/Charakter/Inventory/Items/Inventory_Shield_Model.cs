using UnityEngine;
using System;

public class Inventory_Shield_Model{

    IShield shield;
    ItemSlot itemSlot;

    public Inventory_Shield_Model(IShield shield, ItemSlot itemSlot){
        this.shield = shield;
        this.itemSlot = itemSlot;
    }

    public void disableOldItem(){
        if (shield != null){
            shield.SetVisibility(false);
        }

        if (itemSlot != null){
            itemSlot.DisableHighlightItemSlot();
        }
    }

    public void enableNewItem(){
        itemSlot.EnableHighlightItemSlot();
        shield.SetVisibility(true);
    }

    internal void setShield(IShield shield){
        this.shield = shield;
    }

    internal void setItemSlot(ItemSlot itemSlot)
    {
        this.itemSlot = itemSlot;
    }
}
