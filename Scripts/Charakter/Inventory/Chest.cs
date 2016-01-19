using UnityEngine;
using System.Collections;
using System;

public class Chest : MonoBehaviour, ILootableObject {

    public Inventory item = null;
    public bool isLooted = false;
    public bool isOpened = false;

    public Inventory getItem() {
        return item;
    }

    bool ILootableObject.isLooted() {
        return isLooted;
    }

    public void setLooted(bool looted) {
        isLooted = looted;
    }

    bool ILootableObject.isOpened() {
        return isOpened;
    }

    public void setOpened(bool opened) {
        isOpened = opened;
    }
}
