using UnityEngine;

public enum ItemType
{
    HpPotion, MpPotion, Weapon, Armor, Accessory
}

[CreateAssetMenu(fileName = "itemdata", menuName = "Scriptable Objects/itemdata")]
public class itemdata : ScriptableObject
{
    public string itemName;
    public ItemType itemType;
    public Sprite itemIcon;
    public GameObject prefab;

    public bool isStackable;
    public int maxStack;

    [Header ("Consumable Properties")]
    public int hpRestoreAmount;
    public int mpRestoreAmount;

    [Header("Equipment Properties")]
    public int attackBonus;
    public int defenseBonus;
    internal Sprite icon;
}
