using UnityEngine;

namespace Village.Character
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