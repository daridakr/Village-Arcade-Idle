using UnityEngine;

namespace Village
{
    [CreateAssetMenu(fileName = "NewVillager", menuName = "Villagers/Villager", order = 51)]
    public class Villager : ScriptableObject, IStorableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private string _description;
        [SerializeField] private int _price;
        [SerializeField] private Sprite _icon;
        [SerializeField] private float _multiplicator = 1f;

        public string Name => _name;
        public string Description => _description;
        public int Price => _price;
        public Sprite Icon => _icon;
        public float Multiplicator => _multiplicator;
    }
}