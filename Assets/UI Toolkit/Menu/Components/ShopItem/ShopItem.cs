using UnityEngine.UIElements;
using UnityEditor;
using System;
using UnityEngine;
using static ShopItemConsts;
[UxmlElement]
public partial class ShopItem : VisualElement
{
    public int upgradePrice { get; private set; } = 0;
    public Enums.STATS statType { get; private set; }
    private Action m_CachedClickListener;
    private VisualTreeAsset m_VisualTreeAsset;
    private SegmentedProgressBar progressBar;
    private Label levelLabel;
    private Button upgradeButton;
    private ShopItemData _data;

    public ShopItem()
    {
        m_VisualTreeAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(UxmlPath);
        m_VisualTreeAsset.CloneTree(this);
        progressBar = this.Q<SegmentedProgressBar>(ProgressBarName);
        levelLabel = this.Q<Label>(LevelLabelName);
        upgradeButton = this.Q<Button>(UpgradeButtonName);
    }
    public ShopItem(ShopItemData data) : this()
    {
        SetData(data);
    }

    public void SetData(ShopItemData data)
    {
        this.Q<VisualElement>(ImageContainerName).style.backgroundImage = data.image;
        this.Q<Label>(StatNameLabelName).text = data.statName;
        _data = data;
        statType = data.stat;
        SetMaxLevel();
    }

    public void SubscribeToUpgradeClick(Action<ShopItem> action)
    {
        if (m_CachedClickListener != null)
        {
            upgradeButton.clicked -= m_CachedClickListener;
        }
        m_CachedClickListener = () => action(this);
        upgradeButton.clicked += m_CachedClickListener;
    }

    public void UnsubscribeFromUpgradeClick()
    {
        if (upgradeButton != null && m_CachedClickListener != null)
        {
            upgradeButton.clicked -= m_CachedClickListener;
            m_CachedClickListener = null;
        }
    }

    private void SetMaxLevel()
    {
        progressBar.SetMaxLevel(_data.maxLevel);
        UpdateLevel(0);
    }

    public void UpdateLevel(int level)
    {
        level = Mathf.Clamp(level, 0, _data.maxLevel);
        progressBar.UpdateProgress(level);
        levelLabel.text = $"Level {level}/{_data.maxLevel}";
        SetUpgradePrice(level);
    }

    private void SetUpgradePrice(int level)
    {

        int levelCostIndex = Mathf.Clamp(level, 0, _data.upgradeCosts.Length - 1);

        if (level == _data.maxLevel)
        {
            upgradePrice = int.MaxValue;
            upgradeButton.text = "Maxed Out";
        }
        else
        {
            upgradePrice = _data.upgradeCosts[levelCostIndex];
            upgradeButton.text = $"Upgrade ({upgradePrice} Coins)";
        }
    }
}