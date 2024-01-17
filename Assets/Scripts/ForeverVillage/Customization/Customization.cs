using System;
using System.Collections.Generic;
using UnityEngine;

namespace ForeverVillage.Scripts
{
    [Serializable]
    public class Customization
    {
        public string DisplayName;

        public List<Renderer> Renderers;
        public List<Material> Materials;
        public List<GameObject> SubObjects;
        private int _materialIndex;
        private int _subObjectIndex;

        public void NextMaterial()
        {
            _materialIndex++;
            if (_materialIndex >= Materials.Count)
                _materialIndex = 0;

            UpdateRenderers();
        }

        public void NextSubObject()
        {
            _subObjectIndex++;
            if (_subObjectIndex >= SubObjects.Count)
                _subObjectIndex = 0;

            UpdateSubObjects();
        }

        public void UpdateSubObjects()
        {
            for (var i = 0; i < SubObjects.Count; i++)
                if (SubObjects[i])
                    SubObjects[i].SetActive(i == _subObjectIndex);
        }

        public void UpdateRenderers()
        {
            foreach (var renderer in Renderers)
                if (renderer)
                    renderer.material = Materials[_materialIndex];
        }
    }
}