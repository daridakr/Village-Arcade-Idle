using UnityEngine;

namespace Village.Character
{
    [CreateAssetMenu(fileName = "MouthCustomizationConfig", menuName = "Customization/Character/Mouth")]
    public class MouthCustomizationConfig : MeshCustomizationConfig
    {
        public override Customization InstantiateCustomization(ICustomizableCharacter customizable)
        {
            return new MouthCustomization(customizable, this);
        }
    }
}