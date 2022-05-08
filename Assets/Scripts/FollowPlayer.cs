using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    [SerializeField] private float m_Speed = 5f;
    [SerializeField] private float yOffset = 1f;
    [SerializeField] private GameObject player;
    private Vector3 newPos = Vector3.zero;
    private Transform playerPos;

    private bool isMoving = false;


    private void Start()
    {
        playerPos = player.transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isMoving)
        {
            newPos = new Vector3(playerPos.position.x, playerPos.position.y + yOffset, -10f);
            transform.position = Vector3.Slerp(transform.position, newPos, m_Speed * Time.deltaTime);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            isMoving = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            isMoving = true;
        }
    }
}
