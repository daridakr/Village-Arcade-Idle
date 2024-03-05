using UnityEngine;

namespace ForeverVillage
{
    public class ItemConfig : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private string _description;
        [SerializeField] private Sprite _icon;
    }
}