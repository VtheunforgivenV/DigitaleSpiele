using UnityEngine;
using System.Collections;

public class CollectQuest : Quest {

    CollectType type;
    int collectAmountQuestbegin = -1;
    int collectCount;

    public CollectQuest(CollectType type, int collectAmount, string description, int goldReward, int expReward, Inventory itemReward) {
        this.type = type;
        this.collectCount = collectAmount;

        base.questID = GameConstants.getPlayerStats().questSystem.questID++;
        base.description = description;
        base.goldReward = goldReward;
        base.expReward = expReward;
        base.itemReward = itemReward;
    }

    override public void onQuestBegin() {
        Debug.Log("Quest started");
        this.collectAmountQuestbegin = GameConstants.getPlayerStats().questSystem.getCollectCounter().getCollectsforType(type);
    }

    override public void onQuestRefresh() {
        Debug.Log("Quest refreshed");
        int actualCollectsForType = GameConstants.getPlayerStats().questSystem.getCollectCounter().getCollectsforType(type);
        if ((collectAmountQuestbegin + collectCount) >= actualCollectsForType) {
            onQuestFinished();
        }
    }

    new private void onQuestFinished() {
        Debug.Log("Quest finished");
        base.onQuestFinished();
    }

}
