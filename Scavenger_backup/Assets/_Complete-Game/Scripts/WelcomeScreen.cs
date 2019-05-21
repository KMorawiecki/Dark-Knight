using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WelcomeScreen : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject PlayOfflineButton;
    Button playOfflineButton;
    
    void Start()
    {
        playOfflineButton = PlayOfflineButton.GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            ExitGame();
        }
    }

    public void PlayOffline()
    {

        PlayerInfo.Instance.StartGameCounter("anonymous");
        SceneManager.LoadScene("Final_Game", LoadSceneMode.Single);

    }

    public void GoToLoginScene()
    {
        SceneManager.LoadScene("LoginScene", LoadSceneMode.Single);
    }

    public void GoToRegisterScene()
    {
        SceneManager.LoadScene("RegisterScene", LoadSceneMode.Single);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
