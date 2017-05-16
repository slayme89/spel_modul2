namespace GameEngine
{
    public delegate void ButtonAction();

    class MenuButtonComponent : IComponent
    {
        public bool IsActive { get; set; }
        public string Name { get; set; }
        public ButtonAction Use { get; set; }
    }
}
