using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

[System.Serializable]
public class PickupItem
{
    public itemdata itemdata;
    public int amount;
}

public class PlayerStats : MonoBehaviour
{
    public Button slotPrefab;
    public int maxSlots;
    public List<PickupItem> items = new List<PickupItem>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RenderItems();

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            var item = items.Count > 0 ? items[Random.Range(0, items.Count)].itemdata : null;
            PickItem(item, 20);
        }
        if(Input.GetKeyDown(KeyCode.O))
        {
            var item = items.Count > 0 ? items[Random.Range(0, items.Count)].itemdata : null;
            DropItem(item);
        }
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    var itemPickup = other.GetComponent<PickupItem>();
    //    if(itemPickup != null)
    //    {
    //        PickItem(itemPickup.itemdata, itemPickup.amount);
    //        Destroy(other.gameObject);
    //    }
    //}

    void UseItemAt(int index)
    {
        if(index < 0 || index >= items.Count) return;
        var slotitem = items[index];
       
       var item = slotitem.itemdata;
        if(item == null) return;
        switch (item.itemType)
        {
            case ItemType.HpPotion:
                // Sử dụng vật phẩm tiêu hao
               Debug.Log("Sử dụng bình hồi máu, hồi " + item.hpRestoreAmount + " HP");
                break;
            case ItemType.MpPotion:
                // Trang bị vật phẩm
                // Thêm logic trang bị ở đây
                Debug.Log("Sử dụng bình hồi mana, hồi " + item.mpRestoreAmount + " MP");
                break;
            case ItemType.Weapon:
                // Trang bị vật phẩm
                // Thêm logic trang bị ở đây
                break;
            default:
                break;
        }
        slotitem.amount--;
        if(slotitem.amount <= 0)
        {
            items.RemoveAt(index);
        }
        RenderItems();

    }
    void DropItem(itemdata item)
    {
        if(item == null) return;
        var exitingItem = items.Find(i => i.itemdata == item);
        if(exitingItem != null)
        {
            exitingItem.amount--;
            if(exitingItem.amount <= 0)
            {
                items.Remove(exitingItem);
            }
        }
        RenderItems();
    }
    void PickItem(itemdata item, int amount = 1)
    {
        if(item == null || amount <= 0) return;
        if(item.isStackable)
        {
            for (var i = 0; i < items.Count; i++)
            {
                if (items[i].itemdata != item) continue;
                if (items[i].amount >=  item.maxStack) continue;
                var spaceLeft = item.maxStack - items[i].amount;
                var toAdd = Mathf.Min(spaceLeft, amount);
                items[i] .amount += toAdd;
                amount -= toAdd;
            }
        }
        while (amount > 0 && items.Count < maxSlots)
        {
            var toAdd = item.isStackable ? Mathf.Min(item.maxStack, amount) : 1;
            items.Add( new PickupItem{ itemdata = item, amount = toAdd } );
            amount -= toAdd;
        }
        RenderItems();
    }
    void RenderItems()
    {
        // Xóa hết các slot cũ (trừ prefab gốc nếu muốn giữ làm mẫu)
        foreach (Transform child in slotPrefab.transform.parent)
        {
            if (child != slotPrefab.transform)
            {
                Destroy(child.gameObject);
            }
        }

        // Tạo lại các slot
        for (var i = 0; i < maxSlots; i++)
        {
            var slot = Instantiate(slotPrefab, slotPrefab.transform.parent);
            var iconImg = slot.transform.Find("Icon").GetComponent<Image>();
            var amountText = slot.transform.Find("Amount").GetComponent<TextMeshProUGUI>();

            iconImg.sprite = null;
            amountText.text = string.Empty;

            if (i < items.Count)
            {
                var item = items[i];
                iconImg.sprite = item.itemdata.itemIcon;
                if (item.itemdata.isStackable && item.amount > 1)
                {
                    amountText.text = item.amount.ToString();
                }
            }
            // Kích hoạt slot
            var index = i; // Capture biến i cho closure
            slot.onClick.AddListener(() =>
            {
                if(index < items.Count)
                {
                    UseItemAt(index);
                }
            });
            slot.gameObject.SetActive(true);
        }
    }
}

