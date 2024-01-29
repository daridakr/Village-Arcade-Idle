using UnityEngine;

namespace Village.Character
{
    [CreateAssetMenu(fileName = "SkinColorCustomizationConfig", menuName = "Customization/Character/SkinColor")]
    public class SkinColorCustomizationConfig : MaterialCustomizationConfig
    {
        public override Customization InstantiateCustomization(MonoBehaviour target)
        {
            return new SkinColorCustomization((CustomizableCharacter)target, this);
        }
    }
}
