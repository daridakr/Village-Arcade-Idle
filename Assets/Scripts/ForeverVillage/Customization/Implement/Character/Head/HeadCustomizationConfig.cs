using UnityEngine;

namespace ForeverVillage.Scripts.Character
{
    [CreateAssetMenu(fileName = "HeadCustomizationConfig", menuName = "Customization/Character/Head")]
    public class HeadCustomizationConfig : MeshCustomizationConfig
    {
        public override Customization InstantiateCustomization(MonoBehaviour target)
        {
            return new HeadCustomization((CustomizableCharacter)target, this);
        }
    }
}