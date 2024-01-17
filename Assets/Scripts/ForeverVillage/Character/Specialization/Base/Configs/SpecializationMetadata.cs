using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace ForeverVillage.Scripts.Character
{
    [Serializable]
    public sealed class SpecializationMetadata
    {
        [SerializeField] public string Title;
        [SerializeField][TextArea] public string Description;
        [SerializeField][PreviewField] public Sprite Icon;
    }
}