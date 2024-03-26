using UnityEngine;

namespace Village.Character
{
    [CreateAssetMenu(fileName = "AdventurerSpecializationConfig", menuName = "Specialization/Character/Adventurer")]
    public class AdventurerSpecializationConfig : CharacterSpecializationConfig
    {
        public override string MalePrefabPath => ResourcesParams.Character.Specialization.MaleAdventurer;
        public override string FemalePrefabPath => ResourcesParams.Character.Specialization.FemaleAdventurer;

        public override Specialization InstantiateSpecialization(object condition = null)
        {
            if (condition == null)
                return new AdventurerSpecialization(Gender.Male, this);

            return new AdventurerSpecialization((Gender)condition, this);
        }

        private void OnValidate()
        {
            _id = nameof(AdventurerSpecialization);
        }
    }
}