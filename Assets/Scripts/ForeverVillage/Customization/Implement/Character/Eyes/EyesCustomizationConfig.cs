using UnityEngine;

namespace Village.Character
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