using UnityEngine;

public class PCInputDevice : IInputDevice
{
    public float cursorPosition { get; private set; }

    public void UpdateInput()
    {
        UpdateLeftRightDelta();
    }

    private void UpdateLeftRightDelta()
    {
        if (Input.GetMouseButton(0))
            cursorPosition = (Input.mousePosition.x / Screen.width).Remap(0, 1, -1, 1);
    }
}
