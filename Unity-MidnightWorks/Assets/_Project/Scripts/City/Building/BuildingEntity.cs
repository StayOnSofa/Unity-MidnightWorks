using CityBuilder.Inventory;
using UnityEngine;

namespace CityBuilder.City
{
    [CreateAssetMenu(fileName = "Building", menuName = "City/Building")]
    public class BuildingEntity : UniqueScriptableObject
    {
        [SerializeField] private string _title;
        [SerializeField] private Sprite _icon;
        [SerializeField] private GameObject _prefab;
        [SerializeField] private ItemWithAmount _price;
        [SerializeField] private ItemWithAmount _brings;
        
        public string Title => _title;
        public Sprite Icon => _icon;
        public GameObject Prefab => _prefab;
        public ItemWithAmount Price => _price;
        public ItemWithAmount Brings => _brings;
    }
}