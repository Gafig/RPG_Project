using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopUpText : MonoBehaviour {

    public TextMeshProUGUI dialogText;
    public Event ev;
    bool isTyping;
    string sentence;
    
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (isTyping)
            if (Input.GetKeyDown("z"))
            {
                ev.triggerNextEvent();
                Destroy(gameObject);
            }
	}

    public void type(string sentence)
    {
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
            yield return new WaitForSeconds(1f);
        }
        isTyping = false;
    }
}
