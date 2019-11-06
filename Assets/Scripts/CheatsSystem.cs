using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheatsSystem : MonoBehaviour
{
    private static CheatsSystem instance = null;
    private bool isActive;
    private GameObject inputField;
    private InputField input;
    string key, value;

    public bool IsActive
    {
        get { return isActive; }
        set 
        { 
            isActive = value;
            if (IsActive) inputField.SetActive(true);
            else inputField.SetActive(false);
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
        inputField = transform.GetChild(0).gameObject;
        input = inputField.GetComponent<InputField>();
        if (instance == null) instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.BackQuote) && !isActive)
        {
            inputField.SetActive(true);
            isActive = true;
        }else if (Input.GetKeyDown(KeyCode.BackQuote) && isActive)
        {
            inputField.SetActive(false);
            isActive = false;
        }

        Check();
    }

    private void Check()
    {
        if (!Input.GetKeyDown(KeyCode.Return) || !isActive) return;

        string[] tmp = input.text.Split(' ');

        if (tmp.Length == 0) return;
        else if(tmp.Length == 2)
        {
            key = tmp[0];
            value = tmp[1];
        }else if(tmp.Length > 2)
        {
            Error();
            return;
        }
        else
        {
            key = tmp[0];
            value = string.Empty;
        }

        GetCheat();
    }
    private void GetCheat()
    {
        switch (key)
        {
            case "loadScene":
                if (Application.CanStreamedLevelBeLoaded(value))
                {
                    Debug.Log(inputField + "load scene: " + value);
                    SceneController.Instance.LoadScene(value);
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
        Debug.Log("unknown command: " + input.text);
    }
    private void Error(string value)
    {
        Debug.Log("incorrect value: " + value);
    }
}
