using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Player Interaction")]
    public GameObject[] playerInteraction;
    //0th position has visualCue
    //1st position has InteractButton

    [Header("Dialogue Manager")]
    public GameObject dialogueManager;

    [Header("Text file")]
    [SerializeField] private TextAsset inkJson;

    [SerializeField] private bool playerInRange = false;

    [SerializeField] private InputAction _startCue;

    private void Awake()
    {
        playerInteraction[0].SetActive(false);
        //playerInteraction[1].SetActive(false);
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
        if (playerInRange && !dialogueManager.GetComponent<DialogueManager>().isPlaying)
        {
            playerInteraction[0].SetActive(true);
            //playerInteraction[1].SetActive(true);
            _startCue.Enable();
            _startCue.performed += context =>
            {
                if (context.performed)
                {
                    dialogueManager.GetComponent<DialogueManager>().EnterDialogueMode(inkJson);
                }
            };
        }
        else if (!playerInRange || dialogueManager.GetComponent<DialogueManager>().isPlaying)
        {
            playerInteraction[0].SetActive(false);
            //playerInteraction[1].SetActive(false);
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
