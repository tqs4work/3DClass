using UnityEngine;

[CreateAssetMenu(fileName = "skill_", menuName = "Game/Skill")]
public class skillData : ScriptableObject
{
    public string skillName;
    public Sprite icon;
    public float cooldown;
    public float manaCost;
    public GameObject skillEffectPrefabs;
    public float damage;
    public float level;

    public float GetPower()
    {
        return damage * (1 + 0.2f *(level - 1));
    }
}
