using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour {

    public Text nameText;
    public Text dialogText;

    private Queue<string> sentences;
	// Use this for initialization
	void Start () {
        sentences = new Queue<string>();
	}

    private void Update()
    {
        if (Input.GetKeyDown("z"))
            displayNextSentence();
    }

    public void startDialog( Dialog dialog)
    {
        nameText.text = dialog.name;
        sentences.Clear();
        foreach(string sentence in dialog.sentences)
            sentences.Enqueue(sentence);
        displayNextSentence();
    }

    public void displayNextSentence()
    {
        if(sentences.Count == 0)
        {
            endDialog();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogText.text = sentence;
    }

    public void endDialog()
    {

    }
	
}
