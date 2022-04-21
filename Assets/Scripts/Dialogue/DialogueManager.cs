using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.InputSystem;
using System;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    private Coroutine cor;

    [Header("Dialogue Panel")]
    [SerializeField] private GameObject dialoguePanel;

    [Header("Text Mesh")]
    [SerializeField] private TextMeshProUGUI textMesh;

    [Header("Ink File")]
    [SerializeField] private Story currentStory;

    [SerializeField] private InputAction _startAction;

    public bool isPlaying { get; private set; }


    private void Awake()
    {
        if (instance != null) {
            Debug.LogWarning("More than one singleton class found");
        }
        instance = this;
    }

    private void OnEnable()
    {
        _startAction.Enable();
    }

    private void OnDisable()
    {
        _startAction.Disable();
    }


    public static DialogueManager GetInstance() { 
        return instance;
    }

    private void Start()
    {
        isPlaying = false;
        dialoguePanel.SetActive(false);
    }

    private void Update()
    {
        if (!isPlaying)
        {
            //Debug.Log("Out");
            return;
        }

        else
        {
            _startAction.performed += context =>
            {
                if (context.performed)
                {
                    ContinueStory();
                }
            };
        }
    }

    

    public void EnterDialogueMode(TextAsset getInkJson) {
        //Debug.Log("dhukche");
        currentStory = new Story(getInkJson.text);
        isPlaying = true;
        dialoguePanel.SetActive(true);

        ContinueStory();
    }

    public void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            textMesh.text = currentStory.Continue();
        }
        else
        {
            cor = StartCoroutine(ExitDialogueMode());
        }
    }

    private IEnumerator ExitDialogueMode()
    {
        yield return new WaitForSeconds(0.1f);
        //Debug.Log("cholche");
        isPlaying = false;
        dialoguePanel.SetActive(false);
        textMesh.text = "";
    }
}
