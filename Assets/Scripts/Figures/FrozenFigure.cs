using UnityEngine;

namespace Figures
{
    public class FrozenFigure : Figure
    {
        [SerializeField] private int destroyedFiguresCountToUnlock;
        [SerializeField] private Color frozenColor;

        private Color _normalShapeColor;
        
        private int _destroyedFiguresCount;
        
        public override void Setup(string id, Sprite animalSprite, Shape shape)
        {
            base.Setup(id, animalSprite, shape);
            
            _normalShapeColor = shape.GetComponent<SpriteRenderer>().color;
            
            if (_destroyedFiguresCount < destroyedFiguresCountToUnlock)
            {
                shape.SpriteRenderer.color = frozenColor;
                animal.gameObject.SetActive(false);
                FigureClickObserver.Instance.FigureClicked += HandleFigureClick;
            }
        }

        private void HandleFigureClick(Figure figure)
        {
            if (figure != this)
            {
                _destroyedFiguresCount++;

                if (_destroyedFiguresCount >= destroyedFiguresCountToUnlock)
                {
                    FigureClickObserver.Instance.FigureClicked -= HandleFigureClick;
                    Shape.SpriteRenderer.color = _normalShapeColor;
                    animal.gameObject.SetActive(true);
                }
            }
        }

        protected override void OnShapeClicked()
        {
            if (_destroyedFiguresCount < destroyedFiguresCountToUnlock) 
                return;
            
            base.OnShapeClicked();
        }

        public override Figure Clone()
        {
            return Instantiate(this);
        }
    }
}