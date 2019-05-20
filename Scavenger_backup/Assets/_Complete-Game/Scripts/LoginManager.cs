using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{
    public GameObject Username;
    public GameObject Password;
    public GameObject InfoTextbox;
    private string _username;
    private string _password;
    private string _infoTextBox;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        NavigateOnKeys();
        _username = Username.GetComponent<InputField>().text;
        _password = Password.GetComponent<InputField>().text;
        
    }

    private void NavigateOnKeys()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (Username.GetComponent<InputField>().isFocused)
                Password.GetComponent<InputField>().Select();
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!string.IsNullOrEmpty(_username) && !string.IsNullOrEmpty(_password))
            {
                LoginButtonAsync();
            }

        }

    }

    public async void LoginButtonAsync()
    {
        
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            InfoTextbox.GetComponent<Text>().text = "Check internet connection!";
            print("Error. Check internet connection!");
        }
        else
        {
            print("internet connected");
        }

        bool userLoggedIn = true;
       
        if (string.IsNullOrEmpty(_username))
        {
            InfoTextbox.GetComponent<Text>().text = "Username is empty.";
            print("Username is empty.");
        }

        if (string.IsNullOrEmpty(_password))
        {
            InfoTextbox.GetComponent<Text>().text = "Password is empty.";
            print("Password is empty.");
        }

        InfoTextbox.GetComponent<Text>().text = "Connecting to server...";
        userLoggedIn = await ServerConnector.Login(_username, _password);

        if (userLoggedIn)
        {
            print("user logged");

            SceneManager.LoadScene("Final_Game", LoadSceneMode.Single);

        }
        else
        {
            InfoTextbox.GetComponent<Text>().text = "Wrong credentials.";
            print("Wrong credentials.");
        } 
    }

    public void GoToWelcomeScene()
    {
        SceneManager.LoadScene("WelcomeScene", LoadSceneMode.Single);
    }
}
