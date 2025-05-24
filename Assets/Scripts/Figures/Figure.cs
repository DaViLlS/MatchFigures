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
        [SerializeField] protected SpriteRenderer animal;
        
        protected Shape Shape;
        
        public string Id { get; private set; }
        public FigureUiView FigureUiView { get; private set; }

        public virtual void Setup(string id, Sprite animalSprite, Shape shape)
        {
            Id = id;
            animal.sprite = animalSprite;
            Shape = shape;
            
            var shapeSpriteRenderer = shape.SpriteRenderer;
            FigureUiView = Instantiate(figureUIPrefab, GameCanvas.Instance.Canvas.transform);
            FigureUiView.Setup(shapeSpriteRenderer.sprite, animalSprite, shapeSpriteRenderer.color);
            FigureUiView.gameObject.SetActive(false);

            Shape.OnClick += OnShapeClicked;
        }

        protected virtual void OnShapeClicked()
        {
            OnClick?.Invoke(this);
            DestroyFigure();
        }

        public void DestroyFigure()
        {
            Destroy(Shape.gameObject);
        }

        public abstract Figure Clone();
    }
}
