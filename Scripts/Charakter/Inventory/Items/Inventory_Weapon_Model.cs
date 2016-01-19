using UnityEngine;
using System;

public class Inventory_Weapon_Model{

    IWeapon weapon;
    ItemSlot itemSlot;

    public Inventory_Weapon_Model(IWeapon weapon, ItemSlot itemSlot){
        this.weapon = weapon;
        this.itemSlot = itemSlot;
    }

    public void disableOldItem(){
        if (weapon != null){
            weapon.SetVisibility(false);
        }

        if (itemSlot != null){
            itemSlot.DisableHighlightItemSlot();
        }
    }

    public void enableNewItem(){
        itemSlot.EnableHighlightItemSlot();
        weapon.SetVisibility(true);
    }

    internal IWeapon getWeapon() {
        return weapon;
    }

    internal void setWeapon(IWeapon weapon){
        this.weapon = weapon;
    }

    internal ItemSlot getItemSlot() {
        return itemSlot;
    }

    internal void setItemSlot(ItemSlot itemSlot){
        this.itemSlot = itemSlot;
    }


}
