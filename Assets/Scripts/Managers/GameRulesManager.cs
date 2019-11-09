using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RuleType
{
    TurnCount,
    GoalCount,
}

public enum RuleCompleteType
{
    Lose,
    Win,
    Pending,
    None,
}

public enum RuleConditionType
{
    EqualOrMore,
    More,
    Less,
    NotEqual,
}

[System.Serializable]
public class GameRule
{
    [SerializeField] public RuleType type = RuleType.TurnCount;
    [SerializeField] public int value = 1;
    [SerializeField] public RuleCompleteType completeType = RuleCompleteType.Pending;
    [SerializeField] public RuleConditionType conditionType = RuleConditionType.EqualOrMore;

    public GameRule(GameRule rule){
        type = rule.type;
        conditionType = rule.conditionType;
        value = 0;
        completeType = RuleCompleteType.None;
    }
}

public class GameRulesManager : MonoBehaviour
{
    [SerializeField] private List<GameRule> m_rules;
    
    private List<GameRule> m_current_rules = new List<GameRule>();

    void Start()
    {
        EventBus.Instance.AddListener<GameEvents.RuleTriggered>(OnRuleTriggered);
        foreach(var rule in m_rules){
            
            m_current_rules.Add(new GameRule(rule));
        }
    }

    void OnRuleTriggered(GameEvents.RuleTriggered e)
    {
        var rule = m_current_rules.Find(r => r.type == e.type);
        var ruleDefault = m_rules.Find(r => r.type == e.type);

        rule.value += e.value;
        
        
        if(CheckCondition(rule, ruleDefault)){
            rule.completeType = ruleDefault.completeType;
            ProcessRuleChange();
        }
    }

    bool CheckCondition(GameRule current, GameRule def){
        bool res = false;
        switch(def.conditionType)
        {
            case RuleConditionType.EqualOrMore:
                res = current.value >= def.value;
            break;
            case RuleConditionType.More:
                res = current.value > def.value;
            break;
            case RuleConditionType.Less:
                res = current.value < def.value;
            break;
            case RuleConditionType.NotEqual:
                res = current.value != def.value;
            break;
        }
        return res;
    }

    void ProcessRuleChange(){
        bool allPending = true;
        foreach(var rule in m_current_rules)
        {
            switch(rule.completeType)
            {
                case RuleCompleteType.Lose:
                    RaiseGameEnd(false);
                return;
                case RuleCompleteType.Win:
                    RaiseGameEnd(true);
                return;
                case RuleCompleteType.None:
                    allPending = false;
                break;
            }
        }

        if(allPending){
            RaiseGameEnd(true);
        }
    }

    public void RaiseGameEnd(bool win){
        EventBus.Instance.Raise(new GameEvents.GameEnded{win = win});
    }
}
