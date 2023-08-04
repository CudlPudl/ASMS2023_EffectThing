using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GoalAction
{
    [SerializeField] private AiAction action;
    [Tooltip("Uses base action defined in the creature if the action above is null.")]
    [SerializeField] private CreatureAiBaseAction baseAction = CreatureAiBaseAction.none;


    public AiAction Action => action;
    public CreatureAiBaseAction BaseAction => baseAction;


    public void SetAction(Creature creature)
    {
        if (action != null)
        {
            creature.Ai.ActivateAction(action);
            return;
        }
        creature.Ai.ActivateAction(baseAction);
    }
}
