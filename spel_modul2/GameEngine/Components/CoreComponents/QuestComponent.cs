namespace GameEngine.Components
{
    public class QuestComponent : IComponent
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public bool IsDone { get; set; }
        public bool IsActive { get; set; }
        public bool IsShowing { get; set; }
        public int Experience { get; set; }

        public QuestComponent(string name, string text, int experience)
        {
            Name = name;
            Text = text;
            Experience = experience;
            IsActive = false;
            IsDone = false;
        }

    }
}
