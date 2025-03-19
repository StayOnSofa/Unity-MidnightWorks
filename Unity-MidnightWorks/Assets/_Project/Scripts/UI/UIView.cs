using UnityEngine;

namespace CityBuilder
{
    public class UIView : MonoBehaviour
    {
        protected AppCoreDependencies Dependencies { private set; get; }

        public void Constructor(AppCoreDependencies core)
        {
            Dependencies = core;
        }

        public void Close()
            => Destroy(gameObject);
    }
}
