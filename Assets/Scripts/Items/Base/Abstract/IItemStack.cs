using System;

namespace ForeverVillage
{
    public interface IItemStack
    {
        public IItem Item { get; }
        public int Count { get; }
        public void Add(int count = 1);
        public void Expend();
        public event Action Expended;
    }
}