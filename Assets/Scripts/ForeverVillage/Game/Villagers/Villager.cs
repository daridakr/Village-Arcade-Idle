using UnityEngine;

namespace ForeverVillage.Scripts
{
    [CreateAssetMenu(fileName = "NewVillager", menuName = "Villagers/Villager", order = 51)]
    public class Villager : ScriptableObject, IStorableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private string _description;
        [SerializeField] private int _price;
        [SerializeField] private Sprite _icon;

        public string Name => _name;
        public string Description => _description;
        public int Price => _price;
        public Sprite Icon => _icon;
    }
}