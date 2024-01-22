using System.Collections.Generic;
using UnityEngine;

namespace ForeverVillage.Scripts
{
    public abstract class RigObjectsCustomization : Customization
    {
        private readonly RigObjectsCustomizationConfig _config;
        private List<GameObject> _createdSubObjects;

        protected List<Transform> _rigs;

        public override Object[] Customs => _config.Customs;

        public RigObjectsCustomization(RigObjectsCustomizationConfig config) : base(config)
        {
            _config = config;

            _rigs = new List<Transform>();
            _createdSubObjects = new List<GameObject>();
        }

        public override void ApplyCustom(int index)
        {
            if (_rigs == null)
                return;

            DestroyPreviousSubObjects();
            CreateCurrentSubObjects(index);
        }

        private void DestroyPreviousSubObjects()
        {
            foreach (GameObject subObject in _createdSubObjects)
            {
                Object.Destroy(subObject.gameObject);
            }

            _createdSubObjects.Clear();
        }

        private void CreateCurrentSubObjects(int index)
        {
            foreach (Transform rig in _rigs)
            {
                _createdSubObjects.Add((GameObject)Object.Instantiate(Customs[index], rig));
            }
        }
    }
}