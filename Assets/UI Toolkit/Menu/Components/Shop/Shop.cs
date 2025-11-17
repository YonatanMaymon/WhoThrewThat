using System.Collections.Generic;
using UnityEditor;
using UnityEngine.UIElements;

[UxmlElement]
public partial class Shop : VisualElement
{
    public List<ShopItem> items { get; private set; } = new();
    private const string UxmlPath = "Assets/UI Toolkit/Menu/Components/Shop/Shop.uxml",
    ShopScrollViewName = "ShopScrollView";
    private VisualTreeAsset m_VisualTreeAsset;

    public Shop()
    {
        m_VisualTreeAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(UxmlPath);
        m_VisualTreeAsset.CloneTree(this);
    }

    public void LoadItems()
    {
        ScrollView shopScrollView = this.Q<ScrollView>(ShopScrollViewName);
        foreach (ShopItemData item in ItemLoader.instance.allShopItems)
        {
            ShopItem shopItem = new ShopItem(item);
            items.Add(shopItem);
            shopScrollView.Add(shopItem);
        }
    }

    public void UpdateLevels(Dictionary<Enums.STATS, int> statsLevels)
    {
        foreach (var item in items)
        {
            item.UpdateLevel(statsLevels[item.statType]);
        }
    }
}