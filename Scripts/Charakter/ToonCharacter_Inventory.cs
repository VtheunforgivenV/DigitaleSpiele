using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class ToonCharacter_Inventory : UIManager_BaseGame {

/** ----------------------------------------------------------------------------- Variables -----------------------------------------------------------------------------------------------------*/

/** -------------------------------- Variables for InventoryUI ------------------------------*/
    //ItemHolder for Classes
    Inventory_Weapon_Model weaponModel;
    private GameObject inventoryPanel;
    private GameObject slotPanel;
    public GameObject inventorySlot;
    public GameObject inventoryItem;
    List<GameObject> slots;

    public Text goldAmountText;


    Shield shield;
    public Weapon quiver;

/** -------------------------------- Variables for InventoryModel - for Saving Items ------------------------------*/
    int itemAmount = 20;
    public Dictionary<int, Inventory> inventory;

/** -------------------------------- Other Variables------------------------------*/
    ListWeapons listWeapons;

/** ----------------------------------------------------------------------------- Methods -----------------------------------------------------------------------------------------------------*/

    void Awake() {
        inventoryPanel = GameObject.Find(panel_name);
        slotPanel = GameObject.Find("Slot_Panel");
        goldAmountText = GameObject.Find("Gold").GetComponent<Text>();
    }

    new void Start(){
        base.Start();

        //GetList of Weapons
        listWeapons = GameObject.Find("RolePlayItems").GetComponent<ListWeapons>();
        //Set InventoryCanvas and setup Inventory
        if(slots == null){
            slots = new List<GameObject>();
        }
        for(int i = 0; i < itemAmount; i++){
            slots.Add(Instantiate(inventorySlot));
            slots[i].transform.SetParent(slotPanel.transform);
        }
        updateInventoryFromGameLoading();
        updateActualWeaponFromGameLoading();
    }

    void Update(){
        //Opens Inventory and freezes Game While Inventory is on
        if (Input.GetKeyDown(KeyCode.I)){
            if (!this.gameObject.GetComponent<ToonCharakter_NPCRayCast>().isTalkingToNPC()) {
                activateMenu(!inventoryPanel.GetComponentInParent<Canvas>().isActiveAndEnabled);
            }
        }
    }

    void FixedUpdate() {
        goldAmountText.text = "Goldmenge: " + GameConstants.getPlayerStats().goldAmount;
    }

/** -------------------------------- Ohter Methods ------------------------------*/

    private void updateInventoryFromGameLoading() {
        inventory = new Dictionary<int, Inventory>();
        List<int> inv = GameConstants.getPlayerStats().inventoryDictionary;
        for (int i = 0; i < inv.Count; i++) {
            AddWeaponWithID(inv[i]);
        }
    }

    private void updateActualWeaponFromGameLoading() {
        if(GameConstants.getPlayerStats() != null) {
            if (GameConstants.getPlayerStats().actualWeapon != -1) {
                ItemSlot itemSlot = slots[GameConstants.getPlayerStats().actualWeapon].GetComponentInChildren<ItemSlot>();
                EquipWeaponWithID(itemSlot.getWeaponID(), itemSlot);
            } else {
                int weaponAddId = -1;
                switch (GameConstants.findPlayerInProject().GetComponent<ToonCharacter_PlayerScript>().getBasePlayer().playerType) {
                    case PlayerType.MELEE:
                        weaponAddId = 0;
                        break;
                    case PlayerType.DISTANCE:
                        weaponAddId = 3;
                        break;
                    case PlayerType.MAGIC:
                        weaponAddId = 4;
                        break;
                }
                AddWeaponWithID(weaponAddId);
                AddWeaponWithID(9);
                AddWeaponWithID(15);
            }
        }
    }

    /**
    *   Method for Equiping a Weapon
    *   also
    *   Find Player and Set Attributes to Animator
    *
    *   params:
    *       id - id of Weapon in WeaponDictionary
    *       itemSlot - Slot which holds the Weapon in the InventoryUI
    */
    public void EquipWeaponWithID(int id, ItemSlot itemSlot){
        IWeapon newItemToEquip = listWeapons.getWeapons()[id];

        GameConstants.getPlayerStats().actualWeapon = itemSlot.itemSlotID;

        if (weaponModel == null) {
            weaponModel = new Inventory_Weapon_Model((IWeapon)newItemToEquip, itemSlot);
        }else{
            weaponModel.disableOldItem();
            weaponModel.setWeapon((IWeapon)newItemToEquip);
            weaponModel.setItemSlot(itemSlot);
        }
        weaponModel.enableNewItem();

        float speed = 1.0f;       
        listWeapons.getWeaponSpeed().TryGetValue(((IWeapon)newItemToEquip).GetSpeed(), out speed);

        //Find Player
        if(GameConstants.findPlayerInProject() != null) {
            GameConstants.findPlayerInProject().GetComponent<Animator>().SetFloat("EquipedWeapon", 1.0f);
            GameConstants.findPlayerInProject().GetComponent<Animator>().SetFloat("AttackSpeed", speed);
        }
    }

    /**
    *   Method for Adding an Item to the InventoryUI
    *   Adds Item to the next available Slot in the Inventory
    *   
    */
    public bool AddWeaponWithID(int id){
        if(inventory == null){
            inventory = new Dictionary<int, Inventory>();
        }
        Inventory item = (Inventory) listWeapons.getWeapons()[id];
        if(inventory.Count >= 20){
            return false;
        }else{
            for(int i = 0; i < itemAmount; i++){
                if (slots[i].transform.childCount <= 0){
                    inventory.Add(i, item);
                    GameObject itemObject = Instantiate(inventoryItem);
                    itemObject.GetComponent<ItemSlot>().itemSlotID = i;
                    itemObject.transform.SetParent(slots[i].transform);
                    itemObject.GetComponent<Image>().sprite = item.GetImage();
                    itemObject.transform.position = slots[i].transform.position;
                    itemObject.name = item.GetImage().name;
                    itemObject.GetComponent<ItemSlot>().item = item;
                    itemObject.GetComponent<ItemSlot>().playerInventory = this;
                    break;
                }
            }
            return true;
        }
    }

    public void removeItemFromInventory(int itemSlotID) {
        //Remove Item from inventory
        inventory.Remove(itemSlotID);
        //Delete the old Slot
        GameObject.Destroy(slots[itemSlotID]);
        slots.Remove(slots[itemSlotID]);
        //Instantiate the new Slot
        slots.Add(Instantiate(inventorySlot));
        slots[itemAmount - 1].transform.SetParent(slotPanel.transform);

        //Decrement itemSlotID for available Items in Inventory
        for (int i = itemSlotID; i < inventory.Count; i++) {
            ItemSlot childItemSlot = slots[i].transform.GetComponentInChildren<ItemSlot>();
            if (childItemSlot != null) {
                childItemSlot.itemSlotID = i;
            }
        }
        //Decrement id for Items in inventoryDictionary
        for (int i = (itemSlotID + 1); i < itemAmount; i++) {
            Inventory item;
            inventory.TryGetValue(i, out item);
            if (item != null) {
                inventory.Add((i - 1), item);
            }
            inventory.Remove(i);
        }

    }


    public Inventory_Weapon_Model getWeaponSlot() {
        return weaponModel;
    }

    public IWeapon getPlayerWeapon() {
        if(weaponModel == null) {
            return null;
        } else {
            return weaponModel.getWeapon();
        }
    } 

}