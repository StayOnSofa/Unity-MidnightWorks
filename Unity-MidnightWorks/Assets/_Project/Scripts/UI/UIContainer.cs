using UnityEngine;

namespace CityBuilder
{
    public class UIContainer : MonoBehaviour
    {
        [SerializeField] private Transform _strataMenu;
        [SerializeField] private Transform _strataPopUp;
        
        private UIFactory _uiFactory;

        private UIView _uiMenu;
        private UIView _uiPopUp;
        
        public void Create(UIFactory factory)
        {
            _uiFactory = factory;
        }

        private void Show<T>(Transform starta, ref UIView view) where T : UIView
        {
            if (view != null && view.isActiveAndEnabled)
                view.Close();
            
            view = _uiFactory.Create<T>(starta);
        }

        public void ShowMenu<T>() where T : UIView
            => Show<T>(_strataMenu, ref _uiMenu);
        
        public void ShowPopUp<T>() where T : UIView
            => Show<T>(_strataPopUp, ref _uiPopUp);
    }
}
