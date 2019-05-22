using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StatsScene : MonoBehaviour
{
    public GameObject Day1;
    public GameObject Day2;
    public GameObject Day3;
    public GameObject Day4;
    public GameObject Day5;
    public GameObject Day1Val;
    public GameObject Day2Val;
    public GameObject Day3Val;
    public GameObject Day4Val;
    public GameObject Day5Val;

    public GameObject Time1;
    public GameObject Time2;
    public GameObject Time3;
    public GameObject Time4;
    public GameObject Time5;
    public GameObject Time1Val;
    public GameObject Time2Val;
    public GameObject Time3Val;
    public GameObject Time4Val;
    public GameObject Time5Val;

    public GameObject InfoLabel;

    //List<PlayerDTO> rankingByLevel;
    //List<PlayerDTO> adminIslogged;

    // Start is called before the first frame update
      void  Start()
    {

        StartCoroutine("LoadPlayTimeChart");
        StartCoroutine("LoadLevelChart");

    }

    // Update is called once per frame
    void Update()
    {

    }

    void CheckInternetConnection()
    {
        //if (Application.internetReachability == NetworkReachability.NotReachable)
        //{
        //    InfoLabel.GetComponent<Text>().text = "Check internet connection!";
        //    print("Error. Check internet connection!");
        //}
    }
    public void OnMainMenuButton()
    {
        SceneManager.LoadScene("WelcomeScene", LoadSceneMode.Single);
    }

    public void OnDownloadButton()
    {




    }

    public async Task LoadLevelChart()
    {
        var task = Task.Run(() => ServerConnector.GetPlayersByLevel());
        task.Wait();
        var playersLevel = task.Result;

        if (playersLevel.Count > 0)
        {
            Day1.GetComponent<Text>().text = playersLevel[0].Name;
            Day1Val.GetComponent<Text>().text = playersLevel[0].MaxLevel.ToString();

            if (playersLevel.Count > 1)
            {
                Day2.GetComponent<Text>().text = playersLevel[1].Name;
                Day2Val.GetComponent<Text>().text = playersLevel[1].MaxLevel.ToString();

                if (playersLevel.Count > 2)
                {
                    Day3.GetComponent<Text>().text = playersLevel[2].Name;
                    Day3Val.GetComponent<Text>().text = playersLevel[2].MaxLevel.ToString();

                    if (playersLevel.Count > 3)
                    {
                        Day4.GetComponent<Text>().text = playersLevel[3].Name;
                        Day4Val.GetComponent<Text>().text = playersLevel[3].MaxLevel.ToString();

                        if (playersLevel.Count > 4)
                        {
                            Day5.GetComponent<Text>().text = playersLevel[4].Name;
                            Day5Val.GetComponent<Text>().text = playersLevel[4].MaxLevel.ToString();
                        }

                    }
                }
            }
        }
    }

    public async Task LoadPlayTimeChart()
    {
        var task = Task.Run(() => ServerConnector.GetPlayersByTime());
        task.Wait();
        var playersTime = task.Result;

        if(playersTime.Count > 0)
        {
            Time1.GetComponent<Text>().text = playersTime[0].Name;
            Time1Val.GetComponent<Text>().text = playersTime[0].TotalTime.ToString();

            if (playersTime.Count > 1)
            {
                Time2.GetComponent<Text>().text = playersTime[1].Name;
                Time2Val.GetComponent<Text>().text = playersTime[1].TotalTime.ToString();

                if (playersTime.Count > 2)
                {
                    Time3.GetComponent<Text>().text = playersTime[2].Name;
                    Time3Val.GetComponent<Text>().text = playersTime[2].TotalTime.ToString();

                    if (playersTime.Count > 3)
                    {
                        Time4.GetComponent<Text>().text = playersTime[3].Name;
                        Time4Val.GetComponent<Text>().text = playersTime[3].TotalTime.ToString();

                        if (playersTime.Count > 4)
                        {
                            Time5.GetComponent<Text>().text = playersTime[4].Name;
                            Time5Val.GetComponent<Text>().text = playersTime[4].TotalTime.ToString();

                        }
                    }
                }
            }
        }
    }
}
