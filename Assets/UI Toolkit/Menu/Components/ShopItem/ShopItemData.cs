using UnityEngine;

[CreateAssetMenu(fileName = "ShopItemData", menuName = "Scriptable Objects/ShopItemData")]
public class ShopItemData : ScriptableObject
{
    public Texture2D image;
    public string statName;
    public int maxLevel;
    public int[] upgradeCosts;
}
