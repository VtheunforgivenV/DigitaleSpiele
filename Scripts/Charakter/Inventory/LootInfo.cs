using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LootInfo : MonoBehaviour {

    private GameObject info;
    private GameObject itemName;
    private GameObject itemImage;
    private GameObject itemDescription;
    private GameObject itemValue;

    private GameObject player;
    private ToonCharacter_Inventory inventory;
    
    private ILootableObject loot;

    void Start(){
        //Debug.Log("StartInfo");
        info = GameObject.Find("Loot_Info_Panel");
        info.SetActive(false);
        Transform t;
        for (int i = 0; i < info.transform.childCount; i++){
            t = info.transform.GetChild(i);
            if (t.tag.Equals("Inventory_Item_Name")){
                itemName = t.gameObject;
            }else if (t.tag.Equals("Inventory_Item_Image_Container")){
                itemImage = t.GetChild(0).gameObject;
            }else if (t.tag.Equals("Inventory_Item_Description")){
                itemDescription = t.gameObject;
            }else if (t.tag.Equals("Inventory_Item_Value")){
                itemValue = t.gameObject;
            }
        }
        //InitReference To PlayerCharakter and Inventory
        player = GameConstants.findPlayerInProject();
        inventory = player.GetComponent<ToonCharacter_Inventory>();
    }



    public void Activate(ILootableObject chest) {
        info.SetActive(true);
        loot = chest;
        //ItemName
        if (itemName != null) {
            itemName.SetActive(true);
            itemName.GetComponent<Text>().text = loot.getItem().GetName();
        } else {
            itemName.SetActive(false);
        }

        //ItemImage
        if (itemImage != null) {
            itemImage.SetActive(true);
            itemImage.GetComponent<Image>().sprite = loot.getItem().GetImage();
        } else {
            itemImage.SetActive(false);
        }

        //ItemDescription
        if (itemDescription != null) {
            itemDescription.SetActive(true);
            itemDescription.GetComponent<Text>().text = loot.getItem().GetDescription();
        } else {
            itemDescription.SetActive(false);
        }

        //ItemValue
        if (itemValue != null) {
            itemValue.SetActive(true);
            itemValue.GetComponent<Text>().text = "Wert: " + loot.getItem().GetValue();
        } else {
            itemValue.SetActive(false);
        }
    }

    public void lootObject() {
        if (loot.getItem() is IWeapon) {
            if (inventory.AddWeaponWithID(loot.getItem().GetID())) {
                loot.setLooted(true);
            }
        } else if (loot.getItem() is IShield) {
            /*
            if (inventory.AddShieldWithID(chest.item.GetID())){

            }
            */
        }

        if (loot.isLooted()) {
            Deactivate();
        }
    }

    public void Deactivate() {
        info.SetActive(false);
    }
}
