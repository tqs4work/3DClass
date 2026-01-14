using Mono.Cecil;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public CharacterController characterController;
    public PlayerInput playerInput;
    private Vector3 _movementVelocity;
    private float _verticalVelocity;
    private float gravity = -9.81f;

    public Animator playerAnimator;
    public int speedHash;
    public int comboHash;
    //public int attackHash;
    //public int dieHash;
    public GameObject playerModel;
    void Start()
    {
        speedHash = Constants.SpeedHash;
        comboHash = Constants.ComboHash; // thêm dòng này
    }

    void FixedUpdate()
    {
        CalculateMovement();
        if (characterController.isGrounded == false)
        {
            _verticalVelocity = gravity;
        }
        else
        {
            _verticalVelocity = gravity * 0.1f;
        }
        _movementVelocity += Vector3.up * _verticalVelocity * Time.deltaTime;
        characterController.Move(_movementVelocity);
        playerAnimator.SetFloat(speedHash, _movementVelocity.magnitude);


    }

    void CalculateMovement()
    {
        _movementVelocity.Set(playerInput.horizontalInput, 0, playerInput.verticalInput);
        _movementVelocity.Normalize();
        _movementVelocity = Quaternion.Euler(0, -45f, 0) * _movementVelocity;
        _movementVelocity *= moveSpeed * Time.deltaTime;
        if (_movementVelocity != Vector3.zero)
        {
            //transform.rotation = Quaternion.LookRotation(_movementVelocity);
            playerModel.transform.rotation = Quaternion.LookRotation(_movementVelocity);
        }
    }
}
