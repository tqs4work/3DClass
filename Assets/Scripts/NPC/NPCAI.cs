using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public enum NpcState
{
    Patrolling,
    Talking
}

public class NPCAI : MonoBehaviour
{
    public NpcState currentNpcState = NpcState.Patrolling;
    public NavMeshAgent npcNavMeshAgent;
    public Transform playerTargetTransform;

    public float detectRange = 3f;
    public float leaveRange = 4f;

    public AudioSource audioSource;
    public AudioClip talkAudioClip;
    public TextMeshProUGUI npcNameText;
    public string[] dialoguelines;
    public Coroutine talkCoroutine;
    public bool isTalking = false;

    public Transform initialPosition;
    public Vector3 currentdestination;
    void Start()
    {

        initialPosition = transform;
        currentdestination = transform.position;
        npcNameText.text = "";
    }
    private bool isVisible = false;
    private void OnBecameVisible()
    {
        isVisible = true;
    }
    private void OnBecameInvisible()
    {
        isVisible = false;
    }
    void Update()
    {
        if (!isVisible) return;
        var distanceToPlayer = Vector3.Distance(transform.position, playerTargetTransform.position);
        switch (currentNpcState)
        {
            case NpcState.Patrolling:
                HandlePatrolling(distanceToPlayer);
                break;
            case NpcState.Talking:
                HandleTalking(distanceToPlayer);
                break;
            default:
                break;
        }
    }

    void HandlePatrolling(float distanceToPlayer)
    {
        if (distanceToPlayer <= detectRange)
        {
            currentNpcState = NpcState.Talking;
            return;
        }
        if (talkCoroutine != null)
        {
                       StopCoroutine(talkCoroutine);
            talkCoroutine = null;
            npcNameText.text = "";
            isTalking = false;
        }

        npcNavMeshAgent.SetDestination(currentdestination);

        if (npcNavMeshAgent.remainingDistance <= npcNavMeshAgent.stoppingDistance)
        {
            var randomDirection = Random.insideUnitSphere *10f;
            randomDirection += initialPosition.position;
            NavMeshHit navHit;
            NavMesh.SamplePosition(randomDirection, out navHit, 10f, NavMesh.AllAreas);
            currentdestination = navHit.position;
            npcNavMeshAgent.SetDestination(navHit.position);
        }
    }
    void HandleTalking(float distanceToPlayer)
    {
        if (distanceToPlayer >= leaveRange)
        {
            currentNpcState = NpcState.Patrolling;
            return;
        }
        npcNavMeshAgent.SetDestination(transform.position);

        var directionToPlayer = playerTargetTransform.position - transform.position;
        directionToPlayer.y = 0;
        if (directionToPlayer != Vector3.zero)
        {
            var targetRotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }
        if (!isTalking)
        {
            isTalking = true;
            audioSource.clip = talkAudioClip;
            audioSource.Play();

            //npcNameText.text = "";
           talkCoroutine =  StartCoroutine(TalkDialogue());
        }
        IEnumerator TalkDialogue()
        {
            foreach (var line in dialoguelines)
            {
                npcNameText.text = "";
                foreach (var character in line.ToCharArray())
                {
                    npcNameText.text += character;
                    yield return new WaitForSeconds(0.05f);
                }
                yield return new WaitForSeconds(2f);
            }
            npcNameText.text = "";
        }
    }

}