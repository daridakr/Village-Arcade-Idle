using ForeverVillage.Scripts;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GuidListProgressData", menuName = "ProgressData/GuidListProgressData", order = 51)]
public class ListGuidProgress : ProgressData
{
    private GuidListData _data;

    public IEnumerable<string> Data => _data.Guids;
    public override int CurrentProgress => _data.Count;
    public bool Contains(string guid) => _data.Contains(guid);

    public void Add(string guid)
    {
        _data.Append(guid);
    }

    public override void Load()
    {
        _data = GuidListData.Load(SaveKey);
    }

    public override void Save()
    {
        _data.SaveData(SaveKey);
    }

    [Serializable]
    private class GuidListData
    {
        [SerializeField] private List<string> _guids;

        public IEnumerable<string> Guids => _guids;

        public int Count => _guids.Count;
        public bool Contains(string guid) => _guids.Contains(guid);

        public GuidListData()
        {
            _guids = new List<string>();
        }

        public void Append(string guid)
        {
            if (_guids.Contains(guid))
            {
                throw new InvalidOperationException($"GUID {guid} already exist in progress");
            }

            _guids.Add(guid);
        }

        public void SaveData(string key)
        {
            var guidListData = JsonUtility.ToJson(this);
            PlayerPrefs.SetString(key, guidListData);
        }

        public static GuidListData Load(string key)
        {
            if (PlayerPrefs.HasKey(key) == false)
            {
                return new GuidListData();
            }

            var guidListData = PlayerPrefs.GetString(key);
            return JsonUtility.FromJson<GuidListData>(guidListData);
        }
    }
}