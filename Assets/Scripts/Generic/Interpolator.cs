using System.Collections;
using System.Collections.Generic;
using System;

public class Interpolator
{
    public enum State { MIN, MAX, SHRINKING, GROWING};
    public enum Type { LINEAR, SIN, COS, QUADRATIC, SMOOTH, SMOOTHER}

    private State m_interpState = State.MIN;
    private Type m_interpType = Type.SIN;

    private readonly float m_epsilon = 0.05f;
    private float m_currentTime = 0.0f;
    private float m_interpolationTIme = 0.0f;

    public float Value { get; private set; }
    public float Inverse { get { return 1 - Value; } }

    public bool IsMaxPrecise { get { return this.m_interpState == State.MAX; } }
    public bool IsMinPrecise { get { return this.m_interpState == State.MIN; } }

    public bool IsMax { get { return GetValue() > 1f - m_epsilon; } }
    public bool IsMin { get { return GetValue() < m_epsilon; } }

    public Interpolator(float interpolationTime, Type interpolationType = Type.LINEAR)
    {
        m_interpolationTIme = interpolationTime;
        m_interpType = interpolationType;
    }

    public void Update(float dt)
    {
        if (this.m_interpState == State.MIN || this.m_interpState == State.MAX)
            return;
        float modifiedDT = this.m_interpState == State.GROWING ? dt : -dt;

        m_currentTime += modifiedDT;

        if(m_currentTime >= m_interpolationTIme)
            ForceMax();
        else if(m_currentTime <= 0.0f)
            ForceMin();

        Value = m_currentTime / m_interpolationTIme;
        switch (m_interpType)
        {
            case Type.SIN:
                Value = (float)Math.Sin(Value * Math.PI * 0.5f);
                break;
            case Type.COS:
                Value = 1f - (float)Math.Cos(Value * Math.PI * 0.5f);
                break;
            case Type.QUADRATIC:
                Value *= Value;
                break;
            case Type.SMOOTH:
                Value = Value * Value * (3f - 2f * Value);
                break;
            case Type.SMOOTHER:
                Value = (float)Math.Pow(Value, 3) * (Value * (6f * Value - 15f) + 10f);
                break;
            case Type.LINEAR:

                break;
            
            default:
                break;
        }

    }

    public float GetValue()
    {
        return Value;
    }

    //Interpolation changers
    public void ToMax()
    {
        this.m_interpState = this.m_interpState != State.MAX ? State.GROWING : State.MAX;
    }

    public void ToMin()
    {
        this.m_interpState = this.m_interpState != State.MIN ? State.SHRINKING : State.MIN;
    }

    public void ForceMax()
    {
        if( this.m_interpState == State.MAX)
            return;
        this.m_currentTime = this.m_interpolationTIme;
        this.m_interpState = State.MAX;
    }

    public void ForceMin()
    {
        if (this.m_interpState == State.MIN)
            return;
        this.m_currentTime = 0.0f;
        this.m_interpState = State.MIN;
    }

}
