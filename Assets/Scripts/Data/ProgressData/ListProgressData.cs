using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewListProgressData", menuName = "ProgressData/ListProgressData", order = 51)]
public class ListProgressData : ProgressData
{
    private GuidData _data;

    public IEnumerable<string> Data => _data.Data;
    public override int CurrentProgress => _data.Progress;
    public bool Contains(string guid) => _data.Contains(guid);

    public void Add(string guid)
    {
        _data.Add(guid);
        //Updated?.Invoke();
    }

    public override void Load()
    {
        _data = GuidData.Load(SaveKey);
    }

    public override void Save()
    {
        _data.Save(SaveKey);
    }

    [Serializable]
    private class GuidData
    {
        [SerializeField] private List<string> _guidList;

        public IEnumerable<string> Data => _guidList;

        public int Progress => _guidList.Count;
        public bool Contains(string guid) => _guidList.Contains(guid);

        public GuidData()
        {
            _guidList = new List<string>();
        }

        public void Add(string guid)
        {
            if (_guidList.Contains(guid))
                throw new InvalidOperationException($"GUID {guid} already exist in progress");

            _guidList.Add(guid);
        }

        public void Save(string key)
        {
            var json = JsonUtility.ToJson(this);
            PlayerPrefs.SetString(key, json);
        }

        public static GuidData Load(string key)
        {
            if (PlayerPrefs.HasKey(key) == false)
                return new GuidData();

            var json = PlayerPrefs.GetString(key);
            return JsonUtility.FromJson<GuidData>(json);
        }
    }
}
