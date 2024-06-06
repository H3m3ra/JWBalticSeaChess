using System.ComponentModel;

public class CanvasPanel : Panel
{
    [DefaultValue(true)]
    public new bool DoubleBuffered
    {
        get
        {
            return base.DoubleBuffered;
        }
        set
        {
            base.DoubleBuffered = value;
        }
    }
}