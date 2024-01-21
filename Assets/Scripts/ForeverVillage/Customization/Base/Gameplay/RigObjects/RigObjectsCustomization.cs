using System;
using System.Collections.Generic;
using UnityEngine;

namespace ForeverVillage.Scripts
{
    public abstract class RigObjectsCustomization : Customization
    {
        private readonly RigObjectsCustomizationConfig _config;
        public override UnityEngine.Object[] Customs => _config.Customs;

        private Transform[] _rigs;
        private List<GameObject> _createdSubObjects;

        public override event Action<UnityEngine.Object> Customized;

        public RigObjectsCustomization(RigObjectsCustomizationConfig config) : base(config)
        {
            _config = config;
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
                UnityEngine.Object.Destroy(subObject.gameObject);
            }

            _createdSubObjects.Clear();
        }

        private void CreateCurrentSubObjects(int index)
        {
            foreach (Transform rig in _rigs)
            {
                _createdSubObjects.Add((GameObject)UnityEngine.Object.Instantiate((UnityEngine.Object)Customs[index], rig));
            }
        }
    }
}