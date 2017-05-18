namespace GameEngine.Components
{
    public enum ControllerType { Keyboard, Gamepad1, Gamepad2 };

    public class PlayerControlComponent : IComponent
    {
        public ControllerType ControllerType { get; set; }
        public Stick Movement { get; set; }
        public Button Attack { get; set; }
        public Button Interact { get; set; }
        public Button Menu { get; set; }
        public Button Back { get; set; }
        public Button Inventory { get; set; }
        public Button ActionBar1 { get; set; }
        public Button ActionBar2 { get; set; }
        public Button ActionBar3 { get; set; }
        public Button ActionBar4 { get; set; }

        public PlayerControlComponent(ControllerType controller)
        {
            ControllerType = controller;
            Movement = new Stick();
            Attack = new Button();
            Interact = new Button();
            Menu = new Button();
            Back = new Button();
            Inventory = new Button();
            Back = new Button();
            ActionBar1 = new Button();
            ActionBar2 = new Button();
            ActionBar3 = new Button();
            ActionBar4 = new Button();
        }
    }
}
