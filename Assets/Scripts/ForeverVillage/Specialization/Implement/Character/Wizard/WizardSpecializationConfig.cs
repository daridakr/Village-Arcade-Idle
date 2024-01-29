using UnityEngine;

namespace Village.Character
{
    [CreateAssetMenu(fileName = "WizardSpecializationConfig", menuName = "Specialization/Character/Wizard")]
    public class WizardSpecializationConfig : CharacterSpecializationConfig
    {
        public override string MalePrefabPath => ResourcesParams.Character.Specialization.MaleWizard;
        public override string FemalePrefabPath => ResourcesParams.Character.Specialization.FemaleWizard;
        public override Specialization InstantiateSpecialization(object condition = null)
        {
            if (condition == null)
                return null;

            return new WizardSpecialization((Gender)condition, this);
        }
    }
}