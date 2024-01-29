using UnityEngine;

namespace Village
{
    public class Gem : Item
    {
        [SerializeField] private int _value = 1;

        public int Value => _value;
    }
}