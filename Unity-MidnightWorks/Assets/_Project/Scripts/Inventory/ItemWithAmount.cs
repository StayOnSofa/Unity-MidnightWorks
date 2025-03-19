using System;
using UnityEngine;

namespace CityBuilder.Inventory
{
    [Serializable]
    public class ItemWithAmount
    {
        [SerializeField] public Item Item;
        [SerializeField] public int Amount;
    }
}