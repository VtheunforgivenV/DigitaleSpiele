using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CollectCounter {

    protected Dictionary<CollectType, int> collectDictionary;

    private  void initCollectCounter() {
        collectDictionary = new Dictionary<CollectType, int>();

        foreach(CollectType type in CollectType.GetValues(typeof(CollectType))) {
            collectDictionary.Add(type, 0);
        }
    }

    public  void addNewCollect(CollectType type) {
        if(collectDictionary == null) {
            initCollectCounter();
        }
        collectDictionary.Add(type, (collectDictionary[type] + 1));
    }
  
    public  int getCollectsforType(CollectType type) {
        if (collectDictionary == null) {
            initCollectCounter();
        }
        return collectDictionary[type]; 
    }

}

public enum CollectType {
    FLOWER
}
