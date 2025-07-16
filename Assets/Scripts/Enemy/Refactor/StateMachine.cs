using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : EventMachine
{
    protected enum STATE {};
    protected EventMachine EventMachineParent;
    protected STATE CurrentState;
    protected Dictionary<STATE, StateMachine> StatesList = new Dictionary<STATE, StateMachine>();
}

