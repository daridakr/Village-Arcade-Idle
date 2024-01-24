using UnityEngine;

namespace ForeverVillage.Scripts.Character
{
    [CreateAssetMenu(fileName = "MouthCustomizationConfig", menuName = "Customization/Character/Mouth")]
    public class MouthCustomizationConfig : MeshCustomizationConfig
    {
        public override Customization InstantiateCustomization(MonoBehaviour target)
        {
            return new MouthCustomization((CustomizableCharacter)target, this);
        }
    }
}