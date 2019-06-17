using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingScene : MonoBehaviour
{
    public GameObject InfoLabel;
    // Start is called before the first frame update
    void Start()
    {
       // InfoLabel.GetComponent<Text>().text = "Connecting to server...";


        var rankingByLevel =  ServerConnector.GetPlayersByLevel().Result;

        if (rankingByLevel.Count>0)
        {
            InfoLabel.GetComponent<Text>().text = " elements";

        }
    //    else
    //        InfoLabel.GetComponent<Text>().text = " no no no";
    }

    // Update is called once per frame
    void Update()
    {


    }
}
