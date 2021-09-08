using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using Ink.UnityIntegration;
using UnityEngine.UI;

public class DialogueHandler : MonoBehaviour
{

    private Story story;

    [SerializeField] private Canvas canvas = null;

    // UI Prefabs
    [SerializeField] private Text textPrefab = null;
    Text text;
    [SerializeField] private Button buttonPrefab = null;

    [SerializeField] DialogueProcessor dialogueProcessor;

    bool isTalking = false;
    bool ending = false;
    List<string> tags;
    /*
    TextAsset nametag;
    TextAsset message;*/

    /*TextAsset inkFile;
    GameObject textBox;
    GameObject customButton;
    GameObject optionPanel;
    TextAsset nametag;
    TextAsset message;
    Choice choiceSelected;*/



    public void StartStory(TextAsset dialogue)
    {
        RemoveChildren();
        isTalking = false;
        ending = false;
        story = new Story(dialogue.text);
        if (story != null)
        {
            OnCreateStory(story);
            //COMMENT OR UNCOMMENT THIS FOR INKY EDITOR DEBUG MODE!! todo
        }
        RefreshView();
    }
    void RefreshView()
    {
        // Remove all the UI on screen
        RemoveChildren();
        StopAllCoroutines();

        // Display all the choices, if there are any!
        if (story.canContinue)
        {//add coroutine stuff here TODO
            AdvanceDialogue();
            if (story.currentChoices.Count != 0)
            {
                StartCoroutine(ShowChoices());
            }
        }
        else
        {
            EndDialogue();
        }
    }
    public void ProceedDialogue()
    {
        if (!ending)
        {
            RefreshView();
        }
        else
        {
            EndDialogue();
        }
    }
    void EndDialogue()
    {
        dialogueProcessor.DisableDialogue();

    }
    void AdvanceDialogue()
    {
        ResetText();
        string currentSentence = story.Continue();
        if (!story.canContinue)
        {
            ending = true;
        }
        isTalking = true;
        ParseTags();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(currentSentence));
    }
    void ParseTags()
    {
        tags = story.currentTags;
        foreach (string t in tags)
        {
            string prefix = t.Split(' ')[0];
            string param = t.Split(' ')[1];

            switch (prefix.ToLower())
            {
                case "anim":
                    SetAnimation(param);
                    break;
                case "color":
                    SetTextColor(param);
                    break;
            }
        }
    }
    bool IsNotTalking()
    {
        return !isTalking;
    }
    IEnumerator ShowChoices()
    {
        yield return new WaitUntil(IsNotTalking);
        List<Choice> _choices = story.currentChoices;

        for (int i = 0; i < story.currentChoices.Count; i++)
        {
            Choice choice = story.currentChoices[i];
            Button button = CreateChoiceView(choice.text.Trim());
            // Tell the button what to do when we press it
            button.onClick.AddListener(delegate {
                OnClickChoiceButton(choice);
            });
        }

    }
    IEnumerator TypeSentence(string sentence)
    {

        text.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            text.text += letter;
            yield return null;
        }
        yield return null;
        isTalking = false;
    }
    void OnClickChoiceButton(Choice choice)
    {
        story.ChooseChoiceIndex(choice.index);
        RefreshView();
    }

    void OnCreateStory(Story story)
    {
        // If you'd like NOT to automatically show the window and attach (your teammates may appreciate it!) then replace "true" with "false" here. 
        InkPlayerWindow window = InkPlayerWindow.GetWindow(true);
        if (window != null)
        {
            InkPlayerWindow.Attach(story);
        }
    }

    void ResetText()
    {
        text = Instantiate(textPrefab) as Text;
        text.transform.SetParent(canvas.transform, false);
    }
    Button CreateChoiceView(string text)
    {
        // Creates the button from a prefab
        Button choice = Instantiate(buttonPrefab) as Button;
        choice.transform.SetParent(canvas.transform, false);

        // Gets the text from the button prefab
        Text choiceText = choice.GetComponentInChildren<Text>();
        choiceText.text = text;

        // Make the button expand to fit the text
        HorizontalLayoutGroup layoutGroup = choice.GetComponent<HorizontalLayoutGroup>();
        layoutGroup.childForceExpandHeight = false;

        return choice;
    }

    // Destroys all the children of this gameobject (all the UI)
    void RemoveChildren()
    {
        int childCount = canvas.transform.childCount;
        for (int i = childCount - 1; i >= 0; --i)
        {
            GameObject.Destroy(canvas.transform.GetChild(i).gameObject);
        }
    }
    void SetAnimation(string _name)
    {
        //TODO implement animation for each character possibly
    }
    void SetTextColor(string _color)
    {
        switch (_color)
        {
            case "red":
                textPrefab.color = Color.red;
                break;
            case "blue":
                textPrefab.color = Color.cyan;
                break;
            case "green":
                textPrefab.color = Color.green;
                break;
            case "white":
                textPrefab.color = Color.white;
                break;
            default:
                Debug.Log($"{_color} is not available as a text color");
                break;
        }
    }

}
