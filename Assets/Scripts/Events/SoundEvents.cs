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
        ButtonClick,
    }

    public class SoundEvent : GameEvent
    {
        public SoundType type;
    }
}
