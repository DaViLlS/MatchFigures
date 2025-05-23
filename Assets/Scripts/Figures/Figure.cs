using System;
using Figures.Ui;
using UI;
using UnityEngine;

namespace Figures
{
    public abstract class Figure : MonoBehaviour
    {
        public event Action<Figure> OnClick;
        
        [SerializeField] private FigureUiView figureUIPrefab;
        [SerializeField] private SpriteRenderer animal;
        
        private Shape _shape;
        
        public string Id { get; private set; }
        public FigureUiView FigureUiView { get; private set; }

        public void Setup(string id, Sprite animalSprite, Shape shape)
        {
            Id = id;
            animal.sprite = animalSprite;
            _shape = shape;
            
            var shapeSpriteRenderer = shape.SpriteRenderer;
            FigureUiView = Instantiate(figureUIPrefab, GameCanvas.Instance.Canvas.transform);
            FigureUiView.Setup(shapeSpriteRenderer.sprite, animalSprite, shapeSpriteRenderer.color);
            FigureUiView.gameObject.SetActive(false);

            _shape.OnClick += OnShapeClicked;
        }

        private void OnShapeClicked()
        {
            OnClick?.Invoke(this);
        }

        public abstract Figure Clone();
    }
}
