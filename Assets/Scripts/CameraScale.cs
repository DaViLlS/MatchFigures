using System;
using UnityEngine;

public class CameraScale : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _scaleCoefficient;

    private void Start()
    {
        _camera.orthographicSize = Convert.ToSingle(Screen.height) / Screen.width * _scaleCoefficient;
    }
}
