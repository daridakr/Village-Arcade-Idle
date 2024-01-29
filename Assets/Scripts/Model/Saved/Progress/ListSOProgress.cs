using System.Collections.Generic;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "SOListProgressData", menuName = "ProgressData/SOListProgressData", order = 51)]
public class ListSOProgress : ProgressData
{
    private SOListData _data;

    public IEnumerable<ScriptableObject> Data => _data.Data;
    public override int CurrentProgress => _data.Count;
    public bool Contains(ScriptableObject @object) => _data.Contains(@object);

    public void Add(ScriptableObject @object)
    {
        _data.Append(@object);
    }

    public override void Load()
    {
        _data = SOListData.Load(SaveKey);
    }

    public override void Save()
    {
        _data.SaveData(SaveKey);
    }


    [Serializable]
    private class SOListData
    {
        [SerializeField] private List<ScriptableObject> _data;

        public IEnumerable<ScriptableObject> Data => _data;

        public int Count => _data.Count;
        public bool Contains(ScriptableObject @object) => _data.Contains(@object);

        public SOListData()
        {
            _data = new List<ScriptableObject>();
        }

        public void Append(ScriptableObject @object)
        {
            if (_data.Contains(@object))
            {
                throw new InvalidOperationException($"SO {@object} already exist in progress");
            }

            _data.Add(@object);
        }

        public void SaveData(string key)
        {
            var soListData = JsonUtility.ToJson(this);
            PlayerPrefs.SetString(key, soListData);
        }

        public static SOListData Load(string key)
        {
            if (PlayerPrefs.HasKey(key) == false)
            {
                return new SOListData();
            }

            var soListData = PlayerPrefs.GetString(key);
            return JsonUtility.FromJson<SOListData>(soListData);
        }
    }
}