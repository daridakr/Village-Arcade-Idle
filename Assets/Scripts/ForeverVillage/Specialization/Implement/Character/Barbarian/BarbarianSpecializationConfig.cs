using UnityEngine;

namespace Village.Character
{
    [CreateAssetMenu(fileName = "BarbarianSpecializationConfig", menuName = "Specialization/Character/Barbarian")]
    public class BarbarianSpecializationConfig : CharacterSpecializationConfig
    {
        public override string MalePrefabPath => ResourcesParams.Character.Specialization.MaleBarbarian;
        public override string FemalePrefabPath => ResourcesParams.Character.Specialization.FemaleBarbarian;
        public override Specialization InstantiateSpecialization(object condition = null)
        {
            if (condition == null)
                return new BarbarianSpecialization(Gender.Male, this);

            return new BarbarianSpecialization((Gender)condition, this);
        }

        private void OnValidate()
        {
            _id = nameof(BarbarianSpecialization);
        }
    }
}