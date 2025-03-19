using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CityBuilder
{
       public static class UIHelper
    {
        private static PointerEventData _pointerEventData;
        private static List<RaycastResult> _rayCastResults = new List<RaycastResult>();

        public static bool IsOverUI()
        {
            return IsOverUI(Input.mousePosition);
        }

        public static bool IsOverUI(Vector3 position)
        {
            _pointerEventData = new PointerEventData(EventSystem.current) { position = position };
            _rayCastResults.Clear();

            EventSystem.current.RaycastAll(_pointerEventData, _rayCastResults);
            return _rayCastResults.Count > 0;
        }
        
        public static bool IsPointerInsideScreen()
        {
            var mousePosition = Input.mousePosition;
            if (mousePosition.x >= 0 && mousePosition.x <= Screen.width && mousePosition.y >= 0 &&
                mousePosition.y <= Screen.height)
                return true;

            return false;
        }
        
        public static bool IsTouchPress()
        {
            return Input.GetMouseButtonDown(0);
        }

        public static bool IsTouching()
        {
            return Input.GetMouseButton(0); 
        }

        public static bool IsTouchUp()
        {
            return Input.GetMouseButtonUp(0); 
        }

        public static GameObject GetPointerUI()
        {
            return GetPointerUI(Input.mousePosition);
        }
        
        public static bool TryGetPointerGameObject(out GameObject uiObject)
        {
            var uObject = GetPointerUI(Input.mousePosition);
            uiObject = uObject;

            return uObject != null;
        }

        public static bool TryGetPointer<T>(out T component)
        {
            if (TryGetPointerGameObject(out GameObject uiObject))
            {
                if (uiObject.TryGetComponent(out component))
                    return true;
            }

            component = default;
            return false;
        }
        
        public static bool TryGetAnyPointer<T>(out T component) where T : MonoBehaviour
        {
            var view = FindAnyComponentOverPointer<T>(Input.mousePosition);
            
            if (view != null)
            {
                component = view;
                return true;
            }

            component = default;
            return false;
        }
        
        public static T FindAnyComponentOverPointer<T>(Vector3 position) where T : MonoBehaviour
        {
            _pointerEventData = new PointerEventData(EventSystem.current) { position = position };
            _rayCastResults.Clear();

            EventSystem.current.RaycastAll(_pointerEventData, _rayCastResults);

            foreach (var result in _rayCastResults)
            {
                if (result.gameObject.TryGetComponent(out T component))
                    return component;
            }
            
            return null;
        }


        public static GameObject GetPointerUI(Vector3 position)
        {
            _pointerEventData = new PointerEventData(EventSystem.current) { position = position };
            _rayCastResults.Clear();

            EventSystem.current.RaycastAll(_pointerEventData, _rayCastResults);

            if (_rayCastResults.Count > 0)
                return _rayCastResults[0].gameObject;

            return null;
        }
        

        public static bool GetNormalizedPosition(RectTransform rectTransform, out Vector2 normalizedPosition)
        {
            _pointerEventData = new PointerEventData(EventSystem.current) { position = Input.mousePosition };
            normalizedPosition = default;

            if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, _pointerEventData.position,
                    _pointerEventData.pressEventCamera, out var localPosition))
                return false;
            normalizedPosition = Rect.PointToNormalized(rectTransform.rect, localPosition);

            return true;
        }
    }
}