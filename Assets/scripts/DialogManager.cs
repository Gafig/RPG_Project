using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour {

    public Text nameText;
    public Text dialogText;
    public Animator animator;
    private string sentence;
    private bool isTyping;

    private Queue<string> sentences;
	// Use this for initialization
	void Start () {
        isTyping = false;
        sentences = new Queue<string>();
	}

    private void Update()
    {
        if (Input.GetKeyDown("z"))
        {
            displayNextSentence();
        }
    }

    public void startDialog( Dialog dialog)
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
        if(sentences.Count == 0 && !isTyping)
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

    public void endDialog()
    {
        animator.SetBool("isOpen", false);
    }
	
}
