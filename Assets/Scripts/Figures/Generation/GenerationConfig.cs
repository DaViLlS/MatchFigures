using UnityEngine;

namespace Figures.Generation
{
    [CreateAssetMenu(fileName = "Generation Config", menuName = "Game/Configs/Generation Config")]
    public class GenerationConfig : ScriptableObject
    {
        [SerializeField] private int figuresCount;

        public int FiguresCount => figuresCount;
    }
}