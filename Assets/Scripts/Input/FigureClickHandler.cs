using System;
using UnityEngine;

namespace Input
{
    public class FigureClickHandler : MonoBehaviour
    {
        public event Action OnClick;
        
        private void OnMouseUpAsButton()
        {
            OnClick?.Invoke();
            Destroy(gameObject);
        }
    }
}