using UnityEngine;
using UnityEngine.UI;

namespace Figures.Ui
{
    public class FigureUiView : MonoBehaviour
    {
        [SerializeField] private Image figureImage;
        [SerializeField] private Image animalImage;

        public void Setup(Sprite figure, Sprite animal, Color figureColor)
        {
            figureImage.sprite = figure;
            figureImage.color = figureColor;
            animalImage.sprite = animal;
        }
    }
}