using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContolPlayer : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    private Vector2 move = new Vector2(0, 0);
    [SerializeField] float speed = 5f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void Jump()
    {
        {
            move.y += speed;
            rb.AddRelativeForce(move);
            Debug.Log("Jumped");
        }
    }
}
