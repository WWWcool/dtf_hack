using UnityEngine;

namespace GameEvents
{
    public class ImpulseGiven : GameEvent { }

    public class InputStarted : GameEvent { }
    public class InputUpdated : GameEvent
    {
        public Vector2 impulse;
    }
    public class InputFinished : GameEvent
    {
        public Vector2 impulse;
    }

    public class InputBlocked : GameEvent
    {
        public bool on;
    }

    public class RuleTriggered : GameEvent
    {
        public RuleType type;
        public int value;
    }
    public class GameEnded : GameEvent
    {
        public bool win;
    }

    public class UpdateUI : GameEvent
    {
        public int goalCount = -1;
        public int turnCount = -1;
        public int timer = -1;
    }

    public class GameStarted : GameEvent { }
}
