using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopUpText : MonoBehaviour {

    public TextMeshProUGUI dialogText;
    public Event ev;
    bool isTyping = false;
    string sentence;
    bool started = false;
	
	// Update is called once per frame
	void Update () {
        if (!isTyping && started)
            if (Input.GetKeyDown("z"))
            {
                ev.triggerNextEvent();
                Destroy(gameObject);
            }
	}

    public void type(string sentence)
    {
        started = true;
        this.sentence = sentence;
        StartCoroutine(typeSentence(sentence));
    }

    IEnumerator typeSentence(string sentence)
    {
        dialogText.text = "";
        isTyping = true;
        foreach (char letter in sentence.ToCharArray())
        {
            Debug.Log(letter);
            dialogText.text = dialogText.text + letter;
            yield return new WaitForSeconds(0.05f);
        }
        isTyping = false;
    }
}
