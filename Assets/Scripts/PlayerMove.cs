using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    //[SerializeField] private BoxCollider2D boxCollider;
    //[SerializeField] private LayerMask groundLayer;

    [SerializeField] private float speed;
    [SerializeField] private InputAction playerController;
    private Vector2 moveControl = Vector2.zero;
    [SerializeField] private GameObject dialogueManager;

    public bool isGrounded { get; private set; }
    private float velocity = 0;
    [SerializeField] private float gravitationOffset = 30f;

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
        //boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //CheckGrounded();
        if (dialogueManager.GetComponent<DialogueManager>().isPlaying) {
            return;
        }

        moveControl = playerController.ReadValue<Vector2>();
        if (!isGrounded)
        {
            if (velocity <= -60)
            {
                velocity = 0;
            }
            else
            {
                velocity -= (gravitationOffset + 70);
            }

            rb.velocity = new Vector2(0, velocity * Time.deltaTime);
        }
        else {
            rb.velocity = new Vector2(moveControl.x * speed, 0);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Contains("Ground"))
        {
            isGrounded = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Contains("Ground"))
        {
            isGrounded = false;
        }
    }

    //private bool CheckGrounded()
    //{
    //    float extra = 0.1f;
    //    RaycastHit2D raycast = Physics2D.Raycast(boxCollider.bounds.center, Vector2.down, boxCollider.bounds.extents.y + extra, groundLayer);
    //    Color rayColor;
    //    if(raycast.collider != null)
    //    {
    //        rayColor = Color.green;
    //    }
    //    else
    //    {
    //        rayColor = Color.blue;
    //    }

    //    Debug.DrawRay(boxCollider.bounds.center, Vector2.down * (boxCollider.bounds.extents.y + extra), rayColor);
    //    return raycast.collider != null;
    //}
}
