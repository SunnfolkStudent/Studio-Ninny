using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    [SerializeField] private float firstDelay, lastDelay, scrollSpeed, boatSpeed;
    private bool scrolling, leaving;

    private Animator _anim;

    [SerializeField] private Fader _fader;


    void Start()
    {
        _anim = transform.GetChild(0).GetComponent<Animator>();

        StartCoroutine(CreditsSequence());
    }

    private IEnumerator CreditsSequence()
    {
        yield return new WaitForSeconds(firstDelay);

        scrolling = true;

        yield return new WaitUntil(() => transform.position.y < -20); // hardcoded
        
        scrolling = false;
        leaving = true;

        _anim.speed = 2; // hardcoded

        _fader.FadeOut();

        yield return new WaitUntil(() => transform.GetChild(0).position.x > 13); // hardcoded

        SceneManager.LoadScene("Menu");
    }

    void FixedUpdate()
    {
        if(scrolling)
            transform.position += new Vector3(0, -scrollSpeed*Time.fixedDeltaTime, 0);
        
        if(leaving)
            transform.GetChild(0).position += new Vector3(boatSpeed*Time.fixedDeltaTime, 0, 0);
    }
}