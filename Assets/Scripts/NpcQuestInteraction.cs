using UnityEngine;

public class NpcQuestInteraction : MonoBehaviour
{
    public QuestData questData;

    public void OnInteract()
    {
        //PlayerQuests.Instance.StartQuest(questData);
        Debug.Log($"Started quest: ");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OnInteract();
        }
    }
}
