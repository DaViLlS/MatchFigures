using Figures.Ui;
using Input;
using UI;
using UnityEngine;

namespace Figures
{
    public abstract class Figure : MonoBehaviour
    {
        [SerializeField] private FigureUiView figureUIPrefab;
        [SerializeField] private SpriteRenderer animal;
        
        private FigureClickHandler _figureClickHandler;
        private FigureUiView _figureUiView;
        
        public string Id { get; private set; }

        public void Setup(string id, Sprite animalSprite, Shape shape)
        {
            Id = id;
            animal.sprite = animalSprite;
            _figureClickHandler = shape.FigureClickHandler;
            
            _figureUiView = Instantiate(figureUIPrefab, GameCanvas.Instance.Canvas.transform);
            _figureUiView.Setup(shape.SpriteRenderer.sprite, animalSprite, shape.SpriteRenderer.color);
            _figureUiView.gameObject.SetActive(false);
            
            _figureClickHandler.OnClick += OnClick;
        }

        private void OnClick()
        {
            _figureUiView.gameObject.SetActive(true);
            var screenPosition = Camera.main.WorldToScreenPoint(transform.position);
            _figureUiView.transform.position = screenPosition;
            
            _figureClickHandler.OnClick -= OnClick;
        }

        public abstract Figure Clone();
    }
}
