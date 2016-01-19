using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BaseTooltip : MonoBehaviour {

    private GameObject info;
    private GameObject itemName;
    private GameObject itemType;
    private GameObject itemImage;
    private GameObject itemDescription;
    private GameObject itemValue;
    private GameObject itemWeaponClass;

    void Start(){
        //Debug.Log("StartInfo");
        info = GameObject.Find("Tooltip_Panel");
        Transform t;
        for (int i = 0; i < info.transform.childCount; i++){
            t = info.transform.GetChild(i);
            if (t.tag.Equals("Inventory_Item_Name")) {
                itemName = t.gameObject;
            } else if (t.tag.Equals("Inventory_Item_Type")) {
                itemType = t.gameObject;
            }else if (t.tag.Equals("Inventory_Item_Image_Container")){
                itemImage = t.GetChild(0).gameObject;
            }else if (t.tag.Equals("Inventory_Item_Description")){
                itemDescription = t.gameObject;
            }else if (t.tag.Equals("Inventory_Item_Value")){
                itemValue = t.gameObject;
            } else if (t.tag.Equals("Inventory_Item_WeaponClass")) {
                itemWeaponClass = t.gameObject;
            }
        }
        info.SetActive(false);
    }

    public void Activate(Inventory item){
        info.SetActive(true);
        info.transform.position = new Vector2((Input.mousePosition.x + 200), (Input.mousePosition.y));

        //ItemName
        if(itemName != null){
            //Debug.Log("Text not null");
            itemName.SetActive(true);
            itemName.GetComponent<Text>().text = item.GetName();
        }else {
            //Debug.Log("Text null");
            itemName.SetActive(false);
        }

        //ItemType
        if(itemType != null) {
            itemType.SetActive(true);
            if (GameConstants.mapStringToClass(item.GetType())) {
                itemType.GetComponent<Text>().color = Color.white;
            }else {
                itemType.GetComponent<Text>().color = Color.red;
            }
            itemType.GetComponent<Text>().text = item.GetType();
        } else {
            itemType.SetActive(false);
        }

        //ItemImage
        if (itemImage != null){
            //Debug.Log("itemImage not null");
            itemImage.SetActive(true);
            itemImage.GetComponent<Image>().sprite = item.GetImage();
        }else{
            //Debug.Log("itemImage null");
            itemImage.SetActive(false);
        }

        //ItemDescription
        if (itemDescription != null){
            //Debug.Log("itemDescription not null");
            itemDescription.SetActive(true);
            itemDescription.GetComponent<Text>().text = item.GetDescription();
        }else{
            //Debug.Log("itemDescription null");
            itemDescription.SetActive(false);
        }

        //ItemValue
        if (itemValue != null){
            //Debug.Log("itemValue not null");
            itemValue.SetActive(true);
            itemValue.GetComponent<Text>().text = "Wert: " + item.GetValue();
        }else{
            //Debug.Log("itemValue null");
            itemValue.SetActive(false);
        }

        //itemWeaponClass
        if (itemWeaponClass != null) {
            //Debug.Log("itemValue not null");
            itemWeaponClass.SetActive(true);
            itemWeaponClass.GetComponent<Text>().text = "Klasse: " + getWeaponClassforItem(item);
        } else {
            //Debug.Log("itemValue null");
            itemWeaponClass.SetActive(false);
        }

    }

    public void Deactivate(){
        info.SetActive(false);
    }

    private int getWeaponClassforItem(Inventory item) {
        ListWeapons listWeapons = GameObject.Find("RolePlayItems").GetComponent<ListWeapons>();
        if (listWeapons.getSmallWeaponList().Contains(item)) {
            return 1;
        } else if (listWeapons.getMiddleWeaponList().Contains(item)) {
            return 2;
        } else if (listWeapons.getBigWeaponList().Contains(item)) {
            return 3;
        } else return -1;
    }
}
