using UnityEngine;

public interface IGameSaveDataListener
{
    void OnSaveData(GameSaveReason reason);
}