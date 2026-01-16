using UnityEngine;

public class NpcQuestInteraction : MonoBehaviour
{
    public QuestData questData;

    public void OnInteract()
    {
        PlayerQuests.Instance.AcceptQuest(questData);
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            OnInteract();
            Debug.Log($"Started quest: ");
        }
    }
}
