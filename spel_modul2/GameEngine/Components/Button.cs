namespace GameEngine
{
    public class Button
    {
        private bool IsDown;

        public Button()
        {
            IsDown = false;
        }

        public bool IsButtonDown()
        {
            return IsDown;
        }

        public bool IsButtonUp()
        {
            return !IsDown;
        }

        public void SetButton(bool isDown)
        {
            IsDown = isDown;
        }

    }
}
