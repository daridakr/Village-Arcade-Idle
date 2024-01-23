using System;
using UnityEngine;

namespace ForeverVillage.Scripts
{
    [Serializable]
    public class SpecializationData
    {
        [SerializeField] public string Title;
        [SerializeField] public Sprite Icon;
        [SerializeField] public string PrefabPath;
    }
}