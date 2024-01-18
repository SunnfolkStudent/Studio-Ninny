using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fader : MonoBehaviour
{
    public float timer; // can match up fading
    public bool fadeIn = true, fadeOut;
    [SerializeField] private Image fader;
    
    public float fadeTime = 2;


    void Start()
    {
        fader.fillAmount = 1;
    }

    void Update()
    {
        //In
        if (fadeIn)
        {
            timer += Time.deltaTime;

            fader.color = new Color(0, 0, 0, 1 - timer/fadeTime); 
            
            if(timer > fadeTime)
                fadeIn = false;
        }
            
        
        //Out
        if (fadeOut)
        {
           
            

            //print("Fade Out");
            
            timer += Time.deltaTime;

            fader.color = new Color(0, 0, 0, timer / fadeTime); 
            
            if(fader.color.a > 0.99f)
            {
                fader.color = new Color(0, 0, 0, 1);

                fadeOut = false;
            } 
        }
    }
    public void FadeOut()
    {
        if(fadeIn) {print("No");}
        else
        {
            fadeOut = true;
            timer = 0;
        }
    }
}
