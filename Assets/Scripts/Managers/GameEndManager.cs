﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEndManager : MonoBehaviour
{
    [SerializeField] private GameObject m_gameLoseShowPrefab;
    [SerializeField] private float m_nextSceneDelay = 0.0f;

    void Start()
    {
        EventBus.Instance.AddListener<GameEvents.GameEnded>(OnGameEnded);
    }

    private void OnDisable()
    {
        EventBus.Instance.RemoveListener<GameEvents.GameEnded>(OnGameEnded);
    }

    void OnGameEnded(GameEvents.GameEnded e)
    {
        StartCoroutine(ProcessGameEndDelayed(e));
    }

    private IEnumerator ProcessGameEndDelayed(GameEvents.GameEnded e)
    {
        yield return new WaitForSeconds(m_nextSceneDelay);
        ProcessGameEnd(e);
    }

    private void ProcessGameEnd(GameEvents.GameEnded e)
    {
        if (e.win)
        {
            // TODO add win efx on scene and go to next scene after it
            var manager = ServiceLocator.Get<SceneListManager>();
            if (!manager)
            {
                Debug.LogWarning("[GameEndManager][OnGameEnded] can`t find SceneListManager");
                return;
            }
            manager.LoadNextScene();
        }
        else
        {
            // TODO view lose show and restart scene
            var manager = ServiceLocator.Get<UnityPrototype.RestartManager>();
            if (!manager)
            {
                Debug.LogWarning("[GameEndManager][OnGameEnded] can`t find RestartManager");
                return;
            }
            manager.RestartScene();
        }
    }
}