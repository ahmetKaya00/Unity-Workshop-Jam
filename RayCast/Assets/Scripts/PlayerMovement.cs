using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 4f, _Xspeed = 6f, _gravity = -9.8f, _jump = 1f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.3f;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Joystick movementJoystick;
    [SerializeField] private Button jumpButton;
    private Vector3 _velocity;
    private bool isGrounded;
    private CharacterController characterController;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        jumpButton.onClick.AddListener(Jump);
    }
    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }

        float x = movementJoystick.Horizontal;
        float z = movementJoystick.Vertical;

        Vector3 move = transform.right * x + transform.forward * z;

        if(z > 0.8f || Input.GetKey(KeyCode.LeftShift))
        {
            characterController.Move(move * _Xspeed * Time.deltaTime);
        }
        else
        {
            characterController.Move(move * _speed * Time.deltaTime);
        }

        _velocity.y += _gravity * Time.deltaTime;
        characterController.Move(_velocity * Time.deltaTime);
    }
    private void Jump()
    {
        if (isGrounded)
        {
            _velocity.y = Mathf.Sqrt(_jump * -2f * _gravity);
        }
    }
}
