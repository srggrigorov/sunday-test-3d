using UnityEngine;

public class FootTraceCreator : MonoBehaviour
{
    [SerializeField] private bool _isLeftFoot;
    [SerializeField] private GameObject _tracePrefab;
    [SerializeField] private LayerMask _collisionLayerMask;

    private void OnTriggerEnter(Collider other)
    {
        if (_collisionLayerMask != (_collisionLayerMask | 1 << other.gameObject.layer))
        {
            return;
        }

        Vector3 footRotationEuler = transform.rotation.eulerAngles;
        footRotationEuler.Set(_isLeftFoot ? 180 : 0, footRotationEuler.y, 0);

        var trace = Instantiate(_tracePrefab, other.ClosestPointOnBounds(transform.position) + Vector3.up * 0.01f,
            Quaternion.Euler(footRotationEuler));
        Destroy(trace, 3);
    }
}