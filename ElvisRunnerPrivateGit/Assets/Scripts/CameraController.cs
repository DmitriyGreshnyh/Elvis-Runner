
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _targetObject;
    private Vector3 _offset;
    [SerializeField] float _cameraSpeed = 10;

    void Start()
    {
        _offset = transform.position - _targetObject.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 newPosition = new Vector3(_targetObject.position.x, _targetObject.position.y + _offset.y, _offset.z);

        transform.position = Vector3.Lerp(transform.position, newPosition, _cameraSpeed * Time.deltaTime);
    }
}
