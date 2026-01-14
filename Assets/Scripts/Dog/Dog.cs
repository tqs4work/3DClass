
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public enum DogState
{
    Patrolling,
    FollowingPlayer,
    ReturningHome
}
public class Dog : MonoBehaviour
{
    public Transform playerTargetTransform;
    public NavMeshAgent dogNavMeshAgent;
    public DogState currentDogState = DogState.Patrolling;

    public Transform[] patrolPoints;
    public int currentPatrolIndex = 0;
    public Vector3 initiaPosition;
    public float chaseRange = 10f;
    public float returnRange = 20f;

    public Animator dogAnimator;
    public int dogSpeedHash ;

    public Health dogHealth;
    public int currentHealth;
    public int maxHealth ;
    public Slider dogHealthsilder;


    public AudioClip dogBeingHitClip;
    public AudioSource audioSource;

    public Camera mainCamera;
    void Start()
    {
       initiaPosition = transform.position;
        dogSpeedHash = Constantss.DogSpeedHash;
        currentHealth = maxHealth;
        dogHealth.SetUp(currentHealth, maxHealth);
        dogHealthsilder.maxValue = maxHealth;
        dogHealthsilder.value = currentHealth;
    }
    void Update()
    {
        currentHealth = dogHealth.GetCurrentHealth();
        if(currentHealth <= 0)
        {
            Debug.Log("Dog has died.");
            Destroy(gameObject);
            return;
        }



        var distanceToPlayer = Vector3.Distance(transform.position, playerTargetTransform.position);
        var distanceToHome = Vector3.Distance(transform.position, initiaPosition);

        switch (currentDogState)
        {
            case DogState.Patrolling:
                HandlePatrolling(distanceToPlayer);
                break;
            case DogState.FollowingPlayer:
                HandleChasing(distanceToPlayer, distanceToHome);
                break;
            case DogState.ReturningHome:
                HandleReturningHome();
                break;
            default:
                break;
        }
        dogAnimator.SetFloat(dogSpeedHash, dogNavMeshAgent.velocity.magnitude);
    }
    void HandleReturningHome()
    {
        dogNavMeshAgent.SetDestination(initiaPosition);
        if (!dogNavMeshAgent.pathPending && dogNavMeshAgent.remainingDistance < 0.5f)
        {
            currentDogState = DogState.Patrolling;
            return;
        }
    }
    void HandleChasing(float distanceToPlayer, float distanceToHome)
    {
        if (distanceToPlayer > returnRange)
        {
            currentDogState = DogState.ReturningHome;
            return;
        }
        dogNavMeshAgent.SetDestination(playerTargetTransform.position);
        if (distanceToPlayer > chaseRange + 2f)
        {
            currentDogState = DogState.Patrolling;
            return;
        }
    }
    void HandlePatrolling(float distanceToPlayer)
    {
        if (distanceToPlayer < chaseRange)
        {
            currentDogState = DogState.FollowingPlayer;
            return;
        }
        dogNavMeshAgent.SetDestination(patrolPoints[currentPatrolIndex].position);
        if ( dogNavMeshAgent.remainingDistance < 0.5f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(Constants.PlayerSwordTag))
        {
            Debug.Log("Dog has reached the player!");
            var damageAmount = 100;
            dogHealth.TakeDamage(damageAmount);
            Debug.Log($"Dog took {damageAmount} damage. Current health: {dogHealth.GetCurrentHealth()}");
            audioSource.PlayOneShot(dogBeingHitClip); 

            dogHealthsilder.value= dogHealth.GetCurrentHealth();
        }
    }
    private void LateUpdate()
    {
        dogHealthsilder.transform.LookAt(dogHealthsilder.transform.position + mainCamera.transform.rotation * Vector3.forward, mainCamera.transform.rotation * Vector3.up);
    }
}
