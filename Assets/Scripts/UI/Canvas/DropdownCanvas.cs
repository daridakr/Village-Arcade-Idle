public class DropdownCanvas : CanvasAnimatedView
{
    private bool _opened = false;

    public override void Display()
    {
        if (_opened)
            base.Hide();
        else
            base.Display();

        _opened = !_opened;
    }
}
