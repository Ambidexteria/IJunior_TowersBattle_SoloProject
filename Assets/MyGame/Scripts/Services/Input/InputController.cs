namespace Base.Services.Input
{
    public class InputController
    {
        private InputService _input;

        public InputController(InputService input)
        {
            _input = input;
        }

        public void Enable()
        {
            _input.Enable();
        }

        public void Disable()
        {
            _input.Disable();
        }
    }
}
