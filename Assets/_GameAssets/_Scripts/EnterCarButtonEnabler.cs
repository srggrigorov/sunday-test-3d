using UnityEngine;

public class EnterCarButtonEnabler : MonoBehaviour
{
    [SerializeField] private GameObject _enterCarButton;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent.TryGetComponent<PrometeoCarController>(out var carController))
        {
            _enterCarButton.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.parent.TryGetComponent<PrometeoCarController>(out var carController))
        {
            _enterCarButton.SetActive(false);
        }
    }
}