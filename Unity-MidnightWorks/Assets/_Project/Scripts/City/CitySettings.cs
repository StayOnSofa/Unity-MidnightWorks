using System.Collections.Generic;
using CityBuilder.Inventory;
using UnityEngine;

namespace CityBuilder.City
{
    [CreateAssetMenu(fileName = "CitySettings", menuName = "City/CitySettings")]
    public class CitySettings : ScriptableObject
    {
        [SerializeField] private float _secondsInDayCycle = 60;
        [SerializeField] private ItemWithAmount[] _startingItems;
        
        public float SecondsInDayCycle => _secondsInDayCycle;
        public IReadOnlyCollection<ItemWithAmount> StartingItems => _startingItems;
    }
}