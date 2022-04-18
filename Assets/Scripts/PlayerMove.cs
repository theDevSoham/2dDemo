using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private InputAction playerController;
    private Vector2 moveControl = Vector2.zero;
    [SerializeField] private GameObject dialogueManager;

    private void OnEnable()
    {
        playerController.Enable();
    }
    private void OnDisable()
    {
        playerController.Disable();
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (dialogueManager.GetComponent<DialogueManager>().isPlaying) {
            return;
        }

        moveControl = playerController.ReadValue<Vector2>();
        rb.velocity = new Vector2(moveControl.x * speed, 0);
    }
}
