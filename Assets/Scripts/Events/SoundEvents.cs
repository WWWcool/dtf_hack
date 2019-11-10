namespace SoundEvents
{
    public enum SoundType
    {
        None,
        BallHitEnvironment,
        BallHitGoal,
        BallPassedThroughGoal,
        SpikesAppeared,
        LevelFailed,
        LevelCompleted,
    }

    public class SoundEvent : GameEvent
    {
        public SoundType type;
    }
}
