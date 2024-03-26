using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Village
{
    public class BuildingStatView : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _value;

        public void Render(Sprite sprite, string value)
        {
            _icon.sprite = sprite;
            _value.text = value;
        }
    }
}