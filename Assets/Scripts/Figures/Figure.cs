using Input;
using UnityEngine;

namespace Figures
{
    public abstract class Figure : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer animal;
        
        private FigureClickHandler _figureClickHandler;
        
        public string Id { get; private set; }

        public void Setup(string id, Sprite animalSprite, Shape shape)
        {
            Id = id;
            animal.sprite = animalSprite;
            _figureClickHandler = shape.FigureClickHandler;
            _figureClickHandler.OnClick += OnClick;
        }

        private void OnClick()
        {
            _figureClickHandler.OnClick -= OnClick;
        }

        public abstract Figure Clone();
    }
}
