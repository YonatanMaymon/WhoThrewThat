using UnityEngine;

[CreateAssetMenu(fileName = "ShopItemData", menuName = "Scriptable Objects/ShopItemData")]
public class ShopItemData : ScriptableObject
{
    public Texture2D image;
    public string statName;
    public Enums.STATS stat;
    [Tooltip("in percentage, how much will the stat effectiveness increase per upgrade")]
    public float upgradeEffectiveness = 25f;
    public int maxLevel;
    public int[] upgradeCosts;
}
