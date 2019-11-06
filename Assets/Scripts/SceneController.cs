using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SceneController : MonoBehaviour
{
    private static SceneController instance = null;

    private bool sceneStarting = true;
    private bool sceneEnding = false;
    private Image fade;
    private Text textLoading;
    private float fadeSpeed = 1.5f;
    private string sceneName;

    public static SceneController Instance
    {
        get 
        {
            if (instance == null) Debug.LogError("SceneManager is Null");
            return instance;
        }
    }

    private void Start()
    {
        fade = GetComponentInChildren<Image>();
        textLoading = GetComponentInChildren<Text>();

        if (instance == null) instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        if (sceneStarting) StartScene();
        if (sceneEnding) EndScene();
    }

    private void StartScene()
    {
        fade.enabled = true;
        fade.color = Color.Lerp(fade.color, Color.clear, fadeSpeed * Time.deltaTime);

        if (fade.color.a <= 0.01f)
        {
            fade.color = Color.clear;
            fade.enabled = false;
            sceneStarting = false;
        }
    }
    private void EndScene()
    {
        sceneStarting = false;
        fade.enabled = true;
        fade.color = Color.Lerp(fade.color, Color.black, fadeSpeed * Time.deltaTime);
        if (fade.color.a >= 0.95f)
        {
            sceneEnding = false;
            fade.color = Color.black;
            StartCoroutine(LoadAsync(sceneName));
            textLoading.enabled = true;
        }
    }
    private IEnumerator LoadAsync(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        textLoading.enabled = false;
        sceneStarting = true;
    }

    public void LoadScene(string sceneName)
    {
        sceneEnding = true;
        CheatsSystem.Instance.IsActive = false;
        this.sceneName = sceneName;
    }
}
