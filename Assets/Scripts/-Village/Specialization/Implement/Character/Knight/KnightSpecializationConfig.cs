using UnityEngine;

namespace Village.Character
{
    [CreateAssetMenu(fileName = "KnightSpecializationConfig", menuName = "Specialization/Character/Knight")]
    public class KnightSpecializationConfig : CharacterSpecializationConfig
    {
        public override string MalePrefabPath => ResourcesParams.Character.Specialization.MaleKnight;
        public override string FemalePrefabPath => ResourcesParams.Character.Specialization.FemaleKnight;
        public override Specialization InstantiateSpecialization(object condition = null)
        {
            if (condition == null)
                return new KnightSpecialization(Gender.Male, this);

            return new KnightSpecialization((Gender)condition, this);
        }

        private void OnValidate()
        {
            _id = nameof(KnightSpecialization);
        }
    }
}