using System.Collections.Generic;
using UnityEngine;

namespace ForeverVillage.Scripts.Character
{
    public class Customizable : MonoBehaviour
    {
        public List<Customization> Customizations;
        int _currentCustomizationIndex;
        public Customization CurrentCustomization { get; private set; }

        void Awake()
        {
            foreach (var customization in Customizations)
            {
                customization.UpdateRenderers();
                customization.UpdateSubObjects();
            }
        }

        void Update()
        {
            SelectCustomizationWithUpDownArrows();

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                CurrentCustomization.NextMaterial();
                CurrentCustomization.NextSubObject();
            }
        }

        void SelectCustomizationWithUpDownArrows()
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
                _currentCustomizationIndex++;
            if (Input.GetKeyDown(KeyCode.UpArrow))
                _currentCustomizationIndex--;
            if (_currentCustomizationIndex < 0)
                _currentCustomizationIndex = Customizations.Count - 1;
            if (_currentCustomizationIndex >= Customizations.Count)
                _currentCustomizationIndex = 0;
            CurrentCustomization = Customizations[_currentCustomizationIndex];
        }
    }
}