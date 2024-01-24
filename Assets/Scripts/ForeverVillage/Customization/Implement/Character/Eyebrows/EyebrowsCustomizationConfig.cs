using UnityEngine;

namespace ForeverVillage.Scripts.Character
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