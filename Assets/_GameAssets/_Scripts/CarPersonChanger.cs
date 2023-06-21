using UnityEngine;
using UnityEngine.UI;

public class CarPersonChanger : MonoBehaviour
{
    [SerializeField] private Button _changeButton;
    [SerializeField] private GameObject _characterCanvas;
    [SerializeField] private GameObject _driveCanvas;
    [SerializeField] private GameObject _playerCharacter;
    [SerializeField] private Transform _carTransform;

    private void Awake()
    {
        _changeButton.onClick.AddListener(ChangeGameplay);
    }

    private void ChangeGameplay()
    {
        _characterCanvas.SetActive(!_playerCharacter.activeSelf);
        _driveCanvas.SetActive(_playerCharacter.activeSelf);
        _playerCharacter.transform.position = _carTransform.position - _carTransform.right * 1.5f;
        _playerCharacter.SetActive(!_playerCharacter.activeSelf);
    }
}