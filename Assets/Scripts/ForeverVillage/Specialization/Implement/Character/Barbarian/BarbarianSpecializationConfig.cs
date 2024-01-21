using UnityEngine;

namespace ForeverVillage.Scripts.Character
{
    [CreateAssetMenu(fileName = "BarbarianSpecializationConfig", menuName = "Character/Specialization/Barbarian")]
    public class BarbarianSpecializationConfig : SpecializationConfig
    {
        public override string MalePrefabPath => ResourcesParams.Character.Specialization.MaleBarbarian;
        public override string FemalePrefabPath => ResourcesParams.Character.Specialization.FemaleBarbarian;
    }
}