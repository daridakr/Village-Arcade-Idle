using TMPro;
using UnityEngine;

namespace Village
{
    public class SpecializationInfoDisplayer : MonoBehaviour
    {
        [SerializeField] private TMP_Text _titleText;
        [SerializeField] private TMP_Text _descriptionText;

        public void Display(ISpecialization specialization)
        {
            _titleText.text = specialization.Title;
            _descriptionText.text = specialization.Description;
        }
    }
}