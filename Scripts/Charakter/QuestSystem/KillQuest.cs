using UnityEngine;
using System.Collections;

public class KillQuest : Quest {

    KillType type;
    int killAmountQuestbegin = -1;
    int killCount;

    public KillQuest(KillType type, int killAmount, string description, int goldReward, int expReward, Inventory itemReward) {
        this.type = type;
        this.killCount = killAmount;

        base.questID = GameConstants.getPlayerStats().questSystem.questID++;
        base.description = description;
        base.goldReward = goldReward;
        base.expReward = expReward;
        base.itemReward = itemReward;
    }

    override public void onQuestBegin() {
        this.killAmountQuestbegin = GameConstants.getPlayerStats().questSystem.getKillCounter().getKillsforType(type);
        Debug.Log("Quest started: " + killAmountQuestbegin);
    }

    
    override public void onQuestRefresh() {
        Debug.Log("Quest refreshed");
        int actualkillsForType = GameConstants.getPlayerStats().questSystem.getKillCounter().getKillsforType(type);
        Debug.Log("Enemy killed " + (actualkillsForType - killAmountQuestbegin) + " of " + (killAmountQuestbegin + killCount));
        if (actualkillsForType >= (killAmountQuestbegin + killCount)) {
            onQuestFinished();
        }
    }

    new private void onQuestFinished() {
        Debug.Log("Quest finished");
        base.onQuestFinished();
    }

}
