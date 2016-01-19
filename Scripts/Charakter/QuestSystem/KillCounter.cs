using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KillCounter {

    public Dictionary<KillType, int> killDictionary;

    private  void initKillCounter() {
        killDictionary = new Dictionary<KillType, int>();

        foreach(KillType type in KillType.GetValues(typeof(KillType))) {
            killDictionary.Add(type, 0);
        }
    }

    public  void addNewKill(KillType type) {
        if(killDictionary == null) {
            initKillCounter();
        }
        killDictionary[type] += 1;

        foreach (KillType types in KillType.GetValues(typeof(KillType))) {
            Debug.Log("Killtype: " + types.ToString() + " - Kills: " + killDictionary[types]);
        }
    }
  
    public  int getKillsforType(KillType type) {
        if (killDictionary == null) {
            initKillCounter();
        }
        return killDictionary[type]; 
    }

}

public enum KillType {
    Skeleton
}
