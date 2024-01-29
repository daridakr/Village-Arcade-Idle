using System;
using UnityEngine;

namespace Village
{
    [Serializable]
    public class SpecializationData
    {
        [SerializeField] public string Title;
        [SerializeField] public Sprite Icon;
        [SerializeField] public string PrefabPath;
    }
}