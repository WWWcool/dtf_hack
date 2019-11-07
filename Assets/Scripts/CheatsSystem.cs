using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class CheatsSystem : MonoBehaviour
{
    private static CheatsSystem instance = null;
    private bool isActive;
    [SerializeField]
    private GameObject cheatsUI;
    [SerializeField]
    private GameObject cheatsWindow;
    [SerializeField]
    private GameObject ScenesButtons;
    [SerializeField]
    private GameObject inputField;
    [SerializeField]
    private InputField input;
    string value;
    
    public string CurrentCommand { get; set; }

    public bool IsActive
    {
        get { return isActive; }
        set 
        { 
            isActive = value;
            if (isActive) cheatsUI.SetActive(true);
            else cheatsUI.SetActive(false);
        }
    }
    public static CheatsSystem Instance
    {
        get
        {
            if (instance == null) Debug.LogError("CheatsSystem is Null");
            return instance;
        }
    }

    private void Start()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        cheatsUI.SetActive(false);
        cheatsWindow.SetActive(true);
        ScenesButtons.SetActive(false);
        inputField.SetActive(false);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.BackQuote) && !isActive)
        {
            IsActive = true;
        }else if (Input.GetKeyDown(KeyCode.BackQuote) && isActive)
        {
            IsActive = false;
        }

        Check();
    }

    private void Check()
    {
        if (!Input.GetKeyDown(KeyCode.Return) || !isActive) return;

        string[] tmp = input.text.Split(' ');

        if (tmp.Length == 0) return;
        else if (tmp.Length == 1)
        {
            value = tmp[0];
        }
        else
        {
            Error(input.text);
            return;
        }

        GetCheat();
    }
    private void GetCheat()
    {
        switch (CurrentCommand)
        {
            case "setGravityValue":
                if (int.TryParse(value,out int gravity))
                {
                    Debug.Log($"game gravity is set to {gravity}");
                    Physics2D.gravity = new Vector2(0, gravity);
                }
                else
                {
                    Error(value);
                }
                break;
            default:
                Error();
                break;
        }
        input.text = string.Empty;
    }
    private void Error()
    {
        Debug.Log($"unknown command: {CurrentCommand}");
    }
    private void Error(string value)
    {
        Debug.Log($"incorrect value: {value} for {CurrentCommand}");
    }

    public void ScenesButtonsEnabled(bool enabled)
    {
        if (enabled) ScenesButtons.SetActive(true);
        else ScenesButtons.SetActive(false);
    }
    public void SceneLoadCheat(string scene)
    {
        Debug.Log(" load scene: " + scene);
        SceneController.Instance.LoadScene(scene);
    }
    public void InputFieldEnabled(bool enabled)
    {
        if (enabled) inputField.SetActive(true);
        else inputField.SetActive(false);
    }
}
