using UnityEngine;

namespace ForeverVillage.Scripts.Character
{
    [CreateAssetMenu(fileName = "WizardSpecializationConfig", menuName = "Character/Specialization/Wizard")]
    public class WizardSpecializationConfig : SpecializationConfig
    {
        public override string MalePrefabPath => ResourcesParams.Character.Specialization.MaleWizard;
        public override string FemalePrefabPath => ResourcesParams.Character.Specialization.FemaleWizard;
    }
}