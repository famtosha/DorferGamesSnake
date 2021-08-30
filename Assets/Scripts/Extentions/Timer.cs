using System;
using UnityEngine;

[Serializable]
public class Timer
{
    [SerializeField] private float _coolDown;
    private float cdLeft;

    public bool isReady => cdLeft < 0;
    public float left => cdLeft;

    public void UpdateTimer(float time)
    {
        cdLeft -= time;
    }

    public void Reset()
    {
        cdLeft = _coolDown;
    }

    public void End()
    {
        cdLeft = 0;
    }

    public Timer(float cd)
    {
        this._coolDown = cd;
    }
}