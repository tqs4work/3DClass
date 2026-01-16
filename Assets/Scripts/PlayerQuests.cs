using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;

public class PlayerQuests : MonoBehaviour
{
    public static PlayerQuests Instance;

    public List<QuestData> activeQuests = new List<QuestData>();

    public Dictionary<string, int> questStepIndex = new Dictionary<string, int>();
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AcceptQuest(QuestData quest)
    {
        if (IsQuestActive(quest))
            return;

        quest.ResetProgress();

        activeQuests.Add(quest);
        questStepIndex[quest.questId] = 0;
        Debug.Log($"Accepted quest: {quest.questName}");
    }

    public void NotifyProgress(ObjectType objectType, string targetId, int amount = 1)
    {
        for (int i = activeQuests.Count - 1; i >= 0; i--)
        {
            var quest = activeQuests[i];
            QuestStep currentStep = quest.questSteps[questStepIndex[quest.questId]];

            if (currentStep.objectType == objectType && currentStep.targetId.ToString() == targetId)
            {
                currentStep.currentAmount += amount;
                Debug.Log($"Progressed quest: {quest.questName}, Step: {currentStep.description}, Progress: {currentStep.currentAmount}/{currentStep.requiredAmount}");

                if (currentStep.IsCompleted())
                {
                    Debug.Log($"Completed step: {currentStep.description} of quest: {quest.questName}");
                    AdvanceQuest(quest);
                }
            }
        }

    }

    public void CompleteQuest(QuestData quest)
    {
        Debug.Log($"Completed quest: {quest.questName}. Reward: {quest.goldReward} gold.");
        activeQuests.Remove(quest);
        questStepIndex.Remove(quest.questId);
    }

    public void AdvanceQuest(QuestData quest)
    {
        questStepIndex[quest.questId]++;
        if (questStepIndex[quest.questId] >= quest.questSteps.Count)
        {
            CompleteQuest(quest);
        }
    }

    public bool IsQuestActive(QuestData quest) => activeQuests.Any(q => q.questId == quest.questId);
}
