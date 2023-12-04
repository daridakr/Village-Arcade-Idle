using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class RegionZone : MonoBehaviour
{
    [SerializeField] private Region _region;
    [SerializeField] private MoneyOwnerTrigger _moneyOwnerTrigger;

    private int _price;

    private void OnEnable()
    {
        _region.Unlocked += OnUnlockedRegion;
    }

    private void OnUnlockedRegion(int price)
    {
        _region.Unlocked -= OnUnlockedRegion;

        _moneyOwnerTrigger.Enter += TriggerEnter;
        _moneyOwnerTrigger.Exit += TriggerExit;
        _price = price;

        _region.Buyed += OnBuyedRegion;
    }

    private void OnBuyedRegion()
    {
        Destroy(this);
    }

    private void TriggerEnter(MoneyOwner moneyOwner)
    {
        //ShowRewardPlacement?.Invoke(_placement);

        //switch (_currentState)
        //{
        //    //case BuildingZoneState.Destroyed:
        //    //    _view.ShowClearCanvas(_clearPrice, moneyOwner.Balance);
        //    //    break;
        //    //case BuildingZoneState.Empty:
        //    //    _view.ShowBuildCanvas();
        //    //    break;
        //    //case BuildingZoneState.Builded:
        //    //    //_view.ShowUpgradeCanvas();
        //    //    break;
        //    //default:
        //    //    break;
        //}

        //UpdateView();
    }

    private void TriggerExit(MoneyOwner moneyOwner)
    {

    }

    private void OnDestroy()
    {
        _moneyOwnerTrigger.Enter -= TriggerEnter;
        _moneyOwnerTrigger.Exit -= TriggerExit;

        _region.Buyed -= OnBuyedRegion;
    }
}
