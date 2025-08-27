namespace GameStateMachine
{
    public interface IStateSwitcher
    {
        public void SwitchState<T>() where T : IState;
    }
}