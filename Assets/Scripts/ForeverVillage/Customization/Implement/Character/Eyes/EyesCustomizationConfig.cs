using UnityEngine;

namespace ForeverVillage.Scripts.Character
{
    [CreateAssetMenu(fileName = "EyesCustomizationConfig", menuName = "Customization/Character/Eyes")]
    public class EyesCustomizationConfig : MeshCustomizationConfig
    {
        public override Customization InstantiateCustomization(MonoBehaviour target)
        {
            return new EyesCustomization((CustomizableCharacter)target, this);
        }
    }
}