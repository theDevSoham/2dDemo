using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    [Header("Text file")]
    [SerializeField] private TextAsset inkJson;

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
        TriggerDialogue();
    }

    private void TriggerDialogue()
    {
        if (playerInRange)
        {
            visualCue.SetActive(true);
            _startCue.Enable();
            _startCue.performed += context =>
            {
                if (context.performed)
                {
                    Debug.Log(inkJson.text);
                }
            };
        }
        else if (!playerInRange)
        {
            visualCue.SetActive(false);
            _startCue.Disable();
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
