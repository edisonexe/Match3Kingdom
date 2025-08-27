using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
    public class InputReader : IDisposable
    {
        public event Action Click;
        private Inputs _inputs;
        private InputAction _positionAction;
        private InputAction _fireAction;

        private bool _isFire;

        public InputReader()
        {
            _inputs = new Inputs();
            _inputs.Player.Fire.performed += OnClick;
        }

        public void EnableInputs(bool value)
        {
            if (value)
                _inputs.Enable();
            else
                _inputs.Disable();
        }
        
        public void Dispose()
        {
            _inputs.Player.Fire.performed -= OnClick;
        }

        public Vector2 Position()
        {
            Vector2 pos = _inputs.Player.Select.ReadValue<Vector2>();
            return pos;
        }
        
        private void OnClick(InputAction.CallbackContext context)
        {
            Click?.Invoke();
        }
    }
}