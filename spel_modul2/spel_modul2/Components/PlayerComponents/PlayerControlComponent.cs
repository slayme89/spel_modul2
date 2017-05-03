using Microsoft.Xna.Framework;

namespace GameEngine
{
    class PlayerControlComponent : IComponent
    {
        public string ControllerType { get; set; }
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

        public PlayerControlComponent(string controller)
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

    class Button
    {
        private bool isDown;
        public Button ()
        {
            isDown = false;
        }

        public bool IsButtonDown()
        {
            return isDown;
        }

        public bool IsButtonUp()
        {
            return !isDown;
        }

        public void SetButton(bool isDown)
        {
            this.isDown = isDown;
        }

    }

    class Stick
    {
        private Vector2 stickDirection;

        public Stick()
        {
            stickDirection = new Vector2();
        }

        public void SetDirection(Vector2 newDirection)
        {
            stickDirection = newDirection;
        }

        public Vector2 GetDirection()
        {
            return stickDirection;
        }
    }
}
