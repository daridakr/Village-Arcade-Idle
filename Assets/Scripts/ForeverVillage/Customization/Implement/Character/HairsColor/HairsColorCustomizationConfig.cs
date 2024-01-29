using UnityEngine;

namespace Village.Character
{
    [CreateAssetMenu(fileName = "HairsColorCustomizationConfig", menuName = "Customization/Character/HairsColor")]
    public class HairsColorCustomizationConfig : MaterialCustomizationConfig
    {
        public override Customization InstantiateCustomization(MonoBehaviour target)
        {
            return new HairsColorCustomization((CustomizableCharacter)target, this);
        }
    }
}