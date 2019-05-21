using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScene : MonoBehaviour
{
    public GameObject TimeTextBox;
    public GameObject LevelTextBox;
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerInfo.PlayerName.Equals("anonymous"))
            LevelTextBox.GetComponent<Text>().text = "After " + PlayerInfo.Level + " days, you starved.";
        else
            LevelTextBox.GetComponent<Text>().text = PlayerInfo.PlayerName+", after " + PlayerInfo.Level + " days, you starved.";

        TimeTextBox.GetComponent<Text>().text = "Play time: " + PlayerInfo.GameTime;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnMainMenuClick()
    {

        SceneManager.LoadScene("WelcomeScene", LoadSceneMode.Single);
    }
    public void OnStatsClick()
    {

    }

    public void OnPlayAgainClick()
    {
        SceneManager.LoadScene("Final_Game", LoadSceneMode.Single);
       
    }
}
