using TMPro;
using UnityEngine;
using Zenject;

namespace ForeverVillage.Scripts
{
    public class PlayerNameView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _nameDisplay;

        private PlayerName _playerName;

        [Inject]
        public virtual void Construct(PlayerName playerName)
        {
            _playerName = playerName;
        }

        private void OnEnable()
        {
            _playerName.Setuped += DisplayVillageName;
        }

        private void DisplayVillageName(string name)
        {
            _nameDisplay.text = name;
        }

        private void OnDisable()
        {
            _playerName.Setuped -= DisplayVillageName;
        }
    }
}