using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SceneDescription
{
    [SerializeField] public string scene;
    [SerializeField] public string title = "Unknown title";
    [SerializeField] public List<GameRule> rules;
}

public class SceneDescriptionList : MonoBehaviour
{
    [SerializeField] private List<SceneDescription> m_scenes;

    public List<SceneDescription> GetDescription()
    {
        return m_scenes;
    }
}
