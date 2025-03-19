using UnityEngine;

namespace CityBuilder
{
    public class UIFactory
    {
        private const string ViewsFolder = "Views";

        private readonly AppCoreDependencies _appCore;
        
        public UIFactory(AppCoreDependencies core)
        {
            _appCore = core;
        }

        public T Create<T>(Transform parent) where T : UIView
        {
            var prefab = Resources.Load<UIView>($"{ViewsFolder}/{typeof(T).Name}");
            var view = Object.Instantiate(prefab, parent);
            
            view.Constructor(_appCore);
            return view as T;
        }
    }
}
