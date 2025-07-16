using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class EventMachine
{
    public enum EVENT
    {
        ENTER, UPDATE, EXIT
    }
    
    protected EVENT Stage;
    //
    public EventMachine()
    {
        Stage = EVENT.ENTER;
    }
    public virtual void Enter()
    {
        Stage = EVENT.UPDATE;
    }
    public virtual void Update()
    {
        Stage = EVENT.UPDATE;
    }
    public virtual void Exit()
    {
        Stage = EVENT.EXIT;
    }
    public EventMachine Process()
    {
        if (Stage == EVENT.ENTER)
        {
            Enter();
        }
        if (Stage == EVENT.UPDATE)
        {
            Update();
        }
        if (Stage == EVENT.EXIT)
        {
            Exit();
        }
        return this;
    }
}
