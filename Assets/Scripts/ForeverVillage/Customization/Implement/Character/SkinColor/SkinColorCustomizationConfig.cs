using UnityEngine;

namespace ForeverVillage.Scripts.Character
{
    [CreateAssetMenu(fileName = "SkinColorCustomizationConfig", menuName = "Customization/Character/SkinColor")]
    public class SkinColorCustomizationConfig : MaterialCustomizationConfig
    {
        public override Scripts.Customization InstantiateCustomization(MonoBehaviour target)
        {
            return new SkinColorCustomization((CustomizableCharacter)target, this);
        }
    }
}
