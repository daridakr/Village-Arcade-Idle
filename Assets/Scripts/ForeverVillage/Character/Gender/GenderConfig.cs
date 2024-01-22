using Sirenix.OdinInspector;
using UnityEngine;

namespace ForeverVillage.Scripts.Character
{
    [CreateAssetMenu(fileName = "NewGenderConfig", menuName = "Character/Creation/Gender")]
    public sealed class GenderConfig : ScriptableObject
    {
        [SerializeField] private Gender _gender;
        [SerializeField][PreviewField] private Sprite _icon;

        public Gender Gender => _gender;
        public Sprite Icon => _icon;
    }
}