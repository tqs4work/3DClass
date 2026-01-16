using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public enum ObjectType
{
    Kill,
    Collect,
    Talk
}

[System.Serializable]
public class QuestStep
{
    public string description; // Description of the quest step
    public ObjectType objectType; // Type of objective
    public string targetId; // id of the target (enemy, item, NPC)
    public int requiredAmount; // Quantity required for the objective
    public int currentAmount; // Quantity currently achieved

    public bool IsCompleted()
    {
        return currentAmount >= requiredAmount;
    }
}

[CreateAssetMenu(fileName = "QuestData", menuName = "SO/QuestData")]
public class QuestData : ScriptableObject
{
    public string questId; // id nhi?m v?
    public string questName; // tên nhi?m v?

    [TextArea(3, 10)]
    public string description; // mô t? nhi?m v?

    public int goldReward; // ph?n th??ng vàng

    public List<QuestStep> questSteps; // các b??c nhi?m v?


    public void ResetProgress()
    {
        foreach (var step in questSteps)
        {
            step.currentAmount = 0;
        }
    }
}
