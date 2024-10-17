using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Inventory : MonoBehaviour
{
    private Dictionary<ItemData, int> itemToCountMap = new Dictionary<ItemData, int>(); // アイテムとスタック数の辞書
    public GameObject inventoryPanel;         // インベントリ全体のパネル
    public GameObject itemRowPrefab;
    public ItemData iceItem;          
    public ItemData energyDrinkItem;
    private HeatStroke heatstroke;
    private LaneMovement laneMovement;

    private void Awake()
    {
        heatstroke = GetComponent<HeatStroke>();
        laneMovement = GetComponent<LaneMovement>();
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
            // アイテムデータをインベントリに追加
            AddItem(itemGet.GetitemData(), 1);

            // アイテムオブジェクトを削除
            Destroy(other.gameObject);
        }
    }

    // アイテムを追加するメソッド
    public void AddItem(ItemData item, int count)
    {



        if (itemToCountMap.ContainsKey(item))
        {
            itemToCountMap[item] += count;

            // スタック上限を超えないように
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

    // アイテムを削除するメソッド
    public void RemoveItem(ItemData item, int count)
    {
        if (itemToCountMap.ContainsKey(item))
        {
            
            if (itemToCountMap[item] <= 0)
            {
                itemToCountMap[item] = 0;
                
            }
            else {
                Debug.Log("use");
                itemToCountMap[item] -= count;
                switch (item.itemName)
                {
                    case "Ice":
                        heatstroke.currentStroke -= item.value;
                        break;

                    // 他のアイテム名に対しても特別な処理を追加可能
                    case "EnergyDrink":
                        laneMovement.DrinkEnergy(item.value);
                        break;
                }
            }
        }

        UpdateInventoryUI(itemToCountMap);
    }




    // インベントリUIを更新する
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
    


