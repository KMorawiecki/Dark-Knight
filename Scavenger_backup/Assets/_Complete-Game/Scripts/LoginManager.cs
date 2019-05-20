using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{
    public GameObject Username;
    public GameObject Password;
    private string _username;
    private string _password;
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
            print("Error. Check internet connection!");
        }
        else
        {
            print("internet connected");
        }

        bool userLoggedIn = true;
       

        if (string.IsNullOrEmpty(_username))
            print("username empty");
        if (string.IsNullOrEmpty(_password))
            print("password empty");
      
        userLoggedIn = await ServerConnector.Login(_username, _password);
        
        if (userLoggedIn)
        { 
                print("user logged");
                Username.GetComponent<InputField>().text = "";
                Password.GetComponent<InputField>().text = "";
        }
        else print("wrong username or password");
    }
}
