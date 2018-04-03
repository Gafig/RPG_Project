using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour, EventHandler {

    public Text nameText;
    public Text dialogText;
    public Animator animator;
    private string sentence;
    private bool isTyping;
    private bool isBetweenConversation;

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
        else if (Input.GetKeyDown("z"))
            displayNextSentence();
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
            dialogText.text += letter;
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
        }

        else if(sentences.Count == 0 && !isTyping)
            startDialog(dialogs.Dequeue());

        else
        {
            displayNextSentence();
        }
    }

    public void endDialog()
    {
        if(!isBetweenConversation)
            animator.SetBool("isOpen", false);
        NotifyGameControllerFinish();
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
