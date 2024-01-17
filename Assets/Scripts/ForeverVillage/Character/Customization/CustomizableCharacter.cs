using UnityEngine;

namespace ForeverVillage.Scripts.Character
{
    [RequireComponent(typeof(Character))]
    public class CustomizableCharacter : MonoBehaviour
    {
        private Character _character;

        public Gender Gender => _character.Gender;

        private void Awake()
        {
            _character = GetComponent<Character>();
        }
    }
}