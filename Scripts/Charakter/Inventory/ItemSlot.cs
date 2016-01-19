using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {
    
    public int itemSlotID;
    public Inventory item;
    public ToonCharacter_Inventory playerInventory;

    private ToolTip info;
	private GameObject inventoryPanel;

    void Start(){
		info = GameObject.Find(GameConstants.inventoryUI).GetComponent<ToolTip>();
		inventoryPanel = GameObject.Find (GameConstants.inventoryUI).gameObject;
    }

    public void OnPointerEnter(PointerEventData eventData){
        info.Activate(item);
    }

    public void OnPointerExit(PointerEventData eventData){
        info.Deactivate();
    }

    public void OnPointerClick(PointerEventData eventData){
        if (eventData.button.Equals(PointerEventData.InputButton.Left)) {
            if (item is IWeapon) {
				if (GameConstants.mapStringToClass(item.GetType()) && checkPlayerWeaponLevel()) {
                    playerInventory.EquipWeaponWithID(item.GetID(), this);
                } else {
					Debug.Log ("Waffenlevel Player: " + GameConstants.getPlayerStats ().weaponLevel);
					playerInventory.showMessageBox("Waffe kann nicht angelegt werden", inventoryPanel);
                }

            }
        }else if (eventData.button.Equals(PointerEventData.InputButton.Right)) {
            if (GameConstants.findPlayerInProject().GetComponent<ToonCharakter_NPCRayCast>().isTalkingToNPC()) {
                if ((playerInventory.getWeaponSlot() == null)) {
                    sellItem();
                } else {
                    if (!(playerInventory.getWeaponSlot().getItemSlot().itemSlotID == itemSlotID)) {
                        sellItem();
                    } else {
					    playerInventory.showMessageBox("Kann nicht verkauft werden", inventoryPanel);
                    }
                }
            }
        }
    }

	public bool checkPlayerWeaponLevel() {
		PlayerStats stats = GameConstants.getPlayerStats ();

		ListWeapons weapons = GameConstants.getListWeapons ();

		switch (stats.weaponLevel) {
		case WeaponLevel.SMALL:

			if (weapons.getSmallWeaponList ().Contains (item)) {
				return true;
			}
			break;

		case WeaponLevel.MIDDLE:
			
			if (weapons.getMiddleWeaponList ().Contains (item) ||
				weapons.getSmallWeaponList ().Contains (item)) {
				return true;
			}
			break;
			
		case WeaponLevel.BIG:
			
			if (weapons.getBigWeaponList ().Contains (item) ||
				weapons.getMiddleWeaponList ().Contains (item) ||
				weapons.getSmallWeaponList ().Contains (item)) {
				return true;
			}
			break;

		}

		return false;
	}

    public void sellItem() {
        GameConstants.getPlayerStats().gainMoney(item.GetValue());
        playerInventory.removeItemFromInventory(itemSlotID);
    }

    public void EnableHighlightItemSlot(){
        this.GetComponent<Image>().color = new Color(0, 255, 255);
    }

    public void DisableHighlightItemSlot(){

        this.GetComponent<Image>().color = new Color(255, 255, 255);
    }

    public int getWeaponID() {
        return item.GetID();
    }
}
