using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private AudioSource _audio; //drag in
    private bool fadingOut, fadingIn = true;
    [SerializeField] private float time;

    private void Start(){
        _audio.volume = 0;
    }

// fade out
    public void PlayGame(string scene)
    {        
        StartCoroutine(Wait_2(scene));
    }

    private IEnumerator Wait_2(string scene)
    {
        fadingOut = true;
        
        yield return new WaitUntil(() => _audio.volume < 0.175f);

        SceneManager.LoadScene(scene);
    }

    private void Update()
    {
        if(fadingOut){
            _audio.volume = Mathf.Lerp(_audio.volume, 0, time); //print("fadingOut");
        } else if(fadingIn){
            _audio.volume = Mathf.Lerp(_audio.volume, 1, time*2); //print("fadingIn");
        }

        if(_audio.volume > .95f){
            fadingIn = false;
        }
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
