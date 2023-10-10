using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Flags]
public enum AnimatorLayer
{
    None = 0 << 0,
    Base = 1 << 0,
    Top = 1 << 1,
}

public class BehaviourBase : StateMachineBehaviour
{
    private StateLayerMaskData _stateLayerMaskData;

    public virtual void Init(StateLayerMaskData stateLayerMaskData)
    {
        _stateLayerMaskData = stateLayerMaskData;
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        animator.SetBool($"dirty{(AnimatorLayer)(1 << layerIndex)}", false);
    }

    protected void ChangeState(Animator animator, State newState)
    {
        animator.SetInteger("state", (int)newState);

        int layerIndex = 0;
        foreach (AnimatorLayer layer in Enum.GetValues(typeof(AnimatorLayer)))
        {
            if (layer == AnimatorLayer.None)
                continue;

            if ((layer & _stateLayerMaskData.animatorLayerPairs[newState]) > 0)
            {
                animator.SetBool($"dirty{layer}", true);
                animator.SetLayerWeight(layerIndex, 1.0f);
            }
            else
            {
                animator.SetLayerWeight(layerIndex, 0.0f);
            }
            layerIndex++;
        } 
    }
}
