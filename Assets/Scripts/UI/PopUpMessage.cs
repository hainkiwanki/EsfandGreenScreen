using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopUpMessage : MonoBehaviour
{
    [SerializeField]
    protected TextMeshProUGUI m_messageText;

    public string text
    {
        get
        {
            return m_messageText.text;
        }
        set
        {
            m_messageText.text = value;
        }
    }
}
