using UnityEngine;

namespace Village.Character
{
    [CreateAssetMenu(fileName = "HairsColorCustomizationConfig", menuName = "Customization/Character/HairsColor")]
    public class HairsColorCustomizationConfig : MaterialCustomizationConfig
    {
        public override Customization InstantiateCustomization(ICustomizableCharacter customizable)
        {
            return new HairsColorCustomization(customizable, this);
        }
    }
}