using UnityEngine;

namespace Village.Character
{
    [CreateAssetMenu(fileName = "HairCustomizationConfig", menuName = "Customization/Character/Hair")]
    public class HairCustomizationConfig : MeshCustomizationConfig
    {
        public override Customization InstantiateCustomization(MonoBehaviour target)
        {
            return new HairCustomization((CustomizableCharacter)target, this);
        }
    }
}