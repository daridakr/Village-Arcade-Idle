using UnityEngine;

namespace Village.Character
{
    [CreateAssetMenu(fileName = "EyebrowsCustomizationConfig", menuName = "Customization/Character/Eyebrows")]
    public class EyebrowsCustomizationConfig : MeshCustomizationConfig
    {
        public override Customization InstantiateCustomization(MonoBehaviour target)
        {
            return new EyebrowsCustomization((CustomizableCharacter)target, this);
        }
    }
}