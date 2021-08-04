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
    [SerializeField] private Button buttonPrefab = null;

    [SerializeField] DialogueProcessor dialogueProcessor;

    bool isTalking = false;
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
        story = new Story(dialogue.text);
        if (story != null)
        {
            //OnCreateStory(story);
            //COMMENT OR UNCOMMENT THIS FOR INKY EDITOR DEBUG MODE!! todo
        }
        RefreshView();
    }
    void RefreshView()
    {
        // Remove all the UI on screen
        RemoveChildren();

        // Read all the content until we can't continue any more
        while (story.canContinue)
        {
            // Continue gets the next line of the story
            string text = story.Continue();
            // This removes any white space from the text.
            text = text.Trim();
            // Display the text on screen!
            CreateContentView(text);
        }

        // Display all the choices, if there are any!
        if (story.currentChoices.Count > 0)
        {//add coroutine stuff here TODO
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
        else
        {
            dialogueProcessor.DisableDialogue();
        }
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
    void CreateContentView(string text)
    {
        Text storyText = Instantiate(textPrefab) as Text;
        storyText.text = text;
        storyText.transform.SetParent(canvas.transform, false);
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

}
