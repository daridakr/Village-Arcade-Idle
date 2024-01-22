using UnityEngine;

namespace ForeverVillage.Scripts.Character
{
    [CreateAssetMenu(fileName = "NewGendersCatalog", menuName = "Character/Creation/GenderCatalog")]
    public sealed class GendersCatalog : ScriptableObject
    {
        [SerializeField] private GenderConfig[] _data;

        public GenderConfig[] GetAllGenders()
        {
            return _data;
        }
    }
}