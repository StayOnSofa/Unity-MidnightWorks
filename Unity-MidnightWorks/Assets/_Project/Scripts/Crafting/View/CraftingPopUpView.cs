using CityBuilder.Inventory;
using UnityEngine;
using UnityEngine.UI;

namespace CityBuilder.Crafting.View
{
    public class CraftingPopUpView : UIView
    {
        [SerializeField] private Button[] _closeButtons;
        [SerializeField] private CraftingRecipeView _recipeViewPrefab;
        [SerializeField] private Transform _content;
        
        private CraftingPresenter _presenter => Dependencies.CraftingPresenter;
        private UserInventory _inventory => Dependencies.UserInventory;
        
        private void Start()
        {
            CreateContentList();
        }

        private void CreateContentList()
        {
            var recipes = _presenter.GetAllRecipes();

            foreach (var r in recipes)
            {
                var slide = Instantiate(_recipeViewPrefab, _content);
                slide.Constructor(_inventory, r);

                slide.OnCraft += Craft;
            }
        }

        private void Craft(CraftingRecipe recipe)
        {
            _presenter.TryCraft(recipe);
        }

        private void OnEnable()
        {
            foreach (var closeButton in _closeButtons)
                closeButton.onClick.AddListener(Close);
        }

        private void OnDisable()
        {
            foreach (var closeButton in _closeButtons)
                closeButton.onClick.RemoveListener(Close);
        }
    }
}