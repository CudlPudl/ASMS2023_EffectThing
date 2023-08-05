using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffAi : CreatureAI
{
    [SerializeField] private AiAction defaultOnSelectAction;

    public AiAction DefaultOnSelectAction => defaultOnSelectAction;
    public AiAction OnSelectAction { get; set; }

    protected override void Awake()
    {
        OnSelectAction = DefaultOnSelectAction;
        base.Awake();
    }


    // Select
    public void Select()
    {
        TargetType = CreatureAiTargetType.none;
        OnSelect();
    }
    protected void OnSelect() { ActivateAction(OnSelectAction); }



    public void MakeMad()
    {
        if (!IsMad)
        {
            IsMad = true;
            ObjectManager.instance.SpawnedStaffs.Remove(Creature as StaffCreature);
            ((StaffCreature)Creature).VisitorMagnet.gameObject.SetActive(false);
        }
    }
    public void FreeAi()
    {
    }

    public override void ActivateAction(CreatureAiBaseAction actionType)
    {
        switch (actionType)
        {
            case CreatureAiBaseAction.none:
                break;
            case CreatureAiBaseAction.defaultSpawnAction:
                ActivateAction(DefaultOnSpawnAction);
                break;
            case CreatureAiBaseAction.defaultSelectAction:
                ActivateAction(DefaultOnSelectAction);
                break;
            case CreatureAiBaseAction.defaultTargetSetAction:
                ActivateAction(DefaultOnTargetSetAction);
                break;
            case CreatureAiBaseAction.spawnAction:
                ActivateAction(OnSpawnAction);
                break;
            case CreatureAiBaseAction.selectAction:
                ActivateAction(OnSelectAction);
                break;
            case CreatureAiBaseAction.targetSetAction:
                ActivateAction(OnTargetSetAction);
                break;
            case CreatureAiBaseAction.defaultBoothActivityEnd:
                ActivateAction(DefaultOnSpawnAction);
                break;
            case CreatureAiBaseAction.boothActivityEnd:
                ActivateAction(OnSpawnAction);
                break;
        }
    }
}
