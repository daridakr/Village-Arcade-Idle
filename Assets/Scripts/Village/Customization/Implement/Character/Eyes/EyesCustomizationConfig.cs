using UnityEngine;

namespace Village.Character
{
    [CreateAssetMenu(fileName = "EyesCustomizationConfig", menuName = "Customization/Character/Eyes")]
    public class EyesCustomizationConfig : MeshCustomizationConfig
    {
        public override Customization InstantiateCustomization(ICustomizableCharacter customizable)
        {
            return new EyesCustomization(customizable, this);
        }
    }
}