using UnityEngine;

namespace Figures
{
    [CreateAssetMenu(fileName = "FiguresConfig", menuName = "Game/Configs/FiguresConfig", order = 0)]
    public class FiguresViewConfig : ScriptableObject
    {
        [SerializeField] private FigureData[] figures;
        
        public FigureData[] Figures => figures;
    }
}