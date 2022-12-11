using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.Events;

[System.Serializable]
public class AnswerButton
{
    public Button button;
    public TextMeshProUGUI text;
    public GameObject go
    {
        get { return button.gameObject; }
    }

    public Button.ButtonClickedEvent onClick
    {
        get
        {
            return button.onClick;
        }
    }
}

[RequireComponent(typeof(TextMeshProUGUI))]
public class ChatBox : MonoBehaviour
{
    [SerializeField]
    private GameObject textObj;
    [SerializeField]
    private GameObject answerObj;
    [SerializeField]
    private TextMeshProUGUI questionText;

    public List<AnswerButton> buttons = new List<AnswerButton>();

    public TextMeshProUGUI textDisplay;
    public RectTransform rectTrans
    {
        get
        {
            if (m_rectTrans == null)
                m_rectTrans = gameObject.GetComponent<RectTransform>();
            return m_rectTrans;
        }
    }
    private RectTransform m_rectTrans;

    public string question
    {
        get { return questionText.text; }
        set { questionText.text = value; }
    }

    public string message
    {
        get
        {
            return textDisplay.text;
        }
        set
        {
            textDisplay.text = value;
        }
    }

    public void TurnToAnswer()
    {
        answerObj.SetActive(true);
        textObj.SetActive(false);
        for (int i = 0; i < buttons.Count; i++) 
        {
            buttons[i].go.SetActive(false);
        }
    }

    public void AddButton(string _text, UnityAction _onClick)
    {
        int index = -1;
        for (int i = 0; i < buttons.Count; i++)
        {
            if (!buttons[i].go.activeSelf)
            {
                index = i;
                break;
            }
        }

        if (index >= 0 && index < buttons.Count)
        {
            buttons[index].go.SetActive(true);
            buttons[index].text.text = _text;
            buttons[index].onClick.RemoveAllListeners();
            buttons[index].onClick.AddListener(_onClick);
        }
    }

    public void TurnToText()
    {
        textObj.SetActive(true);
        answerObj.SetActive(false);
        foreach (var b in buttons)
            b.button.gameObject.SetActive(false);
    }

    public void Show()
    {
        rectTrans.DOAnchorPosY(0.0f, 0.5f).SetEase(Ease.OutQuint);
    }

    public void InstantHide()
    {
        rectTrans.DOAnchorPosY(-300.0f, 0.0f);
    }

    public void Hide(float _delay = 0.0f)
    {
        rectTrans.DOAnchorPosY(-300.0f, 0.5f).SetEase(Ease.InQuint).SetDelay(_delay);
    }
}
