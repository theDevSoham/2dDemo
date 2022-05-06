using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    [Header("Dialogue Panel")]
    [SerializeField] private GameObject dialoguePanel;

    [Header("Text Mesh")]
    [SerializeField] private TextMeshProUGUI textMesh;

    [Header("Ink File")]
    [SerializeField] private Story currentStory;

    [SerializeField] private InputAction _startAction;

    [SerializeField] private Button prefabButton;

    [SerializeField] private GameObject choicesObject;

    [SerializeField] private float buttonDistanceFactor = 100f;


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
            return;
        }

        _startAction.performed += context =>
        {
            if (context.performed)
            {
                ContinueStory();
            }
        };
    }

    

    public void EnterDialogueMode(TextAsset getInkJson) {
        currentStory = new Story(getInkJson.text);
        isPlaying = true;
        dialoguePanel.SetActive(true);

        ContinueStory();
    }

    public void ContinueStory()
    {

        //EraseUI();

        if (currentStory.canContinue)
        {
            textMesh.text = currentStory.Continue();
            DisplayChoices();
        }
        else
        {
            ExitDialogueMode();
        }
    }

    private void ExitDialogueMode()
    {
        isPlaying = false;
        dialoguePanel.SetActive(false);
        textMesh.text = "";
    }

    private void DisplayChoices() {
        List<Choice> currentChoices = currentStory.currentChoices;
        if (currentChoices.Count > 0) {
            GameObject continueButton = dialoguePanel.transform.GetChild(1).gameObject;
            continueButton.SetActive(false);
        }
        int i = 0;
        float pos = 0;
        foreach (Choice choice in currentChoices) {
            
            Button choiceButton = Instantiate(prefabButton);
            choiceButton.transform.SetParent(choicesObject.transform, false);
            choiceButton.transform.Translate(0, pos, 0);

            TextMeshProUGUI choiceButtonText = choiceButton.GetComponentInChildren<TextMeshProUGUI>();

            choiceButtonText.text = choice.text;
            pos -= buttonDistanceFactor;

            choiceButton.onClick.AddListener(delegate
            {
                MakeChoice(choice);
            });
            i++;
        }
    }

    private void MakeChoice(Choice choice) {
        GameObject continueButton = dialoguePanel.transform.GetChild(1).gameObject;
        continueButton.SetActive(true);
        currentStory.ChooseChoiceIndex(choice.index);
        DestroyChoices();
        ContinueStory();
    }

    private void DestroyChoices() {
        GameObject choiceParent = dialoguePanel.transform.GetChild(2).gameObject;
        if (choiceParent.transform.childCount > 0) {

            //Store gameobjects
            GameObject[] allChildren = new GameObject[choiceParent.transform.childCount];
            int i = 0;
            foreach (Transform child in choiceParent.transform) {
                allChildren[i] = child.gameObject;
                i++;
            }

            //Use gameobjects
            foreach (GameObject child in allChildren) { 
                DestroyImmediate(child);
            }
        }
    }
}
