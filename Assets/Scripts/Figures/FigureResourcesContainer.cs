using System.Collections.Generic;
using System.Linq;
using Figures.FigureStructure;
using UnityEngine;

namespace Figures
{
    [CreateAssetMenu(menuName = "Game/FigureResourcesContainer", fileName = "FigureResourcesContainer", order = 0)]
    public class FigureResourcesContainer : ScriptableObject
    {
        [SerializeField] private List<ShapesContainer> figureSpritesContainers;
        [SerializeField] private List<AnimalSpritesContainer> animalSpritesContainers;

        public Sprite GetAnimalSprite(AnimalType animalType)
        {
            return animalSpritesContainers.FirstOrDefault(x => x.animalType == animalType).sprite;
        }

        public bool TryGetAnimalSprite(AnimalType animalType, out Sprite animalSprite)
        {
            
            var animalSpritesContainer = animalSpritesContainers.FirstOrDefault(x => x.animalType == animalType);

            if (animalSpritesContainer == null)
            {
                animalSprite = null;
                return false;
            }
            
            animalSprite = animalSpritesContainer.sprite;
            return true;
        }

        public GameObject GetFigureShape(ShapeType shapeType)
        {
            return figureSpritesContainers.FirstOrDefault(x => x.shapeType == shapeType).shapePrefab;
        }

        public bool TryGetFigureShape(ShapeType shapeType, out GameObject shapePrefab)
        {
            var figureSpritesContainer = figureSpritesContainers.FirstOrDefault(x => x.shapeType == shapeType);

            if (figureSpritesContainer == null)
            {
                shapePrefab = null;
                return false;
            }
            
            shapePrefab = figureSpritesContainer.shapePrefab;
            return true;
        }
    }
}