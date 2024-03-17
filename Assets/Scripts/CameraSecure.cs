using UnityEngine;

public class CameraSecure : MonoBehaviour
{
    private Camera _camera;

    [SerializeField] private Transform _character;
    [SerializeField] private Vector3 _offset;

    private void Awake()
    {
        _camera = Camera.main;
    }

    void Update()
    {
        if( _character != null )
        {
            _camera.transform.position = _character.position + _offset;
        }
    }
}
