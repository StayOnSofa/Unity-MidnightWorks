using UnityEngine;

namespace CityBuilder.City
{
    public class SunDayCycleView : MonoBehaviour
    {
        [SerializeField] private AppCoreDependencies _dependencies;
        [SerializeField] private Light _sunLight;

        private CityPresenter _cityPresenter => _dependencies.CityPresenter;
        
        private float Lerp3(float a, float b, float c, float t)
        {
            if (t < 0.5f)
                return Mathf.Lerp(a, b, t * 2);
            
            return Mathf.Lerp(b, c, (t - 0.5f) * 2);
        }
        
        public void Update()
        {
            if (_cityPresenter == null)
                return;
            
            float lerp = _cityPresenter.TimeBeforeReward / _cityPresenter.SecondsInDayCycle;
            _sunLight.intensity = Lerp3(1f, 0, 1f, lerp);
        }
    }
}