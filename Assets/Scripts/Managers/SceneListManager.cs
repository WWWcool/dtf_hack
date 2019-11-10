using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneListManager : MonoBehaviour
{
    [SerializeField] private GameObject m_scenesDescriptionPrefab;
    [SerializeField] private bool m_resetLoadIndex = false;

    private static string SAVE_KEY = "lastSceneIndex";

    private List<SceneDescription> m_scenes;
    private int loadIndex = 0;
    private int currentLoadIndex = 0;

    private bool sceneStarting = true;
    private bool sceneEnding = false;
    private Image fade;
    [SerializeField]private float fadeSpeed = 5.5f;

    private void OnEnable()
    {
        var description = Instantiate(m_scenesDescriptionPrefab);
        description.transform.SetParent(transform);
        m_scenes = description.GetComponent<SceneDescriptionList>().GetDescription();
    }

    private void Start()
    {
        GameObject image = GameObject.Find("Fade");
        fade = image.GetComponent<Image>();
        loadIndex = m_resetLoadIndex ? 0 : PlayerPrefs.GetInt(SAVE_KEY, loadIndex);
        currentLoadIndex = SceneManager.GetActiveScene().buildIndex;
        fade.enabled = false;
    }

    public string GetCurrentSceneTitle()
    {
        return m_scenes[GetCurrentSceneIndex()].title;
    }

    public List<GameRule> GetCurrentSceneRules()
    {
        return m_scenes[GetCurrentSceneIndex()].rules;
    }

    public TopHUDCfg GetCurrentSceneTopHUDCfg()
    {
        return m_scenes[GetCurrentSceneIndex()].topHUDCfg;
    }

    public void LoadLastSavedScene()
    {
        currentLoadIndex = loadIndex;
        sceneEnding = true;
    }

    public void LoadNextScene()
    {
        currentLoadIndex++;
        SaveCurrentSceneIndex();
        sceneEnding = true;
    }

    public void LoadPreviousScene()
    {
        currentLoadIndex--;
        sceneEnding = true;
    }

    private void SaveCurrentSceneIndex()
    {
        PlayerPrefs.SetInt(SAVE_KEY, currentLoadIndex);
    }

    private int GetCurrentSceneIndex()
    {
        return m_scenes.FindIndex(desc => desc.scene == SceneManager.GetActiveScene().name);
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
            SceneManager.LoadScene(currentLoadIndex);
        }
    }

}
