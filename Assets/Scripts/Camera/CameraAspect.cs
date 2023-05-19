using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class CameraAspect : MonoBehaviour
{
    private Camera _camera;
    private float _lastSize;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
        _lastSize = _camera.orthographicSize;
    }

    private void OnEnable()
    {
        _camera.orthographicSize = _lastSize = 11.37f;
        EditorApplication.update += OnEditorUpdate;
    }

    private void OnDisable()
    {
        EditorApplication.update -= OnEditorUpdate;
    }

    private void OnEditorUpdate()
    {
        if (_camera != null && _camera.orthographicSize != _lastSize)
        {
            _camera.orthographicSize = _lastSize = 11.37f;
        }
    }
}
