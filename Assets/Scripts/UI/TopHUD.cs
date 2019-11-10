using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class TopHUDCfg
{
    [SerializeField] public bool showTopHUDTurnCount = true;
    [SerializeField] public bool showTopHUDGoalCount = false;
    [SerializeField] public bool showTopHUDTimer = false;
}

public class TopHUD : MonoBehaviour
{
    [SerializeField] private Animation m_anim;
    [SerializeField] private Button m_buttonRestart;
    [SerializeField] private Text m_title;
    [SerializeField] private Text m_turnCount;
    [SerializeField] private Text m_timer;
    [SerializeField] private Text m_goalCount;
    [SerializeField] private string m_animNameAppear;
    [SerializeField] private string m_animNameDisappear;

    private TopHUDCfg cfg;

    void Start()
    {
        var manager = ServiceLocator.Get<SceneListManager>();
        if (!manager)
        {
            Debug.LogWarning("[TopHUD][OnEnable] can`t find SceneListManager");
            return;
        }

        cfg = manager.GetCurrentSceneTopHUDCfg();
        InitUI(manager.GetCurrentSceneTitle(), manager.GetCurrentSceneRules());
        ApplyCfg();

        EventBus.Instance.AddListener<GameEvents.UpdateUI>(OnUpdateUI);
        EventBus.Instance.AddListener<GameEvents.GameEnded>(OnGameEnded);
        m_buttonRestart.onClick.AddListener(delegate { Restart(); });
        m_anim.Play(m_animNameAppear);
    }

    private void OnDisable()
    {
        EventBus.Instance.RemoveListener<GameEvents.UpdateUI>(OnUpdateUI);
        EventBus.Instance.RemoveListener<GameEvents.GameEnded>(OnGameEnded);
    }

    public void Restart()
    {
        ServiceLocator.Get<UnityPrototype.RestartManager>().RestartScene();
    }

    void ApplyCfg()
    {
        m_goalCount.gameObject.transform.parent.gameObject.SetActive(cfg.showTopHUDGoalCount);
        m_turnCount.gameObject.transform.parent.gameObject.SetActive(cfg.showTopHUDTurnCount);
        m_timer.gameObject.transform.parent.gameObject.SetActive(cfg.showTopHUDTimer);
    }

    void InitUI(string title, List<GameRule> rules)
    {
        m_title.text = title;

        foreach (var rule in rules)
        {
            switch (rule.type)
            {
                case RuleType.TurnCount:
                    SetTurnCount(rule.value);
                    break;
                case RuleType.GoalCount:
                    SetGoalCount(rule.value);
                    break;
            }
        }
        SetTimer(0);
    }

    void OnUpdateUI(GameEvents.UpdateUI e)
    {
        if (e.turnCount != -1)
        {
            SetTurnCount(e.turnCount);
        }
        if (e.goalCount != -1)
        {
            SetGoalCount(e.goalCount);
        }
        if (e.timer != -1)
        {
            SetTimer(e.timer);
        }
    }

    void OnGameEnded(GameEvents.GameEnded e)
    {
        // gameObject.SetActive(false);
        m_anim.Play(m_animNameDisappear);
        StartCoroutine("OnCompleteDisappearAnimation");
    }

    IEnumerator OnCompleteDisappearAnimation()
    {
        while (m_anim.IsPlaying(m_animNameDisappear))
            yield return null;

        gameObject.SetActive(false);
    }

    public void SetGoalCount(int count)
    {
        if (m_goalCount.IsActive())
        {
            m_goalCount.text = count.ToString();
        }
    }

    public void SetTurnCount(int count)
    {
        if (m_turnCount.IsActive())
        {
            m_turnCount.text = count.ToString() + " left";
        }
    }

    public void SetTimer(int count)
    {
        if (m_timer.IsActive())
        {
            m_timer.text = count.ToString() + " sec";
        }
    }

}
