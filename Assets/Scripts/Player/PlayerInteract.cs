using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private Camera _playerCam;
    [SerializeField] private LayerMask _interactMask;
    [SerializeField] private float _interactDistance = 1.5f;
    [SerializeField] private GameInput _gameInput;
    [SerializeField] private Transform _holdPoint;
    [SerializeField] private Transform _pistolPoint;
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private PlayerMove _player;
    [SerializeField] private PlayerShoot _shootLogic;

    private GameObject _heldObject;
    private GameObject _lookInteractable;
    private GameObject _previousInteractable;

    private void Start()
    {
        _gameInput.OnInteractPress += GameInput_OnInteractPress;
        _gameInput.OnShoot += _gameInput_OnShoot;
    }

    private void Update()
    {
        HandleInteractOutline();
    }

    private void HandleInteractOutline()
    {
        _previousInteractable = _lookInteractable;
        _lookInteractable = null;

        if (Physics.Raycast(_playerCam.transform.position, _playerCam.transform.forward, out RaycastHit hit, _interactDistance, _interactMask))
        {
            _lookInteractable = hit.collider.gameObject;
        }

        if (_previousInteractable != _lookInteractable)
        {
            if (_previousInteractable != null && _previousInteractable.TryGetComponent<IInteract>(out IInteract previousInteract))
            {
                previousInteract.HideOutline();
            }

            if (_lookInteractable != null && _lookInteractable.TryGetComponent<IInteract>(out IInteract currentInteract))
            {
                currentInteract.ShowOutline();
            }
        }
    }

    private void _gameInput_OnShoot()
    {
        if (_heldObject != null && _heldObject.TryGetComponent<PistolLogic>(out PistolLogic logic))
        {
            _shootLogic.Shoot(_audioManager, _playerCam);
        }
    }

    private void GameInput_OnInteractPress()
    {
        TryInteract();
    }

    private void TryInteract()
    {
        if (Physics.Raycast(_playerCam.transform.position, _playerCam.transform.forward, out RaycastHit hit, _interactDistance, _interactMask))
        {
            var interactable = hit.collider.GetComponentInParent<IInteract>();
            if (interactable != null)
            {
                interactable.Interact(this);
            }
        }
    }

    public void HoldObject(GameObject newObject)
    {
        _heldObject = newObject;

        if (_heldObject.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            rb.isKinematic = true;
            rb.detectCollisions = false;
        }

        if (_heldObject.TryGetComponent<PistolLogic>(out PistolLogic logic))
        {
            _heldObject.transform.SetParent(_pistolPoint);
            _shootLogic.ActivateGunUI();
        }
        else
        {
            _heldObject.transform.SetParent(_holdPoint);
        }

        if (_player.GetCrouched())
        {
            _player.SetStanding();
        }

        _heldObject.transform.localPosition = Vector3.zero;
        _heldObject.transform.localRotation = Quaternion.Euler(180, 0, 0);

        _audioManager.PlaySound(AudioManager.SoundType.Pickup);
    }

    public GameObject GetHeldItem()
    {
        return _heldObject;
    }
}