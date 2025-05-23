namespace Figures
{
    public class SimpleFigure : Figure
    {
        public override Figure Clone()
        {
            return Instantiate(this);
        }
    }
}