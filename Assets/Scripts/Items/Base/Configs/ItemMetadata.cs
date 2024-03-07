using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace ForeverVillage
{
    [Serializable]
    public sealed class ItemMetadata
    {
        [SerializeField] public string Name;
        [SerializeField] public string Description;
        [SerializeField][PreviewField] public Sprite Icon;
    }
}