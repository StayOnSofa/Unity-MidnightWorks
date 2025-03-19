using CityBuilder.MainScreen;
using UnityEngine;

namespace CityBuilder
{
    public class AppBootstrap : MonoBehaviour
    {
        [SerializeField] private AppCoreDependencies _dependencies;

        public void Setup()
        {
            _dependencies.UIContainer.ShowMenu<MainScreenView>();
        }
    }
}