using System;
using UnityEngine;
using Zenject;

public class VillageInfo : StringSavedValue
{
    [SerializeField] private StartGame _start;

    private string _name;
    private readonly PlayerLevel _playerLevel;

    public int Level => _playerLevel.Level;

    public event Action<string> Named;

    private void Awake()
    {
        if (_start.IsNewGame)
        {
            _start.VillageNamed += OnVillageNamed;
        }
        else
        {
            _name = Get();
            Named?.Invoke(_name);
        }
    }

    private void OnVillageNamed(string name)
    {
        _start.VillageNamed -= OnVillageNamed;

        _name = name;
        Save(_name);

        Named?.Invoke(name);
    }

    //[Inject]
    //public void Construct(PlayerLevel playerLevel)
    //{
    //    _playerLevel = playerLevel;
    //}

    protected override void SetKey()
    {
        Key = SaveKeyParams.Player.VillageName;
    }
}
