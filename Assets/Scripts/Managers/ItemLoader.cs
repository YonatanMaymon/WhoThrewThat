using UnityEngine;
using System.Collections.Generic;

public class ItemLoader : MonoBehaviour
{
    public static ItemLoader instance { get; private set; }
    public List<ShopItemData> allShopItems { get; private set; }
    public Dictionary<Enums.STATS, float> statsEffectiveness { get; private set; } = new();

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        LoadAllShopItems();
    }


    private void LoadAllShopItems()
    {
        // Load from the "Resources/ShopItems" folder
        allShopItems = new List<ShopItemData>(Resources.LoadAll<ShopItemData>("ShopItems"));
        foreach (var item in allShopItems)
        {
            statsEffectiveness.Add(item.stat, item.upgradeEffectiveness);
        }
    }
}