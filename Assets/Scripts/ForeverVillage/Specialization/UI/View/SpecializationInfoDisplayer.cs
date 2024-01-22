using TMPro;
using UnityEngine;

namespace ForeverVillage.Scripts
{
    public class SpecializationInfoDisplayer : MonoBehaviour
    {
        [SerializeField] private TMP_Text _titleText;
        [SerializeField] private TMP_Text _descriptionText;

        public void Display(SpecializationMetadata data)
        {
            _titleText.text = data.Title;
            _descriptionText.text = data.Description;
        }
    }
}