using UnityEngine;

public class ShootAimProjector : MonoBehaviour
{
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private Transform _aimTarget;
    [SerializeField] private LayerMask _collisionLayerMask;

    private void Start()
    {
        _cameraTransform ??= Camera.current.transform;
    }

    private void Update()
    {
        if (_aimTarget == null)
        {
            return;
        }

        Ray aimRay = new Ray(_cameraTransform.position, _cameraTransform.forward);
        if (Physics.Raycast(aimRay, out var hitInfo, Mathf.Infinity, _collisionLayerMask))
        {
            _aimTarget.position = hitInfo.point;
        }
        else
        {
            _aimTarget.position = _cameraTransform.position + _cameraTransform.forward * 1000;
        }
    }
}