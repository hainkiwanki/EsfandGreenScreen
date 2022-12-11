using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[NodeTint("#63D471")]
public class RewardNode : BaseNode
{
    public RectTransform rewardToGive;
    private RewardAnimation rewardAnimPrefab;

    private bool m_executed = false;

    protected override void Init()
    {
        base.Init();
        type = ENodeType.Reward;
    }

    public override void Enter()
    {
        rewardAnimPrefab = Resources.Load<RewardAnimation>("Prefabs/RewardAnimation");
    }

    public override void Execute()
    {
        if (m_executed) return;
        m_executed = true;

        var r = Instantiate(rewardAnimPrefab, GameSettings.Inst.mainCanvas);
        r.SetReward(Instantiate(rewardToGive));
        r.onRewardClose += () =>
        {
            isdone = true;
        };
    }

    public override void Exit()
    {
    }
}
