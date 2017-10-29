using System;
using UnityEngine;

[Serializable]
public class ShopItem : ScriptableObject
{
    public string itemName = "Enter item name here";
    public int itemPrice = 100;
    public int movementSpeed = 30;
    public bool isDefault = false;
    public bool isSelected = false;
    public bool isPurchased = false;
}
