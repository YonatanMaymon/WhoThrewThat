using UnityEditor;
using UnityEngine.UIElements;

[UxmlElement]
public partial class Shop : VisualElement
{
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
            shopScrollView.Add(new ShopItem(item));
        }
    }
}