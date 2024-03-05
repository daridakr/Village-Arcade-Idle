using ForeverVillage;
using UnityEngine;

namespace Arena
{
    public sealed class Weapon : Collectable, IEquippable
    {
        public void Equip(Transform parentRig)
        {

        }
    }

    public interface IEquippable
    {
        public void Equip(Transform parentRig);
    }

    public abstract class Equipment : IEquippable
    {
        public void Equip(Transform parentRig)
        {

        }
    }
}