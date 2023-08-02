using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private float time;
    private AudioSource _audio;

    [SerializeField] private Fader fader;

    private bool fading;
    private float volume;

    void Start()
    {
        _audio = GetComponent<AudioSource>();

        volume = _audio.volume;

        DontDestroyOnLoad(gameObject);
        StartCoroutine(NextScene());
    }

    private IEnumerator NextScene()
    {
        yield return new WaitUntil(() => time <= Time.timeSinceLevelLoad);
        
        SceneManager.LoadScene("Tutorial");
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0) // Menu scene
        {
            Destroy(gameObject);
        }

        if(SceneManager.GetActiveScene().buildIndex == 10 && fader.fadeOut) // Credits scene
        {
            _audio.volume = volume - (fader.timer/fader.fadeTime) * volume;
        }

        if(SceneManager.GetActiveScene().buildIndex != 1) // Music scene
            return;

        if(!fading && Time.timeSinceLevelLoad >= time - fader.fadeTime){
            fading = true;
            fader.FadeOut();
        }
            
    }
}
