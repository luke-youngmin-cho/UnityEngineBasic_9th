using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWorkflow<T>
    where T : Enum
{
    T ID { get; }
    bool CanExecute { get; }
    int Current { get; }
    void OnEnter(object[] parameters);
    void OnExit();
    T OnUpdate();
    void OnFixedUpdate();
    void Reset();
}
