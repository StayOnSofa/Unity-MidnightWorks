using UnityEngine;
using UnityEngine.UI;

namespace CityBuilder.City
{
    public class DayCycleView : MonoBehaviour
    {
        [SerializeField] private Image _fillImage;
        
        private CityPresenter _cityPresenter;
        
        public void Constructor(CityPresenter presenter)
        {
            _cityPresenter = presenter;
        }

        private void Update()
        {
            if (_cityPresenter == null)
                return;
            
            float lerp = _cityPresenter.TimeBeforeReward / _cityPresenter.SecondsInDayCycle;
            _fillImage.fillAmount = 1f - lerp;
        }
    }
}