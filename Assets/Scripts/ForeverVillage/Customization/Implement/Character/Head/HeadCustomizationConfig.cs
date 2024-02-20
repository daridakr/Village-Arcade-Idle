using UnityEngine;

namespace Village.Character
{
    [CreateAssetMenu(fileName = "HeadCustomizationConfig", menuName = "Customization/Character/Head")]
    public class HeadCustomizationConfig : MeshCustomizationConfig
    {
        public override Customization InstantiateCustomization(ICustomizableCharacter customizable)
        {
            return new HeadCustomization(customizable, this);
        }
    }
}