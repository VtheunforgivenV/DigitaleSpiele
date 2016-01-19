using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NPC_LootInfo : MonoBehaviour {

    private GameObject info;
    private GameObject itemName;
    private GameObject itemType;
    private GameObject itemImage;
    private GameObject itemDescription;
    private GameObject itemValue;

    void Start(){
        //Debug.Log("StartInfo");
        info = GameObject.Find("NPC_Info_Panel");
        info.SetActive(false);
        Transform t;
        for (int i = 0; i < info.transform.childCount; i++){
            t = info.transform.GetChild(i);
            if (t.tag.Equals("Inventory_Item_Name")) {
                itemName = t.gameObject;
            } else if (t.tag.Equals("Inventory_Item_Type")) {
                itemType = t.gameObject;
            } else if (t.tag.Equals("Inventory_Item_Image_Container")){
                itemImage = t.GetChild(0).gameObject;
            }else if (t.tag.Equals("Inventory_Item_Description")){
                itemDescription = t.gameObject;
            }else if (t.tag.Equals("Inventory_Item_Value")){
                itemValue = t.gameObject;
            }
        }
    }



    public void Activate(Inventory item) {
        info.SetActive(true);
        info.transform.position = new Vector2((Input.mousePosition.x + 200), (Input.mousePosition.y));
        //ItemName
        if (itemName != null) {
            itemName.SetActive(true);
            itemName.GetComponent<Text>().text = item.GetName();
        } else {
            itemName.SetActive(false);
        }

        //ItemType
        if (itemType != null) {
            itemType.SetActive(true);
            if (GameConstants.mapStringToClass(item.GetType())) {
                itemType.GetComponent<Text>().color = Color.white;
            } else {
                itemType.GetComponent<Text>().color = Color.red;
            }
            itemType.GetComponent<Text>().text = item.GetType();
        } else {
            itemType.SetActive(false);
        }

        //ItemImage
        if (itemImage != null) {
            itemImage.SetActive(true);
            itemImage.GetComponent<Image>().sprite = item.GetImage();
        } else {
            itemImage.SetActive(false);
        }

        //ItemDescription
        if (itemDescription != null) {
            itemDescription.SetActive(true);
            itemDescription.GetComponent<Text>().text = item.GetDescription();
        } else {
            itemDescription.SetActive(false);
        }

        //ItemValue
        if (itemValue != null) {
            itemValue.SetActive(true);
            itemValue.GetComponent<Text>().text = "Wert: " + (item.GetValue() * GameConstants.traderValueMultiplier);
        } else {
            itemValue.SetActive(false);
        }
    }

    public void Deactivate() {
        info.SetActive(false);
    }
}
