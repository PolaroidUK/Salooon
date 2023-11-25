using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterDialogue", menuName = "ScriptableObjects/CharacterDialogue")]
public class CharacterDialogue : ScriptableObject
{
    public Dialogue[] dialogue;
    public DialogueOption[] options;

}

[System.Serializable]
public class Dialogue
{
    public Character character;

    [TextArea(5, 15)]
    public string text;
}

[System.Serializable]
public class DialogueOption
{
    public string answer;
    public CharacterDialogue nextDialogue;
}