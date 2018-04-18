using System;
using System.Collections;
using System.Collections.Generic;
//watch here
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour {

    //public Text nameText;
    //public Text dialogText;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogText;
    public Image image;
    public Animator animator;
    private string sentence;
    private bool isTyping;
    private bool isBetweenConversation;
    Sprite sprite;
    private Queue<Dialog> dialogs;
    private Queue<string> sentences;
	// Use this for initialization
	void Start () {
        isTyping = false;
        sentences = new Queue<string>();
        dialogs = new Queue<Dialog>();
	}

    private void Update()
    {
        if (isBetweenConversation)
        {
            if (Input.GetKeyDown("z"))
                displayNextDialog();
        }
    }

    public void startConversation(DialogConversation conversation)
    {
        if (!NotifyGameControllerStart())
            return;
        isBetweenConversation = true;
        dialogs.Clear();
        foreach (Dialog dialog in conversation.dialogs)
            dialogs.Enqueue(dialog);
        displayNextDialog();
    }

    public void startDialog(Dialog dialog)
    {
        image.enabled = true;
        if (dialog.sprite == null)
            image.enabled = false;
        else
        {
            image.sprite = dialog.sprite;
            image.SetNativeSize();
        }
        animator.SetBool("isOpen", true);
        nameText.text = dialog.name;
        sentences.Clear();
        foreach(string sentence in dialog.sentences)
            sentences.Enqueue(sentence);
        displayNextSentence();
    }

    IEnumerator typeSentence(string sentence)
    {
        dialogText.text = "";
        isTyping = true;
        foreach (char letter in sentence.ToCharArray())
        {
            dialogText.text = dialogText.text + letter;
            yield return new WaitForSeconds(0.05f);
        }
        isTyping = false;
    }

    public void displayNextSentence()
    {
        if (sentences.Count == 0 && !isTyping)
        {
            endDialog();
            return;
        }

        if (!isTyping)
        {
            sentence = sentences.Dequeue();
            StartCoroutine(typeSentence(sentence));
        }
        else
        {
            StopAllCoroutines();
            dialogText.text = sentence;
            isTyping = false;
        }
    }

    public void displayNextDialog()
    {
        if(dialogs.Count == 0 && sentences.Count == 0 && !isTyping)
        {
            isBetweenConversation = false;
            endDialog();
            return;
        }

        if (sentences.Count == 0 && !isTyping && dialogs.Count > 0)
        {
            sprite = dialogs.Peek().sprite;
            startDialog(dialogs.Dequeue());
        }

        else
        {
            displayNextSentence();
        }
    }

    public void endDialog()
    {
        NotifyGameControllerFinish();
        if (!isBetweenConversation)
        {
            Debug.Log("Stop");
            animator.SetBool("isOpen", false);
        }
        
    }

    public bool NotifyGameControllerStart()
    {
        return GameMasterController.instance.startEvent();
    }

    public void NotifyGameControllerFinish()
    {
        GameMasterController.instance.endEvent();
    }
}
