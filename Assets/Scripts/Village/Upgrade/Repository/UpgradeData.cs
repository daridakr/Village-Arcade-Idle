using System;
using UnityEngine;

namespace Village
{
    [Serializable]
    public class UpgradeData
    {
        [SerializeField] public string Id;
        [SerializeField] public int Level;
    }
}