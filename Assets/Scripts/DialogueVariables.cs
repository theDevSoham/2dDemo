using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using System.IO;

public class DialogueVariables
{
    public Dictionary<string, Ink.Runtime.Object> variables;

    public DialogueVariables(string globalFilesPath)
    {
        string inkFileContents = File.ReadAllText(globalFilesPath);
        Ink.Compiler compiler = new Ink.Compiler(inkFileContents);
        Story globalVariableStory = compiler.Compile();

        variables = new Dictionary<string, Ink.Runtime.Object>();

        foreach(string name in globalVariableStory.variablesState)
        {
            Ink.Runtime.Object value = globalVariableStory.variablesState.GetVariableWithName(name);
            variables.Add(name, value);
            Debug.Log("Initialized " + name + " as: " + value);
        }
    }
    public void StartListening(Story story)
    {
        VariableToStory(story);
        story.variablesState.variableChangedEvent += VariablesChanged;
    }
    public void StopListening(Story story)
    {
        story.variablesState.variableChangedEvent -= VariablesChanged;
    }
    private void VariablesChanged(string name, Ink.Runtime.Object value)
    {
        if (variables.ContainsKey(name))
        {
            variables.Remove(name);
            variables.Add(name, value);
            Debug.Log(name + " Changed to: " + value);
        }
    }

    private void VariableToStory(Story story)
    {
        foreach(KeyValuePair<string, Ink.Runtime.Object> variable in variables)
        {
            story.variablesState.SetGlobal(variable.Key, variable.Value);
        }
    }
}
