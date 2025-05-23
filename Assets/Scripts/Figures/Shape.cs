using Input;
using UnityEngine;

namespace Figures
{
    public class Shape : MonoBehaviour
    {
        [SerializeField] private FigureClickHandler figureClickHandler;
        [SerializeField] private SpriteRenderer spriteRenderer;
        
        public FigureClickHandler FigureClickHandler => figureClickHandler;
        public SpriteRenderer SpriteRenderer => spriteRenderer;
    }
}