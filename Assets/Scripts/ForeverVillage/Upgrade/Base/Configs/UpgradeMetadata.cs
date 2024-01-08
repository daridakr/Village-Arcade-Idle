using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace ForeverVillage.Scripts
{
    [Serializable]
    public sealed class UpgradeMetadata
    {
        //[TranslationKey]
        //[SerializeField]
        //public string localizedTitle;
        [SerializeField] public string Title;
        [SerializeField][PreviewField] public Sprite Icon;
    }
}