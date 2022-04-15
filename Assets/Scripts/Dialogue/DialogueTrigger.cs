using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    [SerializeField] private bool playerInRange = false;

    private void Awake()
    {
        visualCue.SetActive(false);
        playerInRange = false;
    }
    private void Update()
    {
        if (playerInRange)
        {
            visualCue.SetActive(true);
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
