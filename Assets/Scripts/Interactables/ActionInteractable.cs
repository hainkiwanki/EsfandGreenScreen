using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

public class ActionInteractable : Interactable
{
    public UnityEvent onUse;

    protected override void OnUse()
    {
        onUse?.Invoke();
        HideInteractionKey();
    }

    public void ProgressToNext()
    {
        if (useForSidequest)
        {
            ProgressSideQuest(ref m_amountUsed);
        }
    }

    public void DecreaseAmount()
    {
        m_amountUsed--;
    }
}
