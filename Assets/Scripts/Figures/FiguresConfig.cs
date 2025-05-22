using UnityEngine;

namespace Figures
{
    [CreateAssetMenu(fileName = "FiguresConfig", menuName = "Game/Configs/FiguresConfig", order = 0)]
    public class FiguresConfig : ScriptableObject
    {
        [SerializeField] private FigureData[] figures;
    }
}