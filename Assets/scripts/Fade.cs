using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour {

    public static Fade instance;
    public Animator animator;
    public bool isFading;
    public Image fade;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        isFading = false;
        fade = gameObject.GetComponent<Image>();
    }

    public void fadeIn()
    {
        isFading = true;
        StartCoroutine(fadingIn());
    }

    private IEnumerator fadingIn()
    {
        animator.SetBool("Fade", false);
        yield return new WaitUntil(() => fade.color.a == 0);
        doneFading();
    }

    public void fadeOut()
    {
        isFading = true;
        StartCoroutine(fadingOut());
    }

    private IEnumerator fadingOut()
    {
        animator.SetBool("Fade", true);
        yield return new WaitUntil(() => fade.color.a == 1);
        doneFading();
    }

    private void doneFading()
    {
        isFading = false;
    }
}
