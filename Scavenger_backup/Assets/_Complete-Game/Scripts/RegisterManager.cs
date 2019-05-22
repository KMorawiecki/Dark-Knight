using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RegisterManager : MonoBehaviour
{
    public GameObject Username;
    public GameObject Password;
    public GameObject ConfPassword;
    public GameObject RegisterButton;
    public GameObject InfoTextbox;

    private string _username;
    private string _password;
    private string _confPassword;
    private string _form;
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
        _confPassword = ConfPassword.GetComponent<InputField>().text;

    }

    private void NavigateOnKeys()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (Username.GetComponent<InputField>().isFocused)
                Password.GetComponent<InputField>().Select();

            if (Password.GetComponent<InputField>().isFocused)
                ConfPassword.GetComponent<InputField>().Select();
        }
        if (Input.GetKeyDown(KeyCode.Return) )
        {
            if(!string.IsNullOrEmpty(_username) && !string.IsNullOrEmpty(_password) && !string.IsNullOrEmpty(_confPassword))
            {
                RegisterButtonAsync();
            }

        }

    }

    public async void RegisterButtonAsync()
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

        bool existsUser = true;
        bool createdUser = false;

        if (string.IsNullOrEmpty(_username))
        {
            InfoTextbox.GetComponent<Text>().text = "Username is empty";
            print("username empty");
            return;
        }

        if (string.IsNullOrEmpty(_password))
        {
            InfoTextbox.GetComponent<Text>().text = "Password is empty.";
            print("confpass empty");
            return;
        }

        if (string.IsNullOrEmpty(_confPassword))
        {
            InfoTextbox.GetComponent<Text>().text = "Password confirmation is empty.";
            print("confpass empty");
            return;
        }

        if (_password.Equals(_confPassword))
        {
            InfoTextbox.GetComponent<Text>().text = "Connecting to server...";
            existsUser = await ServerConnector.CheckIfUserExists(_username);           
        }
        else
        {
            InfoTextbox.GetComponent<Text>().text = "Passwords do not match.";
            print("pass and cond pass dont match");
            return;
        }

        if (!existsUser)
        {
            InfoTextbox.GetComponent<Text>().text = "Connecting to server...";
            createdUser = await ServerConnector.CreateUser(_username, _password);
            if (createdUser)
            {
                InfoTextbox.GetComponent<Text>().text = "User created.";
                print("user created");
                //  Username.GetComponent<InputField>().text = "";
                //  Password.GetComponent<InputField>().text = "";
                //  ConfPassword.GetComponent<InputField>().text = "";
            }

        }
        else
        {
            InfoTextbox.GetComponent<Text>().text = "User cannot be created.";
            print("exists user and cannot be created");
        }

    }

    public void GoToWelcomeScene()
    {
        SceneManager.LoadScene("WelcomeScene", LoadSceneMode.Single);
    }
}
