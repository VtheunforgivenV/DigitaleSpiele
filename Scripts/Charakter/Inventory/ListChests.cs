using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ListChests : MonoBehaviour {

    private List<Chest> chestList;
    private List<Inventory> itemList;
    private List<List<Inventory>> itemLists;

    private ListWeapons listWeapons;
    static System.Random randomizer = new System.Random();
    
    void Start(){
        listWeapons = GetComponent<ListWeapons>();
        initChestList();
        initItemLists();
        generateItemList();
        initRandomLoot();
    }
    
    private void initChestList(){
        chestList = new List<Chest>();
        GameObject[] chests = GameObject.FindGameObjectsWithTag("Chest");
        for (int i = 0; i < chests.Length; i++){
            chestList.Add((Chest)chests[i].GetComponent<Chest>());
        }
    }

    private void initItemLists(){
        itemLists = new List<List<Inventory>>();
        itemLists.Add(listWeapons.getSmallWeaponList());
        itemLists.Add(listWeapons.getMiddleWeaponList());
        itemLists.Add(listWeapons.getBigWeaponList());
    }

    private void generateItemList(){
        itemList = new List<Inventory>();
        foreach(List<Inventory> lists in itemLists){
            foreach(Inventory item in lists){
                itemList.Add(item);
            }
        }
    }

    private void initRandomLoot(){
        foreach(Chest chest in chestList){
            chest.item = itemList[randomizer.Next(itemList.Count)];
        }
    }
}
