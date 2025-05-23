using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Figures.Ui
{
    public class FigureUiView : MonoBehaviour
    {
        [SerializeField] private Image figureImage;
        [SerializeField] private Image animalImage;
        
        private Tween _tween;

        public void Setup(Sprite figure, Sprite animal, Color figureColor)
        {
            figureImage.sprite = figure;
            figureImage.color = figureColor;
            animalImage.sprite = animal;
        }

        public Tween MoveTo(Vector2 position)
        {
            _tween = transform.DOMove(position, 0.5f);
            return _tween;
        }

        private void OnDestroy()
        {
            _tween?.Kill();
        }
    }
}