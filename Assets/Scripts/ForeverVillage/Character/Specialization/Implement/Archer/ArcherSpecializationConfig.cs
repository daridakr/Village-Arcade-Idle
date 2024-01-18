using UnityEngine;

namespace ForeverVillage.Scripts.Character
{
    [CreateAssetMenu(fileName = "ArcherSpecializationConfig", menuName = "Character/Specialization/Archer")]
    public class ArcherSpecializationConfig : SpecializationConfig
    {
        public override string MalePrefabPath => ResourcesParams.Character.Specialization.MaleArcher;
        public override string FemalePrefabPath => ResourcesParams.Character.Specialization.FemaleArcher;
    }
}