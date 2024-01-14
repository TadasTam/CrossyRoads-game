using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CrossyRoad2D.Client.Singletons
{
    public class KeyboardState
    {
        private static KeyboardState _instance;
        public static KeyboardState Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new KeyboardState();
                }
                return _instance;
            }
        }

        private Dictionary<Key, KeyState> _keyStates = new Dictionary<Key, KeyState>();

        private KeyboardState() { }

        public bool IsKeyPressed(Key key)
        {
            return _keyStates.ContainsKey(key) &&
                (_keyStates[key] == KeyState.JustPressedBeforeFrame || _keyStates[key] == KeyState.Pressed);
        }

        public bool IsKeyJustPressed(Key key)
        {
            return _keyStates.ContainsKey(key) &&
                _keyStates[key] == KeyState.JustPressedBeforeFrame;
        }

        public bool IsKeyJustReleased(Key key)
        {
            return _keyStates.ContainsKey(key) &&
                _keyStates[key] == KeyState.JustReleasedBeforeFrame;
        }

        public void UpdateBeforeProcessing()
        {
            _keyStates = _keyStates.Select(keyValuePair =>
            {
                if (keyValuePair.Value == KeyState.JustPressed)
                {
                    return KeyValuePair.Create(keyValuePair.Key, KeyState.JustPressedBeforeFrame);
                }
                else if (keyValuePair.Value == KeyState.JustReleased)
                {
                    return KeyValuePair.Create(keyValuePair.Key, KeyState.JustReleasedBeforeFrame);
                }
                else
                {
                    return keyValuePair;
                }
            }).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }

        public void UpdateAfterProcessing()
        {
            _keyStates = _keyStates.Select(keyValuePair =>
            {
                if (keyValuePair.Value == KeyState.JustPressedBeforeFrame)
                {
                    return KeyValuePair.Create(keyValuePair.Key, KeyState.Pressed);
                }
                else if (keyValuePair.Value == KeyState.JustReleasedBeforeFrame)
                {
                    return KeyValuePair.Create(keyValuePair.Key, KeyState.NotPressed);
                }
                else
                {
                    return keyValuePair;
                }
            }).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }

        public void UpdateKeyDown(Key key)
        {
            _keyStates[key] = KeyState.JustPressed;
        }

        public void UpdateKeyUp(Key key)
        {
            _keyStates[key] = KeyState.JustReleased;
        }
    }

    public enum KeyState
    {
        NotPressed,
        JustPressed,
        JustPressedBeforeFrame,
        Pressed,
        JustReleased,
        JustReleasedBeforeFrame
    }
}
