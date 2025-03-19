using UnityEngine;

namespace CityBuilder.City.View
{
    public class CityMapRaycaster : MonoBehaviour
    {
        [SerializeField] private Camera _camera;

        private BuildingPlaceGraphics _currentViewPlace;
        private BuildingPlaceGraphics _prevViewPlace;

        private void CalculateRayCaster()
        {
            if (!UIHelper.IsPointerInsideScreen())
                return;

            if (UIHelper.IsOverUI())
                return;
                
            var screenRay = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(screenRay, out RaycastHit hit))
            {
                if (hit.collider.TryGetComponent(out BuildingPlaceGraphics buildingPlace))
                    _currentViewPlace = buildingPlace;
            }
        }

        private void FixedUpdate()
        {
            _currentViewPlace = null;

            CalculateRayCaster();

            if (_currentViewPlace != _prevViewPlace)
            {
                if (_prevViewPlace != null)
                    _prevViewPlace.SetHighlights(false);
                
                if (_currentViewPlace != null)
                    _currentViewPlace.SetHighlights(true);
                
                _prevViewPlace = _currentViewPlace;
            }
        }

        private void Update()
        {
            if (_currentViewPlace == null)
                return;
            
            if (UIHelper.IsTouchPress()) 
                _currentViewPlace.Click();
        }
    }
}