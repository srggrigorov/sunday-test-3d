using System;
using UnityEngine;

public class BulletHead : MonoBehaviour
{
    [SerializeField] private float _damageAmount;
    [SerializeField] private float _destroyTime = 5;

    private void Start()
    {
        Destroy(gameObject, _destroyTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out IDamagaeble iDamagaeble))
        {
            iDamagaeble.GetDamage(_damageAmount);
        }

        Destroy(gameObject);
    }
}