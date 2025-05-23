using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Figures
{
    [CreateAssetMenu(menuName = "Game/FigureResourcesContainer", fileName = "FigureResourcesContainer", order = 0)]
    public class FigureResourcesContainer : ScriptableObject
    {
        [SerializeField] private List<FigureSpritesContainer> figureSpritesContainers;
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

        public Sprite GetFigureSprite(ShapeType shapeType)
        {
            return figureSpritesContainers.FirstOrDefault(x => x.shapeType == shapeType).sprite;
        }

        public bool TryGetFigureSprite(ShapeType shapeType, out Sprite figureSprite)
        {
            var figureSpritesContainer = figureSpritesContainers.FirstOrDefault(x => x.shapeType == shapeType);

            if (figureSpritesContainer == null)
            {
                figureSprite = null;
                return false;
            }
            
            figureSprite = figureSpritesContainer.sprite;
            return true;
        }
    }
}