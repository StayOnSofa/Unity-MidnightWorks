using System;
using UnityEngine;

namespace CityBuilder
{
    public class UniqueScriptableObject : ScriptableObject
    {
        [SerializeField, ReadOnly] private string _guid;
    
        public string GUID => _guid; 
        
        private void OnValidate()
        {
            if (string.IsNullOrEmpty(_guid))
            {
                _guid = Guid.NewGuid().ToString();
#if UNITY_EDITOR
                UnityEditor.EditorUtility.SetDirty(this);
#endif
            }
        }

        private void Reset()
            => OnValidate();
    }
}