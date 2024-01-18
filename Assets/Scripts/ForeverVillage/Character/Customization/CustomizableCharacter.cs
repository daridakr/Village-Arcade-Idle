using UnityEngine;

namespace ForeverVillage.Scripts.Character
{
    [RequireComponent(typeof(Character))]
    public class CustomizableCharacter : MonoBehaviour
    {
        private Character _character;

        private void Awake()
        {
            _character = GetComponent<Character>();
        }
    }
}