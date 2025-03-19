using CityBuilder.City;
using CityBuilder.Crafting;
using CityBuilder.Inventory;
using CityBuilder.SaveStorage;
using UnityEngine;
using UnityEngine.Events;

namespace CityBuilder
{
    public class AppCoreDependencies : MonoBehaviour
    {
        [SerializeField] public UnityEvent OnFinish;
        [SerializeField] private UIContainer _uiContainer;
        
        private UIFactory _uiFactory;
        private CraftingSystem _craftingSystem;
        private CitySystem _citySystem;
        
        public UIContainer UIContainer => _uiContainer;
        public ILocalStorage LocalStorage { get; private set; }
        public UserInventory UserInventory { get; private set; }
        public CraftingPresenter CraftingPresenter { get; private set; }
        public CityPresenter CityPresenter { get; private set; }
        
        private void Awake()
        {
            BindUI();
            BindStorage();
            BindInventory();
            BindCityMap();

            OnFinish?.Invoke();
        }

        private void BindStorage()
        {
            LocalStorage = new JsonStorage();
        }

        private void BindInventory()
        {
            UserInventory = new UserInventory(this);
            
            _craftingSystem = new CraftingSystem(UserInventory);
            CraftingPresenter = new CraftingPresenter(UIContainer, _craftingSystem);
        }

        private void BindCityMap()
        {
            _citySystem = new CitySystem(UserInventory, LocalStorage);
            CityPresenter = new CityPresenter(_citySystem, UIContainer);
        }

        private void BindUI()
        {
            _uiFactory = new UIFactory(this);
            _uiContainer.Create(_uiFactory);
        }

        private void Update()
        {
            float dt = Time.deltaTime;
            
            if (_citySystem != null)
                _citySystem.Tick(dt);
        }
    }
}
