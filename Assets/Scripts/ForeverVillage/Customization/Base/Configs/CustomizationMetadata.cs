using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Village
{
    [Serializable]
    public sealed class CustomizationMetadata
    {
        [SerializeField] public string Title;
        [SerializeField][PreviewField] public Sprite Icon;
    }
}