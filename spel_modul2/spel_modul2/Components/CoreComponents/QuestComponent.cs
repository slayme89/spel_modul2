using GameEngine.Components;

namespace GameEngine
{
    public class QuestComponent : IComponent
    {
        public string Text { get; set; }
        public bool IsDone { get; set; }
        public bool IsStarted { get; set; }
        public int Experience { get; set; }
    }
}
