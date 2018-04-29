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
    public bool isQuestion = false;
    public bool readyToAsk = false;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogText;
    public Image image;
    public Animator animator;
    private string sentence;
    private bool isTyping;
    public bool isBetweenConversation;
    Sprite sprite;
    private Queue<Dialog> dialogs;
    private Queue<string> sentences;
    public Event currentEvent;
    public static DialogManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

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
        

        if (!isTyping && sentences.Count != 0)
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
            if(!isQuestion){
                isBetweenConversation = false;
                endDialog();
                return;
            }
            else{
                readyToAsk = true;
            }
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
        Event current = currentEvent;
        currentEvent = null;
        isQuestion = false;
        readyToAsk = false;
        current.triggerNextEvent();
        if (!isBetweenConversation)
        {
            //Debug.Log("Stop");
            animator.SetBool("isOpen", false);
        }
        
    }

    public void skip()
    {
        StopAllCoroutines();
        dialogs.Clear();
        sentences.Clear();
        isBetweenConversation = false;
        isTyping = false;
        endDialog();
    }
}
