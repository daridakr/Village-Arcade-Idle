using UnityEngine;

namespace Village.Character
{
    [CreateAssetMenu(fileName = "RogueSpecializationConfig", menuName = "Specialization/Character/Rogue")]
    public class RogueSpecializationConfig : CharacterSpecializationConfig
    {
        public override string MalePrefabPath => ResourcesParams.Character.Specialization.MaleRogue;
        public override string FemalePrefabPath => ResourcesParams.Character.Specialization.FemaleRogue;
        public override Specialization InstantiateSpecialization(object condition = null)
        {
            if (condition == null)
                return null;

            return new RogueSpecialization((Gender)condition, this);
        }
    }
}