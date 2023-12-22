using TMPro;
using UnityEngine;

public class PriceDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _price;

    private int _priceValue;

    private void Start()
    {
        if (_priceValue <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void SetPrice(int price)
    {
        if (price > 0)
        {
            _priceValue = price;
            Display();
        }
    }

    private void Display()
    {
        _price.text = _priceValue.ToString();
        gameObject.SetActive(true);
    }
}
