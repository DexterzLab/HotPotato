using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseScript : MonoBehaviour
{
    public GameObject CreditsUI;
    public GameObject MainUI;


    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CreditsUI.SetActive(true);
            MainUI.SetActive(false);
            Debug.Log("Credits opened");
        }

    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log("Scene has been restarted");
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit game");

    }

    public void OpenCredits()
    {
        CreditsUI.SetActive(true);
        MainUI.SetActive(false);
        Debug.Log("Credits opened");
    }

    public void RemoveCredits()
    {
        CreditsUI.SetActive(false);
        MainUI.SetActive(true);
        Debug.Log("Credits closed");
    }
}
