using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    [SerializeField] private bool playerInRange = false;

    [SerializeField] private InputAction _startCue;

    private void Awake()
    {
        visualCue.SetActive(false);
        playerInRange = false;
    }
    private void OnEnable()
    {
        _startCue.Enable();
    }
    private void OnDisable()
    {
        _startCue.Disable();
    }
    private void Update()
    {
        if (playerInRange)
        {
            visualCue.SetActive(true);
            _startCue.performed += context =>
            {
                if (context.performed) {
                    Debug.Log("Show Dialogue");
                }
            };
        }
        else if(!playerInRange){ 
            visualCue.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") { 
            playerInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInRange = false;
        }
    }
}
