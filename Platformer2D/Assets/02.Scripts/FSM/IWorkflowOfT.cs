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
    void OnEnter();
    void OnExit();
    T MoveNext();
    void Reset();
}
