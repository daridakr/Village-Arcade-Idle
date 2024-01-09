using System.Collections.Generic;
using UnityEngine;

namespace ForeverVillage.Scripts
{
    // General single data specific to a particular type of building.
    [CreateAssetMenu(fileName = "BuildingType", menuName = "Buildings/Building Type", order = 51)]
    public class BuildingTypeData : ScriptableObject
    {
        [SerializeField] private string _description;
        [SerializeField] private Sprite[] _statsIcons;

        public string Description => _description;
        public IEnumerable<Sprite> StatIcons => _statsIcons;
    }
}