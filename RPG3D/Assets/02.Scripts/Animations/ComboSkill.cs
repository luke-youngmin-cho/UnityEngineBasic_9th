using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboSkill : Skill
{
    public int comboMax;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        controller.isComboAvailable = false;
        controller.comboMax = comboMax;
        controller.comboResetTimer = 0.0f;
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        controller.comboResetTimer = 0.25f;
    }
}
