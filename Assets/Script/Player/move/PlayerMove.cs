using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float Speed
    {
        get { return _movementSpeed; }
        set
        {
            if (value < 0)  value = 1; 
           
            _movementSpeed = value;
        }
    }

    [SerializeField] private float _movementSpeed = 3f;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private float _turnSpeed = 0.1f;
    private Rigidbody _playerRigidbody;
    private Vector3 _movementDirection;

    private void FixedUpdate()
    {
        MoveThePlayer();
        TurnThePlayer();
    }

    private void MoveThePlayer()
    {
        Vector3 movement = CameraDirection(_movementDirection) * _movementSpeed * Time.deltaTime;
        _playerRigidbody.MovePosition(transform.position + movement);
    }

    private void TurnThePlayer()
    {
        if (_movementDirection.sqrMagnitude > 0.01f)
        {
            Quaternion rotation = Quaternion.Slerp(_playerRigidbody.rotation,
                                                 Quaternion.LookRotation(CameraDirection(_movementDirection)),
                                                 _turnSpeed);
            _playerRigidbody.MoveRotation(rotation);
        }
    }

    public void UpdateMovementData(Vector3 newMovementDirection)
    {
        _movementDirection = newMovementDirection;
    }

    public void SetupPlayerMove()
    {
        _playerRigidbody = GetComponent<Rigidbody>();
    }

    public Vector3 CameraDirection(Vector3 movementDirection)
    {
        var cameraForward = _mainCamera.transform.forward;
        var cameraRight = _mainCamera.transform.right;
        cameraForward.y = 0f;
        cameraRight.y = 0f;
        return cameraForward * movementDirection.z + cameraRight * movementDirection.x;
    }
}
