using ForeverVillage;
using UnityEngine;

namespace Village
{
    [CreateAssetMenu(fileName = "WoodItemConfig", menuName = "Item/Resources/Wood")]
    public class WoodItemConfig : ItemConfig
    {
        public override Item InstantiateItem()
        {
            return new Wood(this);
        }
    }
}