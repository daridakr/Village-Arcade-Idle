using UnityEngine;

namespace Village.Character
{
    [CreateAssetMenu(fileName = "BeardCustomizationConfig", menuName = "Customization/Character/Beard")]
    public class BeardCustomizationConfig : MeshCustomizationConfig
    {
        public override Customization InstantiateCustomization(MonoBehaviour target)
        {
            return new BeardCustomization((CustomizableCharacter)target, this);
        }
    }
}