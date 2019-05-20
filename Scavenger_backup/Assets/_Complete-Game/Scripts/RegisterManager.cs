using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class RegisterManager : MonoBehaviour
{
    public GameObject Username;
    public GameObject Password;
    public GameObject ConfPassword;
    public GameObject RegisterButton;
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
            print("Error. Check internet connection!");
        }
        else
        {
            print("internet connected");
        }

        bool existsUser = true;
        bool createdUser = false;

        if (string.IsNullOrEmpty(_username))
            print("username empty");
        if (string.IsNullOrEmpty(_password))
            print("password empty");
        else if(string.IsNullOrEmpty(_confPassword))
                print("confpass empty");
        if (_password.Equals(_confPassword))
            existsUser = await ServerConnector.CheckIfUserExists(_username);
        else print("pass and cond pass dont match");

        if (!existsUser)
        {
            createdUser = await ServerConnector.CreateUser(_username, _password);
            if (createdUser)
            {
                print("user created");
                Username.GetComponent<InputField>().text = "";
                Password.GetComponent<InputField>().text = "";
                ConfPassword.GetComponent<InputField>().text = "";
            }
               
        }
        else print("exists user and cannot be created");


    }


}
