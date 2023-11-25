using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueUI;
    public TMP_Text characterName;
    public TMP_Text dialogueText;
    public Image characterImage;

    CharacterDialogue curDialogue;
    int dialogueIndex = 0;
    bool showingDialogue = false;

    public GameObject optionsGroup;
    public GameObject optionBtnPrefab;

    bool showingOptions = false;
    public string backBtnText = "Back";

    List<GameObject> addedOptionBtns = new List<GameObject>();

    private void Awake()
    {
        dialogueUI.SetActive(false);
        optionBtnPrefab.SetActive(false);
    }

    public void StartDialogue(CharacterDialogue characterDialogue)
    {
        if (!showingOptions && !showingDialogue)
        {
            curDialogue = characterDialogue;
            showingDialogue = true;
            dialogueUI.SetActive(true);
            dialogueIndex = 0;
            ShowDialogue();
        }
    }

    public void OptionBtnClick(int index)
    {
        optionsGroup.SetActive(false);
        foreach (GameObject option in addedOptionBtns)
            Destroy(option);

        addedOptionBtns.Clear();

        DialogueOption[] options = curDialogue.options;
        showingOptions = false;

        print(index);

        if (options.Length > index && options[index].nextDialogue)
            StartDialogue(options[index].nextDialogue);
    }

    void ShowOptions()
    {
        DialogueOption[] options = curDialogue.options;
        if (options.Length > 0)
        {
            optionsGroup.SetActive(true);
            for (int i = 0; i < options.Length + 1; i++)
            {
                GameObject optionBtn = Instantiate(optionBtnPrefab, optionsGroup.transform);
                TMP_Text optionText = optionBtn.GetComponentInChildren<TMP_Text>();
                if (i < options.Length)
                    optionText.text = options[i].answer;
                else
                    optionText.text = backBtnText;
                optionBtn.SetActive(true);

                int index = i;
                optionBtn.GetComponent<Button>().onClick.AddListener(() => OptionBtnClick(index));
                addedOptionBtns.Add(optionBtn);
            }
            showingOptions = true;
        }
    }

    void ShowDialogue()
    {
        if (curDialogue.dialogue.Length <= dialogueIndex)
        {
            showingDialogue = false;
            dialogueUI.SetActive(false);
            ShowOptions();
        }
        else
        {
            Character character = curDialogue.dialogue[dialogueIndex].character;
            characterName.text = character.name;
            characterImage.sprite = character.characterSprite;
            dialogueText.text = curDialogue.dialogue[dialogueIndex].text;
        }
    }

    public bool CurrentlyShowingUI()
    {
        return showingOptions || showingDialogue;
    }

    private void Update()
    {
        if (!showingOptions && showingDialogue && Input.GetMouseButtonDown(0))
        {
            dialogueIndex++;
            ShowDialogue();
        }
    }
}
