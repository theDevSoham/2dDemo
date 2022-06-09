using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Ink.UnityIntegration;
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

    [Header("Globals Ink File")]
    [SerializeField] private InkFile globalsInkFile;

    [SerializeField] private InputAction _startAction;

    [SerializeField] private Button prefabButton;

    [SerializeField] private GameObject choicesObject;

    [SerializeField] private float buttonDistanceFactor = 100f;

    private DialogueVariables dialogueVariables;

    [SerializeField] private GameObject[] variablesInPhase; 

    [SerializeField] private GameObject choicesObjects;
    private GameObject[] choicesArray;
    private int numberOfObjects;

    public bool isPlaying { get; private set; }


    private void Awake()
    {
        if (instance != null) {
            Debug.LogWarning("More than one singleton class found");
        }
        instance = this;

        dialogueVariables = new DialogueVariables(globalsInkFile.filePath);

        GameObject g = GameObject.Find("ChoiceTypes");
        foreach(Transform go in g.transform) 
        {
            foreach(Transform child in go.gameObject.transform)
            {
                child.gameObject.SetActive(false);
            }
        }

        numberOfObjects = choicesObjects.GetComponent<NumberOfChoices>().Choices.Length;


        choicesArray = new GameObject[numberOfObjects];
        for(int i=0; i<numberOfObjects; i++)
        {
            if (numberOfObjects != 0)
            {
                choicesArray[i] = choicesObjects.GetComponent<NumberOfChoices>().Choices[i];
            }
        }
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

        dialogueVariables.StartListening(currentStory);

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
        dialogueVariables.StopListening(currentStory);
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
        //ObjectEquivalentChoice();
        DestroyChoices();
        ContinueStory();
        OtherInteractionsWithChoice(choice.index);
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

    public Ink.Runtime.Object GetVariableState(string variableName)
    {
        Ink.Runtime.Object variableValue = null;
        dialogueVariables.variables.TryGetValue(variableName, out variableValue);

        if(variableValue == null)
        {
            Debug.Log("Variable found to be null: " + variableName);
        }

        return variableValue;
    }

    private void OtherInteractionsWithChoice(int index)
    {
        int phaseCount = ((Ink.Runtime.IntValue)GetVariableState("storyPhase")).value - 1;
        GameObject chosenGameObj;

        //Get and activate Relevant Gameobject bound with choice
        Transform choiceToPhase = GameObject.Find("ChoiceTypes").transform.GetChild(phaseCount).transform;
        chosenGameObj = choiceToPhase.GetChild(index).gameObject;
        if (choiceToPhase.gameObject.name == "FinalPhase")
        {
            Debug.Log("Final Phase");
            choiceToPhase.GetChild(index).gameObject.SetActive(true);
        }
        else
        {
            choiceToPhase.GetChild(index).gameObject.SetActive(true);
        }
        //Move barrier to end of active gameobject

        MoveBarrier(chosenGameObj);
    }

    private void MoveBarrier(GameObject sizeReferrence)
    {
        GameObject barrier = GameObject.Find("Barrier");
        float sizeToRefer = sizeReferrence.transform.GetChild(0).gameObject.transform.localScale.x;
        barrier.transform.Translate((sizeToRefer), 0, 0);
    }



    //private void ObjectEquivalentChoice()
    //{
    //    //string pokemonName = ((Ink.Runtime.StringValue)GetVariableState("pokemonName")).value;
    //    int indexBound = ((Ink.Runtime.IntValue)GetVariableState("storyIndex")).value;

    //    GameObject nextPhase = Instantiate(choicesArray[indexBound], GameObject.Find("ChoiceTypes").transform);
    //    nextPhase.SetActive(true);
    //    nextPhase.transform.localPosition = Vector3.zero;

    //    //nextPhase.transform.GetChild(1).transform.GetChild(1).GetComponent<DialogueTrigger>().playerInteraction[1] = variablesInPhase[0];
    //    foreach (Transform t in nextPhase.transform)
    //    {
    //        if(t.gameObject.tag.Contains("NPC"))
    //        {
    //            foreach(Transform child in t.gameObject.transform)
    //            {
    //                if (child.gameObject.name.Contains("Trigger"))
    //                {
    //                    //child.gameObject.GetComponent<DialogueTrigger>().playerInteraction[1] = variablesInPhase[0];
    //                    child.gameObject.GetComponent<DialogueTrigger>().dialogueManager = variablesInPhase[1];
    //                }
    //            }
    //        }
    //    }
    //    //Debug.Log(nextPhase.transform.childCount);

    //    //List<GameObject> NPCs = new List<GameObject>();

    //    //NPCs = FindChildrenWithName(nextPhase, "NPC");

    //}

    private static List<GameObject> FindChildrenWithName(GameObject go, string name)
    {
        List<GameObject> children = new List<GameObject>();

        foreach (Transform t in go.transform)
        {
            if (t.gameObject.name.Contains(name))
            {
                children.Add(t.gameObject);
            }
        }
        return children;
    }
}
