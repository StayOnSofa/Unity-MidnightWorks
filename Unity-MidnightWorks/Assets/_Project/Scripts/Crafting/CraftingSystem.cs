using CityBuilder.Inventory;
using UnityEngine;

namespace CityBuilder.Crafting
{
    public class CraftingSystem
    {
        private const string RecipesFolder = "ScriptableObjects/Recipes";
        
        private readonly UserInventory _userInventory;
        private readonly CraftingRecipe[] _recipes;
        
        public CraftingSystem(UserInventory inventory)
        {
            _userInventory = inventory;
            _recipes = Resources.LoadAll<CraftingRecipe>(RecipesFolder);
        }

        public CraftingRecipe[] GetAllRecipes()
            => _recipes;

        public bool TryCraft(CraftingRecipe recipe)
        {
            var need = recipe.Need;
            var reward = recipe.Give;
            
            if (_userInventory.TryRemoveItem(need.Item, need.Amount))
            {
                _userInventory.AddItem(reward.Item, reward.Amount);
                return true;
            }

            return false;
        }
    }
}