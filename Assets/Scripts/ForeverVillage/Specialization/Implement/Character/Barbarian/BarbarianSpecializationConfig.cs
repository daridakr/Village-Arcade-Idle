using UnityEngine;

namespace ForeverVillage.Scripts.Character
{
    [CreateAssetMenu(fileName = "BarbarianSpecializationConfig", menuName = "Specialization/Character/Barbarian")]
    public class BarbarianSpecializationConfig : CharacterSpecializationConfig
    {
        public override string MalePrefabPath => ResourcesParams.Character.Specialization.MaleBarbarian;
        public override string FemalePrefabPath => ResourcesParams.Character.Specialization.FemaleBarbarian;
        public override Specialization InstantiateSpecialization(object condition = null)
        {
            if (condition == null)
                return null;

            return new BarbarianSpecialization((Gender)condition, this);
        }
    }
}