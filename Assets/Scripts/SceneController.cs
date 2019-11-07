using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SceneController : MonoBehaviour
{
    private static SceneController instance = null;
    [SerializeField]
    private GameObject loadingSlider;

    private bool sceneStarting = true;
    private bool sceneEnding = false;
    private Image fade;
    private Slider slider;
    private float fadeSpeed = 1.5f;
    private string sceneName;

    public static SceneController Instance
    {
        get 
        {
            if (instance == null) Debug.LogError("SceneController is Null");
            return instance;
        }
    }

    private void Start()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        fade = GetComponentInChildren<Image>();
        slider = loadingSlider.GetComponent<Slider>();
        fade.enabled = false;
        loadingSlider.SetActive(false);
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
            loadingSlider.SetActive(true);
        }
    }
    private IEnumerator LoadAsync(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
        {
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            slider.value = progress;
            yield return null;
        }
        loadingSlider.SetActive(false);
        sceneStarting = true;
    }

    public void LoadScene(string sceneName)
    {
        sceneEnding = true;
        CheatsSystem.Instance.IsActive = false;
        this.sceneName = sceneName;
    }
}
