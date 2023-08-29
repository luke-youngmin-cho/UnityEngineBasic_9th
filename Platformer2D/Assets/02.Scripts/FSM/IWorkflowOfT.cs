using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWorkflow<T>
    where T : Enum
{
    T ID { get; }
    int Current { get; }
    T MoveNext();
    void Reset();
}
