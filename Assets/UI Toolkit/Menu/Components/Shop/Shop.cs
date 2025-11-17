using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.UIElements;

[UxmlElement]
public partial class Shop : VisualElement
{
    private const string UxmlPath = "Assets/UI Toolkit/Menu/Components/Shop/Shop.uxml",
    ShopScrollViewName = "ShopScrollView",
    BackButtonName = "BackButton",
    CoinCounterLabelName = "CoinCounterLabel";

    public List<ShopItem> items { get; private set; } = new();

    private VisualTreeAsset m_VisualTreeAsset;
    private Button backButton;
    private Label coinCounterLabel;

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
        backButton = this.Q<Button>(BackButtonName);
        coinCounterLabel = this.Q<Label>(CoinCounterLabelName);
    }


    public void SubscribeToBackClick(Action action)
    {
        backButton.clicked += action;
    }

    public void UnsubscribeFromBackClick(Action action)
    {
        backButton.clicked -= action;
    }

    public void UpdateShop(Dictionary<Enums.STATS, int> statsLevels, int coins)
    {
        foreach (var item in items)
        {
            item.UpdateLevel(statsLevels[item.statType]);
        }
        coinCounterLabel.text = coins + "";
    }
}