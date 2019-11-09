using UnityEngine;

namespace GameEvents
{
    public class BallReachedGoal : GameEvent { }

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
}
