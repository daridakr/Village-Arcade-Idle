using TMPro;
using UnityEngine;

namespace ForeverVillage.Scripts
{
    public class VillageInfoView : MonoBehaviour
    {
        [SerializeField] private PlayerVillageInfo _villageInfo;
        [SerializeField] private TMP_Text _nameDisplay;

        private void OnEnable()
        {
            _villageInfo.Named += DisplayVillageName;
        }

        private void DisplayVillageName(string name)
        {
            _nameDisplay.text = name;
        }

        private void OnDisable()
        {
            _villageInfo.Named -= DisplayVillageName;
        }
    }
}