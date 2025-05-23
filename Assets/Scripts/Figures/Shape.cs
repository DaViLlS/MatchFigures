using Input;
using UnityEngine;

namespace Figures
{
    public class Shape : MonoBehaviour
    {
        [SerializeField] private FigureClickHandler figureClickHandler;
        
        public FigureClickHandler FigureClickHandler => figureClickHandler;
    }
}