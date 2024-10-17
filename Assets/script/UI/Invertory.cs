using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Inventory : MonoBehaviour
{
    private Dictionary<ItemData, int> itemToCountMap = new Dictionary<ItemData, int>(); // �A�C�e���ƃX�^�b�N���̎���
    public GameObject inventoryPanel;         // �C���x���g���S�̂̃p�l��
    public GameObject itemRowPrefab;
    public ItemData iceItem;          
    public ItemData energyDrinkItem;
    private HeatStroke heatstroke;
    private ShadowCollider shadowcollider;

    private void Awake()
    {
        heatstroke = GetComponent<HeatStroke>();
        shadowcollider = GetComponent<ShadowCollider>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            RemoveItem(iceItem, 1);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            RemoveItem(energyDrinkItem, 1);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Item itemGet = other.GetComponent<Item>();
        if (itemGet != null)
        {
            // �A�C�e���f�[�^���C���x���g���ɒǉ�
            AddItem(itemGet.GetitemData(), 1);

            // �A�C�e���I�u�W�F�N�g���폜
            Destroy(other.gameObject);
        }
    }

    // �A�C�e����ǉ����郁�\�b�h
    public void AddItem(ItemData item, int count)
    {



        if (itemToCountMap.ContainsKey(item))
        {
            itemToCountMap[item] += count;

            // �X�^�b�N����𒴂��Ȃ��悤��
            if (itemToCountMap[item] > item.maxStackSize)
            {
                itemToCountMap[item] = item.maxStackSize;
            }
        }
        else
        {
            itemToCountMap[item] = Mathf.Min(count, item.maxStackSize);
        }

        UpdateInventoryUI(itemToCountMap);


    }

    // �A�C�e�����폜���郁�\�b�h
    public void RemoveItem(ItemData item, int count)
    {
        if (itemToCountMap.ContainsKey(item))
        {
            
            if (itemToCountMap[item] <= 0)
            {
                itemToCountMap[item] = 0;
                
            }
            else {
            itemToCountMap[item] -= count;
                switch (item.itemName)
                {
                    case "Ice":
                        heatstroke.currentStroke -= item.value;
                        break;

                    // ���̃A�C�e�����ɑ΂��Ă����ʂȏ�����ǉ��\
                    case "EnergyDrink":
                        shadowcollider.SetIfShadowForDuration(item.value);
                        break;
                }
            }
        }

        UpdateInventoryUI(itemToCountMap);
    }

    public void UseItem() {

        if (Input.GetButton("Q")) { 
        
        }
    
    
    }




    // �C���x���g��UI���X�V����
    public void UpdateInventoryUI(Dictionary<ItemData, int> items)
    {
        Debug.Log("UIupdate");
        foreach (Transform child in inventoryPanel.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (var itemEntry in items)
        {
            GameObject itemRow = Instantiate(itemRowPrefab, inventoryPanel.transform);
            int itemCount = itemEntry.Value;
            Debug.Log(itemCount);
            for (int i = 0; i < itemEntry.Value; i++)
            {
                GameObject slot = Instantiate(itemEntry.Key.itemSlot, itemRow.transform);

            }
        }
    }
}
    


