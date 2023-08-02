using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;
using Update = UnityEngine.PlayerLoop.Update;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public PlayerInput input;

    void Update()
    {
        if (input.escPressed && GameIsPaused)
        {
            print("lols");
            Resume();
        }
        
        else if (input.escPressed)
        {
            Pause();
        }
        
    }

    private bool hadControll = true;

    public void Resume()
    {
        print("Resume");
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        
        if(hadControll)
            input.characterControl = true;
    }
    public void Pause()
    {
        print("Pause");
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;

        if(input.characterControl == false)
            hadControll = false;
        else 
            hadControll = true;
        
        input.characterControl = false;
    }

    public void LoadGame(string scene)
    {
        SceneManager.LoadScene(scene);
        Resume();
    }
    
    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
