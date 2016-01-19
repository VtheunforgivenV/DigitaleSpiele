using UnityEngine;
using System.Collections;
using System;

public abstract class Quest : IComparer{

    protected int questID;

    protected QuestType questType;

    protected string description;

    protected int goldReward = -1;
    protected int expReward = -1;
    protected Inventory itemReward = null;

    protected bool markAsSolved = false;

    public abstract void onQuestBegin();

    public abstract void onQuestRefresh();

    public void onQuestFinished() {
        if(goldReward != -1) {
            GameConstants.getPlayerStats().gainMoney(goldReward);
        }

        if(expReward != -1) {
            GameConstants.getPlayerStats().gainExp(expReward);
        }

        if(itemReward != null) {
            GameConstants.getPlayerInventory().AddWeaponWithID(itemReward.GetID());
        }
        markAsSolved = true;
    }

    public string getQuestDescription() {
        return description;
    }

    public bool isQuestSolved() {
        return markAsSolved;
    }

    public int Compare(object x, object y) {
        if(((Quest)x).questID == ((Quest)y).questID){
            return 1;
        } else {
            return -1;
        }
    }
}

public enum QuestType {
    KILL, COLLECT
}