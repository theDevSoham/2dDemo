using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerJump : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float _playerJumpSpeed = 100f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void OnJump()
    {
        if (!GetComponent<PlayerMove>().isGrounded)
        {
            return;
        }

        rb.AddRelativeForce(_playerJumpSpeed * Vector3.up);
    }
}
