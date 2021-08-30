using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public static bool _isInputEnabled = true;

    public float cursorPosition
    {
        get
        {
            if (_isInputEnabled)
            {
                return Mathf.Clamp(_inputDevice.cursorPosition, -1, 1);
            }
            else
            {
                return 0;
            }
        }
    }

    private static IInputDevice _inputDevice;

    private void OnEnable()
    {
        if (_inputDevice == null)
        {
            switch (Application.platform)
            {
                case RuntimePlatform.WindowsEditor:
                    _inputDevice = new PCInputDevice();
                    break;

                case RuntimePlatform.Android:
                    _inputDevice = new PhoneInputDevice();
                    break;

                case RuntimePlatform.IPhonePlayer:
                    _inputDevice = new PhoneInputDevice();
                    break;
            }
        }
    }

    private void Update()
    {
        _inputDevice.UpdateInput();
    }
}