using System;
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

        if (PlayerInfo.GameTime < 60)
        {
            TimeTextBox.GetComponent<Text>().text = "Play time: " + PlayerInfo.GameTime + " sec." ;
        }
        else
        {
            var minutes = Math.Floor((double)PlayerInfo.GameTime/60);
            var seconds = (PlayerInfo.GameTime) % 60;
            TimeTextBox.GetComponent<Text>().text = "Play time: " +minutes+" min "+ seconds + "sec.";
            print(PlayerInfo.GameTime);

        }
        if (!PlayerInfo.PlayerName.Equals("anonymous"))
            SendGameInfo();

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

    void SendGameInfo()
    {
        GameInfoDTO gameInfo = new GameInfoDTO()
        {
            Name = PlayerInfo.PlayerName,
            GameTime = PlayerInfo.GameTime,
            Level = PlayerInfo.Level,
        };
        var call = ServerConnector.UpdateAfterGame(gameInfo);

        if (call.IsCanceled)
        {
            print("ok");
        }
    }

}
