using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public PlayerInput playerInput;
    public Animator playerAnimator;
    public int attackhash;
    public int combohash;
    

    public AudioClip attackClip;
    public AudioSource audioSource;
    void Start()
    {
        attackhash = Constants.AttackHash;
        combohash = Constants.ComboHash;
    }
    void Update()
    {
        if (playerInput.attackInput)
        {
            playerAnimator.SetTrigger(attackhash);
            audioSource.PlayOneShot(attackClip);
        }
        if (playerInput.comboInput)
        {
            playerAnimator.SetTrigger(combohash);
        }
    }
}
