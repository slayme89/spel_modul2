namespace GameEngine
{
    class StateManager
    {
        static StateManager instance;
        private string State { get; set; }
     

        static StateManager()
        {
            instance = new StateManager();
        }

        public static StateManager GetInstance()
        {
            return instance;
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
