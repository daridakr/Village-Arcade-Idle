using UnityEngine;

public class BuyRegionCanvas : CanvasAnimatedView
{
    [SerializeField] private TextDisplay _priceView;

    public void Display(int price)
    {
        base.Display();
        _priceView.Display(price.ToString());
    }
}
