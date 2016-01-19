using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ListEnemyLoot : MonoBehaviour {

    private List<Enemy_Loot> enemyList;
    private List<Inventory> itemList;
    private List<List<Inventory>> itemLists;

    private ListWeapons listWeapons;
    static System.Random randomizer = new System.Random();
    
    void Start(){
        listWeapons = GetComponent<ListWeapons>();
        initEnemyList();
        initItemLists();
        generateItemList();
        initRandomLoot();
    }
    
    private void initEnemyList(){
        enemyList = new List<Enemy_Loot>();
		GameObject[] enemies = GameObject.FindGameObjectsWithTag(GameConstants.enemyTag);
        for (int i = 0; i < enemies.Length; i++){
            if (!enemies[i].name.EndsWith("(Clone)")) {
                enemyList.Add((Enemy_Loot)enemies[i].GetComponent<Enemy_Loot>());

            }
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
        foreach(Enemy_Loot enemyLoot in enemyList) {
            enemyLoot.item = itemList[randomizer.Next(itemList.Count)];
        }
    }
}
