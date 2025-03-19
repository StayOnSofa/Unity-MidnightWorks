using CityBuilder.Inventory;
using UnityEngine;

namespace CityBuilder.Crafting
{
    [CreateAssetMenu(fileName = "CraftingRecipe", menuName = "City/Crafting/Recipe")]
    public class CraftingRecipe : ScriptableObject
    {
        [SerializeField] private ItemWithAmount _need;
        [SerializeField] private ItemWithAmount _give;

        public ItemWithAmount Need => _need;
        public ItemWithAmount Give => _give;
    }
}