using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintShow : MonoBehaviour
{
    [SerializeField] private string m_hintKey = "";
    [SerializeField] private bool m_resetKey = false;

    private void Start()
    {
        if(PlayerPrefs.GetInt(m_hintKey, -1) != 1 || m_resetKey)
        {
            EventBus.Instance.Raise(new GameEvents.InputBlocked{on = true});
        }
        else
        {
            gameObject.SetActive(false);
        }

    }

    public void SkipHint()
    {
        PlayerPrefs.SetInt(m_hintKey, 1);
        gameObject.SetActive(false);
        EventBus.Instance.Raise(new GameEvents.InputBlocked{on = false});
    }
}
