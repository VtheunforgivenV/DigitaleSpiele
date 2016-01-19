using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToonCharakter_NPCRayCast : UIManager_BaseGame {

    private NPC loot;

    private GameObject inventoryPanel;
    private GameObject slotPanel;
    public GameObject inventorySlot;
    public GameObject inventoryItem;
    List<GameObject> slots;
    private ToonCharacter_Inventory playerInventory;

    private Text openText;

    private bool talkingToNPC = false;
    private int npcInventoryAmount = 12;
    private int lootIDs = -1;
    static System.Random randomizer = new System.Random();

    Quest NPC_Quest;

    new void Start(){
		inventoryPanel = GameObject.Find(panel_name);
        slotPanel = GameObject.Find("NPC_Slot_Panel");
        openText = GameObject.Find("NPC_Text").GetComponent<Text>();
        openText.transform.gameObject.SetActive(false);

        playerInventory = GameConstants.findPlayerInProject().GetComponent<ToonCharacter_Inventory>();

        inventoryPanel.SetActive(false);

        if (slots == null) {
            slots = new List<GameObject>();
        }

        for (int i = 0; i < npcInventoryAmount; i++) {
            slots.Add(Instantiate(inventorySlot));
            slots[i].transform.SetParent(slotPanel.transform);
        }

        lootIDs = GameObject.Find("RolePlayItems").GetComponent<ListWeapons>().getWeapons().Count;
        for (int i = 0; i < npcInventoryAmount; i++) {
            AddWeaponWithID(randomizer.Next(lootIDs), i);
        }

        NPC_Quest = new KillQuest(KillType.Skeleton,
                                          3,
                                          "Hallo, ein paar Skelette haben mich vermöbelt und ich brauche dringend eure Hilfe um mich an Ihnen zu rächen. Bitte Töte ein paar Skelette für mich \n\n Töte " + 3 + " Skelette",
                                          1000,
                                          1000,
                                          null);
    }

    void Update(){
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 3, (1<< LayerMask.NameToLayer("NPC")))){
			if (hit.transform.tag.Equals("NPC")) {
                loot = hit.transform.gameObject.GetComponent<NPC>();
			} else {
                loot = null;
            }

            if (loot != null) {
                if (talkingToNPC) {
                    openText.transform.gameObject.SetActive(false);
                } else {
                    openText.transform.gameObject.SetActive(true);
                }


                if (!talkingToNPC){
                    if (Input.GetKeyDown(KeyCode.E)) {
                        talkingToNPC = true;
                        if (!inventoryPanel.activeSelf) {
                            inventoryPanel.SetActive(true);
                            Time.timeScale = 0.0f;
                            //Lock Cursor to Middle
                            Cursor.visible = true;
                            Cursor.lockState = CursorLockMode.None;
                            openText.transform.gameObject.SetActive(false);
                            playerInventory.activateMenu(true);
                        }
                    } else if (Input.GetKeyDown(KeyCode.F)) {
                        Debug.Log("Quest annehmen");
                        int killCount = 1;
                        GameConstants.getPlayerStats().questSystem.addNewQuest(NPC_Quest);
                    }
                } else {
                    if (Input.GetKeyDown(KeyCode.Q)) {

                        inventoryPanel.SetActive(false);
                        playerInventory.activateMenu(false);
                        Time.timeScale = 1.0f;
                        //Lock Cursor to Middle
                        Cursor.visible = false;
                        Cursor.lockState = CursorLockMode.Locked;
                        openText.transform.gameObject.SetActive(true);
                    }
                }
            }
            else{
                DisableUIElements(false);
            }
        }
        else{
            DisableUIElements(false);
        }
    }

    void DisableUIElements(bool setActive){
        if(loot != null) {
            talkingToNPC = false;
        }
        openText.text = "Reden (E)\nQuest annehmen (F)";
        openText.transform.gameObject.SetActive(setActive);
    }

    void setLootText(string text){
        openText.text = text;
    }

    public void AddWeaponWithID(int id, int index) {
        //getItem
        Inventory item = (Inventory)GameObject.Find("RolePlayItems").GetComponent<ListWeapons>().getWeapons()[id];

        //Instantiate Prefab
        GameObject itemObject = Instantiate(inventoryItem);
        itemObject.GetComponent<NPC_ItemSlot>().itemSlotID = index;
        itemObject.transform.SetParent(slots[index].transform);
        itemObject.GetComponent<Image>().sprite = item.GetImage();
        itemObject.transform.position = slots[index].transform.position;
        itemObject.name = item.GetImage().name;
        itemObject.GetComponent<NPC_ItemSlot>().item = item;
    }

    public void removeItemFromNPCInventory(int itemSlotID) {
        //Delete the old Slot
        GameObject.Destroy(slots[itemSlotID]);
        slots.Remove(slots[itemSlotID]);
        //Instantiate the new Slot
        slots.Add(Instantiate(inventorySlot));
        slots[npcInventoryAmount - 1].transform.SetParent(slotPanel.transform);
        //Add new Item to Inventory
        AddWeaponWithID(randomizer.Next(lootIDs), npcInventoryAmount - 1);
        //Decrement itemSlotID for available Items in Inventory
        for (int i = 0; i < npcInventoryAmount; i++) {
            NPC_ItemSlot childItemSlot = slots[i].transform.GetComponentInChildren<NPC_ItemSlot>();
            if (childItemSlot != null) {
                childItemSlot.itemSlotID = i;
            }
        }

    }

    public bool isTalkingToNPC() {
        return talkingToNPC;
    }
}
