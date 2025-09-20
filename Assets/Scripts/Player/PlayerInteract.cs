using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private Camera _playerCam;
    [SerializeField] private LayerMask _interactMask;
    [SerializeField] private float _interactDistance = 1.5f;
    [SerializeField] private GameInput _gameInput;
    [SerializeField] private Transform _holdPoint;
    [SerializeField] private AudioManager _audioManager;

    private GameObject _heldObject;

    private void Start()
    {
        _gameInput.OnInteractPress += GameInput_OnInteractPress;
    }

    private void GameInput_OnInteractPress()
    {
        TryInteract();
    }

    private void TryInteract()
    {
        if (Physics.Raycast(_playerCam.transform.position, _playerCam.transform.forward, out RaycastHit hit, _interactDistance, _interactMask))
        {
            var interactable = hit.collider.GetComponent<IInteract>();
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

        _heldObject.transform.SetParent(_holdPoint);
        _heldObject.transform.localPosition = Vector3.zero;
        _heldObject.transform.localRotation = Quaternion.identity;

        _audioManager.PlaySound(AudioManager.SoundType.Pickup);
    }

    public GameObject GetHeldItem()
    {
        return _heldObject;
    }
}