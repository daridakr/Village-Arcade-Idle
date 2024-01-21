using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace ForeverVillage.Scripts
{
    [Serializable]
    public sealed class CustomizationMetadata
    {
        [SerializeField] public string Title;
        [SerializeField][PreviewField] public Sprite Icon;
    }
}