using UnityEngine;

namespace UI
{
    public class GameCanvas : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        
        public Canvas Canvas => _canvas;
        
        public static GameCanvas Instance;

        private void Awake()
        {
            Instance = this;
        }
    }
}