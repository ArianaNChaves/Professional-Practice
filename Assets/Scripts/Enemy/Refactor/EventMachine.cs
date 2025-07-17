using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EventMachine
{
    public enum EVENT
    {
        ENTER, UPDATE, EXIT
    }
    
    protected EVENT EventStage;
    //
    public EventMachine()//constructor
    {
        EventStage = EVENT.ENTER;
    }
    public virtual void Enter()
    {
        EventStage = EVENT.UPDATE;
    }
    public virtual void Update()
    {
        EventStage = EVENT.UPDATE;
    }
    public virtual void Exit()
    {
        EventStage = EVENT.EXIT;
    }
    public EventMachine Process()
    {
        if (EventStage == EVENT.ENTER)
        {
            Enter();
        }
        if (EventStage == EVENT.UPDATE)
        {
            Update();
        }
        if (EventStage == EVENT.EXIT)
        {
            Exit();
        }
        return this;
    }
}
