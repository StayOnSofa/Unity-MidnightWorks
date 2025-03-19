using CityBuilder.City;
using CityBuilder.Inventory;
using UnityEngine;
using UnityEngine.UI;

namespace CityBuilder.Views
{
    public class MapScreenView : UIView
    {
        [SerializeField] private DayCycleView _dayCycleView;
        [SerializeField] private ItemView[] _itemViews;
        
        [SerializeField] private Item _item;
        [SerializeField] private Button _craftingButton;
        
        private void Start()
        {
            foreach (var view in _itemViews)
                view.Constructor(Dependencies.UserInventory);
            
            _dayCycleView.Constructor(Dependencies.CityPresenter);
        }

        private void OnEnable()
            => _craftingButton.onClick.AddListener(OnCraftClick);

        private void OnDisable()
            => _craftingButton.onClick.RemoveListener(OnCraftClick);

        private void OnCraftClick()
            => Dependencies.CraftingPresenter.Show();
    }
}