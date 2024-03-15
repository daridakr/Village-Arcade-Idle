using UnityEngine;

namespace Village.Character
{
    [CreateAssetMenu(fileName = "ArcherSpecializationConfig", menuName = "Specialization/Character/Archer")]
    public class ArcherSpecializationConfig : CharacterSpecializationConfig
    {
        public override string MalePrefabPath => ResourcesParams.Character.Specialization.MaleArcher;
        public override string FemalePrefabPath => ResourcesParams.Character.Specialization.FemaleArcher;

        public override Specialization InstantiateSpecialization(object condition = null)
        {
            if (condition == null)
                return new ArcherSpecialization(Gender.Male, this);

            return new ArcherSpecialization((Gender)condition, this);
        }

        private void OnValidate()
        {
            _id = nameof(ArcherSpecialization);
        }
    }
}