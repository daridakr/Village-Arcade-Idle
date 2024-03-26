using TMPro;
using UnityEngine;

namespace Village
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