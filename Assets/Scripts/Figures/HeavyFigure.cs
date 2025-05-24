using UnityEngine;

namespace Figures
{
    public class HeavyFigure : Figure
    {
        [SerializeField] private float massValue;
        
        public override void Setup(string id, Sprite animalSprite, Shape shape)
        {
            base.Setup(id, animalSprite, shape);

            Shape.GetComponent<Rigidbody2D>().mass = massValue;
        }

        public override Figure Clone()
        {
            return Instantiate(this);
        }
    }
}