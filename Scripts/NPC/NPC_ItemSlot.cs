using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class NPC_ItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {
    
    public int itemSlotID;
    public Inventory item;
    public ToonCharacter_Inventory inventory;
    public ToonCharakter_NPCRayCast npcInventory;

	public GameObject iventoryPanel;

    private NPC_LootInfo info;

    void Start(){
		info = GameObject.Find("NPCUI").GetComponent<NPC_LootInfo>();

        inventory = GameConstants.getPlayerInventory();
        npcInventory = GameConstants.findPlayerInProject().GetComponent<ToonCharakter_NPCRayCast>();
		iventoryPanel = GameObject.Find (GameConstants.inventoryUI).transform.FindChild ("Inventory_Panel").gameObject;
    }

    public void OnPointerEnter(PointerEventData eventData){
        info.Activate(item);
    }

    public void OnPointerExit(PointerEventData eventData){
        info.Deactivate();
    }

    public void OnPointerClick(PointerEventData eventData){
        if (eventData.button.Equals(PointerEventData.InputButton.Left)) {
            if(GameConstants.getPlayerStats().goldAmount >= (GameConstants.traderValueMultiplier * item.GetValue())) {
                if (item is IWeapon) {
                    purchaseItem();
                }
            } else {
				inventory.showMessageBox ("Nicht genügend Gold vorrätig.", iventoryPanel);
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

    public void purchaseItem() {
        if (GameConstants.getPlayerInventory().AddWeaponWithID(item.GetID())) {
            GameConstants.getPlayerStats().looseMoney((GameConstants.traderValueMultiplier * item.GetValue()));
            npcInventory.removeItemFromNPCInventory(itemSlotID);
        }else {
            //TODO MessageBox
			inventory.showMessageBox ("Inventar ist voll.", iventoryPanel);
        }

        
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
