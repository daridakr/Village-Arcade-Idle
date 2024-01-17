using UnityEngine;

namespace ForeverVillage.Scripts.Character
{
    [CreateAssetMenu(fileName = "KnightSpecializationConfig", menuName = "Character/Specialization/Knight")]
    public class KnightSpecializationConfig : SpecializationConfig
    {
        public override string MalePrefabPath => ResourcesParams.Character.Specialization.MaleKnight;
        public override string FemalePrefabPath => ResourcesParams.Character.Specialization.FemaleKnight;
    }
}