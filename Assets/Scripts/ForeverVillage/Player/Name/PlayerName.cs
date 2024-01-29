using System;

namespace Village
{
    public class PlayerName : StringSavedValue
    {
        private string _name;

        public event Action<string> Setuped;

        public void Setup(string name)
        {
            _name = name == null? Get() : name;
            if (_name != Get()) Save(_name);

            Setuped?.Invoke(_name);
        }

        protected override void SetKey()
        {
            Key = SaveKeyParams.Player.Name;
        }
    }
}