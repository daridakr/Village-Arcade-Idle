using UnityEngine;

namespace Village.Character
{
    [CreateAssetMenu(fileName = "BeardCustomizationConfig", menuName = "Customization/Character/Beard")]
    public class BeardCustomizationConfig : MeshCustomizationConfig
    {
        public override Customization InstantiateCustomization(ICustomizableCharacter customizable)
        {
            return new BeardCustomization(customizable, this);
        }
    }
}