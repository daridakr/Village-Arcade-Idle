using TMPro;
using UnityEngine;

namespace ForeverVillage.Scripts
{
    public class CustomizationInfoDisplayer : MonoBehaviour
    {
        [SerializeField] private TMP_Text _titleText;

        public void Display(ICustomization customization)
        {
            _titleText.text = customization.Title;
        }
    }
}