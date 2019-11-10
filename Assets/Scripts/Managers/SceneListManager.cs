using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneListManager : MonoBehaviour
{
    [SerializeField] private GameObject m_scenesDescriptionPrefab;
    [SerializeField] private bool m_resetLoadIndex = false;

    private static string SAVE_KEY = "lastSceneIndex";

    private List<SceneDescription> m_scenes;
    private int loadIndex = 0;

    private void OnEnable()
    {
        var description = Instantiate(m_scenesDescriptionPrefab);
        description.transform.SetParent(transform);
        m_scenes = description.GetComponent<SceneDescriptionList>().GetDescription();
    }

    private void Start()
    {
        loadIndex = m_resetLoadIndex ? 0 : PlayerPrefs.GetInt(SAVE_KEY, loadIndex);
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
        LoadScene(loadIndex);
    }

    public void LoadNextScene()
    {
        SaveCurrentSceneIndex();
        LoadScene(GetCurrentSceneIndex() + 1);
    }

    public void LoadPreviousScene()
    {
        LoadScene(GetCurrentSceneIndex() - 1);
    }

    private void SaveCurrentSceneIndex()
    {
        PlayerPrefs.SetInt(SAVE_KEY, GetCurrentSceneIndex());
    }

    private void LoadScene(int index)
    {
        SceneManager.LoadScene(m_scenes[index].scene);
    }

    private int GetCurrentSceneIndex()
    {
        return m_scenes.FindIndex(desc => desc.scene == SceneManager.GetActiveScene().name);
    }
}
