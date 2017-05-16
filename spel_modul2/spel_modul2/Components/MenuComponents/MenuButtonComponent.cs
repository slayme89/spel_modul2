namespace GameEngine
{
    public delegate void ButtonAction();

    class MenuButtonComponent : IComponent
    {
        public string Name { get; set; }
        public ButtonAction Use { get; set; }
    }
}
