using UnityEngine;

namespace ForeverVillage.Scripts
{
    public class CharacterModel : MonoBehaviour
    {
        private string _modelPath;

        public void SetupPrefabPath(string path)
        {
            _modelPath = path;
        }

        public void InstantiateModel()
        {

        }
    }
}