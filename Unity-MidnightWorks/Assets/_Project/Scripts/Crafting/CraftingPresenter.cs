using CityBuilder.Crafting.View;

namespace CityBuilder.Crafting
{
    public class CraftingPresenter
    {
        private readonly CraftingSystem _craftingSystem;
        private readonly UIContainer _uiContainer;
        
        public CraftingPresenter(UIContainer container, CraftingSystem craftingSystem)
        {
            _uiContainer = container;
            _craftingSystem = craftingSystem;
        }

        public void Show()
            => _uiContainer.ShowPopUp<CraftingPopUpView>();
        
        public CraftingRecipe[] GetAllRecipes()
            => _craftingSystem.GetAllRecipes();
        
        public bool TryCraft(CraftingRecipe recipe)
            => _craftingSystem.TryCraft(recipe);
    }
}