namespace GameEngine
{
    class StateManager
    {
        static StateManager instance;
        string State;

        static StateManager()
        {
            instance = new StateManager();
        }

        public static StateManager GetInstance()
        {
            return instance;
        }

        private StateManager()
        {
            State = "";
        }

        public void SetState(string state)
        {
            State = state;
        }

        public string GetState()
        {
            return State;
        }
    }
}
