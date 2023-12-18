using SweetSugar.Scripts.Items;
using UnityEngine;

namespace SweetSugar.Scripts
{
    public class ItemAnimEvents : MonoBehaviour {


        public Items.Item item;

        public void SetAnimationDestroyingFinished()
        {
            item.SetAnimationDestroyingFinished();
        }
    }
}
