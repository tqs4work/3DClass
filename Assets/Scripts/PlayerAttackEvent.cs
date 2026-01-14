using UnityEngine;

public class PlayerAttackEvent : MonoBehaviour
{
    public BoxCollider swordCollider;

    public void BeginAttack()
    {
        Debug.Log("Attack started.");
        swordCollider.enabled = true;
    }
    public void EndAttack() 
    {
        Debug.Log("Attack ended.");
        swordCollider.enabled = false;
    }
}
