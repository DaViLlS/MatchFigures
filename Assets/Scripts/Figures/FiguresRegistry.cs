using UnityEngine;

namespace Figures
{
    [CreateAssetMenu(fileName = "Figures Registry", menuName = "Game/Configs/Figures Registry")]
    public class FiguresRegistry : ScriptableObject
    {
        [SerializeField] private Figure[] figuresPrefabs;

        public Figure GetRandomFigure()
        {
            return figuresPrefabs[Random.Range(0, figuresPrefabs.Length)];
        }
    }
}