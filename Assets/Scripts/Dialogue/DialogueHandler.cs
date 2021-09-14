using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using Ink.UnityIntegration;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class DialogueHandler : MonoBehaviour
{
    TextMeshProUGUI dialogueBox;
    Image portrait;
    TextMeshProUGUI speaker;
    RectTransform choiceContainer;
    [SerializeField] GameObject choiceButtonPrefab;
    List<GameObject> choiceButtons =  new List<GameObject>();

    Story story;

    private void Awake()
    {
        foreach(RectTransform child in gameObject.GetComponentsInChildren<Transform>().ToList())
        {
            switch (child.name)
            {
                case "Dialogue":
                    dialogueBox = child.GetComponent<TextMeshProUGUI>();
                    break;
                case "Portrait":
                    portrait = child.GetComponent<Image>();
                    break;
                case "Speaker":
                    speaker = child.GetComponent<TextMeshProUGUI>();
                    break;
                case "Content":
                    choiceContainer = child;
                    break;
            }
        }
    }

    public void StartStory(TextAsset dialogue, DialogueProcessor processor)
    {
        story = new Story(dialogue.text);
        StartCoroutine(HandleStory(processor));
    }
    
    IEnumerator HandleStory(DialogueProcessor processor)
    {
        while(story.canContinue || story.currentChoices.Count > 0)
        {
            if (story.canContinue)
            {
                story.Continue();
                dialogueBox.text = story.currentText;
                if (story.currentTags.Count > 0)
                {
                    speaker.text = story.currentTags[0];
                }
            }

            if(story.currentChoices.Count > 0 && choiceButtons.Count < 1)
            {
                for (int i = 0; i < story.currentChoices.Count; i++)
                {
                    int j = i;//Fixes pass by reference issue
                    GameObject obj = Instantiate(choiceButtonPrefab, choiceContainer);
                    choiceButtons.Add(obj);
                    obj.GetComponent<Button>().onClick.AddListener(delegate()
                    {
                        OnClicked(j);
                    });
                    choiceButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = (i + 1 + ": " + story.currentChoices[i].text);
                }
            }
            yield return null;
        }
        processor.DisableDialogue();
        yield return null;
    }

    void OnClicked(int index)
    {
        story.ChooseChoiceIndex(index);
        foreach(GameObject obj in choiceButtons)
        {
            GameObject.Destroy(obj);
        }
        choiceButtons.Clear();
    }
}
