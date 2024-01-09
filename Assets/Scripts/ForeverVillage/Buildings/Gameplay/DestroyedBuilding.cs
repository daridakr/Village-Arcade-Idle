using UnityEngine;

namespace ForeverVillage.Scripts
{
    public class DestroyedBuilding : MonoBehaviour
    {
        public void Clear()
        {
            Destroy(gameObject);
        }
    }
}