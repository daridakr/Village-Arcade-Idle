using UnityEngine;

namespace ForeverVillage.Scripts.Character
{
    public sealed class CharacterCreator
    {
        private Transform _point;

        public CharacterCreator(Transform point) => _point = point;

        public Character Create(Character prefab) => Object.Instantiate(prefab, _point.position, Quaternion.Euler(_point.rotation.eulerAngles), _point);
    }
}