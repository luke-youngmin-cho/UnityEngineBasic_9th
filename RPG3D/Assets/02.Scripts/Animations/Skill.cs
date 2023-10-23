using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Skill : BehaviourBase
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        animator.SetBool($"exit{(AnimatorLayers)(1 << layerIndex)}", false);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        if (stateInfo.normalizedTime >= 1.0f)
        {
            ChangeState(animator, State.Move);
            animator.SetBool($"exit{(AnimatorLayers)(1 << layerIndex)}", true);
            Debug.Log($"Skill..{stateInfo.normalizedTime}");
        }    
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        animator.SetBool($"exit{(AnimatorLayers)(1 << layerIndex)}", false);
    }
}
