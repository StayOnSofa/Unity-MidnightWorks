using System;
using System.IO;
using UnityEngine;

namespace CityBuilder.SaveStorage
{
    public interface ILocalStorage
    {
        public bool TryGetValue<T>(string key, out T result);
        public void SaveValue<T>(string key, T value);
        public void ClearValue(string key);
        public void ClearSaves();
        public bool HasSaves();
    }
    
    public class JsonStorage : ILocalStorage
    {
        private string SavePath => Path.Combine(Application.persistentDataPath, "JsonStorage");
        
        public void ClearSaves()
        {
            if (HasSaves())
                Directory.Delete(SavePath, true);
        }
        
        public bool HasSaves()
            => Directory.Exists(SavePath);

        private void CreateDirectoryIfNotExists()
        {
            if (!HasSaves())
                Directory.CreateDirectory(SavePath);
        }

        public bool TryGetValue<T>(string key, out T result)
        {
            result = default;
            var saveFile = Path.Combine(SavePath, $"{key}.json");

            if (File.Exists(saveFile))
            {
                var json = File.ReadAllText(saveFile);
                
                if (typeof(T).IsClass || typeof(T).IsArray)
                {
                    try
                    {
                        result = JsonUtility.FromJson<T>(json);
                        
                        return true;
                    }
                    catch (Exception e)
                    {
                        Debug.LogException(e);
                    }
                }
                else if (typeof(T).IsValueType)
                {
                    try
                    {
                        result = (T)Convert.ChangeType(json, typeof(T));
                        return true;
                    }
                    catch (Exception e)
                    {
                        Debug.LogException(e);
                    }
                }
            }
            
            return false;
        }

        public void ClearValue(string key)
        {
            var saveFile = Path.Combine(SavePath, $"{key}.json");
            
            if (File.Exists(saveFile))
                File.Delete(saveFile);
        }

        public void SaveValue<T>(string key, T value)
        {
            CreateDirectoryIfNotExists();
            var saveFile = Path.Combine(SavePath, $"{key}.json");
            
            if (typeof(T).IsClass || typeof(T).IsArray)
            {
                try
                {
                    var json = JsonUtility.ToJson(value);
                    File.WriteAllText(saveFile, json);
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                }
            }
            else if (typeof(T).IsValueType)
            {
                try
                {
                    string valueString = Convert.ToString(value);
                    File.WriteAllText(saveFile, valueString);
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                }
            }
        }
    }
}