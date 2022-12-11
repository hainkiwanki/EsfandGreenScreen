using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Events;

public abstract class ActionInterrupt : SerializedMonoBehaviour
{
    [HideInInspector]
    public UnityAction onCompleteCallback;

    public abstract void DoAction();
}
