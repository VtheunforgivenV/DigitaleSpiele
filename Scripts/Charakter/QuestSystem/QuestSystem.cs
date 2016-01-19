using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class QuestSystem {

    public int questID = 0;

    KillCounter killCounter;
    CollectCounter collectCounter;

    List<Quest> questList;

    public QuestSystem() {
        killCounter = new KillCounter();
        collectCounter = new CollectCounter();
        questList = new List<Quest>();
    }

    public void refreshQuestSystem(KillType? killType, CollectType? collectType) {

        if(killType != null) {
            Debug.Log("Add Kill");
            killCounter.addNewKill((KillType)killType);
        }

        if(collectType != null) {
            Debug.Log("Add Collect");
            collectCounter.addNewCollect((CollectType)collectType);
        }

        foreach(Quest q in questList) {
            if (!q.isQuestSolved()) {
                q.onQuestRefresh();
            }
        }
    }

    public KillCounter getKillCounter() {
        return killCounter;
    }

    public CollectCounter getCollectCounter() {
        return collectCounter;
    }

    public List<Quest> getQuestList() {
        return questList;
    }

    public bool addNewQuest(Quest q) {
        if (questList.Contains(q)) {
            Debug.Log("Quest schon erhalten");
            return false;
        } else {
            Debug.Log("Füge Quest der Questliste hinzu");
            questList.Add(q);
            q.onQuestBegin();
            return true;
        }
    }
}