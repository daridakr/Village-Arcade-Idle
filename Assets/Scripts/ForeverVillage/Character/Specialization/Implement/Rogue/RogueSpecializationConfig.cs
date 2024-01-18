using UnityEngine;

namespace ForeverVillage.Scripts.Character
{
    [CreateAssetMenu(fileName = "RogueSpecializationConfig", menuName = "Character/Specialization/Rogue")]
    public class RogueSpecializationConfig : SpecializationConfig
    {
        public override string MalePrefabPath => ResourcesParams.Character.Specialization.MaleRogue;
        public override string FemalePrefabPath => ResourcesParams.Character.Specialization.FemaleRogue;
    }
}