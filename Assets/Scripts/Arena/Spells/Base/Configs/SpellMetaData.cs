using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Arena
{
    [Serializable]
    public sealed class SpellMetaData
    {
        [SerializeField] public string Title;
        [SerializeField][TextArea] public string Description;
        [SerializeField][PreviewField] public Sprite Icon;
    }
}