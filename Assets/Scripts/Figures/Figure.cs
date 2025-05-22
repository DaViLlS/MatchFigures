using UnityEngine;

namespace Figures
{
    public class Figure : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer figureShape;
        [SerializeField] private SpriteRenderer animal;
        [SerializeField] private SpriteRenderer frame;
        
        public string Id { get; private set; }

        public void Setup(string id, Sprite shapeSprite, Sprite animalSprite, Color frameColor)
        {
            Id = id;
            figureShape.sprite = shapeSprite;
            animal.sprite = animalSprite;
            frame.color = frameColor;
        }
    }
}
