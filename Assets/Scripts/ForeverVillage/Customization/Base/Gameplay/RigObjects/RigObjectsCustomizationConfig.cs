using UnityEngine;

namespace ForeverVillage.Scripts
{
    public abstract class RigObjectsCustomizationConfig : CustomizationConfig
    {
        [SerializeField] private GameObject[] _objects;
        public override Object[] Customs => _objects;
    }
}