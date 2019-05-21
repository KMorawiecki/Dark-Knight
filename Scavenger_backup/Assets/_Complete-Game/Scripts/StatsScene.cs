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
     void Start()
    {


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
        InfoLabel.GetComponent<Text>().text = "Connecting to server...";


        var rankingByLevel =   ServerConnector.GetPlayersByLevel().Result;

        if (rankingByLevel != null)
        {
            InfoLabel.GetComponent<Text>().text = " elements";

        }
        else
            InfoLabel.GetComponent<Text>().text = " no no no";

    }
}
