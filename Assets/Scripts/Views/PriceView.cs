using TMPro;
using UnityEngine;

public class PriceView : MonoBehaviour
{
    [SerializeField] private TMP_Text _priceDisplay;

    private int _price;

    public void Init(int price)
    {
        _price = price;
        _priceDisplay.text = _price.ToString();
    }
}
