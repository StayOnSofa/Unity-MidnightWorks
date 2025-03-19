using UnityEngine;

namespace CityBuilder.Inventory
{
    [CreateAssetMenu(fileName = "City", menuName = "City/Item")]
    public class Item : UniqueScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private Sprite _icon;

        public string Name => _name;
        public Sprite Icon => _icon;
        
        public override int GetHashCode()
            => GUID.GetHashCode();
    }
}