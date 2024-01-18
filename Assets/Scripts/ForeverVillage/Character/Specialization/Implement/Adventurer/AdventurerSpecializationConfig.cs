using UnityEngine;

namespace ForeverVillage.Scripts.Character
{
    [CreateAssetMenu(fileName = "AdventurerSpecializationConfig", menuName = "Character/Specialization/Adventurer")]
    public class AdventurerSpecializationConfig : SpecializationConfig
    {
        public override string MalePrefabPath => ResourcesParams.Character.Specialization.MaleAdventurer;
        public override string FemalePrefabPath => ResourcesParams.Character.Specialization.FemaleAdventurer;
    }
}