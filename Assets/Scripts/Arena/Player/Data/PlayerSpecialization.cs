using IJunior.TypedScenes;
using UnityEngine;

namespace Village
{
    public class PlayerSpecialization : MonoBehaviour,
        ISceneLoadHandler<ICharacterSpecialization>
    {
        private SpecializationConfig _data;
        private string _prefabPath;

        public bool IsSettuped { get; private set; } // for testing

        public void OnSceneLoaded(ICharacterSpecialization argument)
        {
            if (argument != null)
            {
                _data = argument.Data;
                _prefabPath = argument.GetModelPath();

                IsSettuped = true;
            }
        }
    }
}