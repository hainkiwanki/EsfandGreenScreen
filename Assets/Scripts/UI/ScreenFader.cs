using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class ScreenFader : Singleton<ScreenFader>
{
    private string prefString = "shouldFade";
    private Image blackscreenImage;

    private Image fader
    {
        get
        {
            if (blackscreenImage == null)
                CreateBlackScreenObject();
            return blackscreenImage;
        }
    }

    private void Awake()
    {
        CreateBlackScreenObject();
    }

    private void CreateBlackScreenObject()
    {
        GameObject blackImgObj = new GameObject("BlackScreen", typeof(RectTransform), typeof(Image));
        blackImgObj.transform.parent = GameSettings.Inst.mainCanvas;
        RectTransform rectTransform = blackImgObj.GetComponent<RectTransform>();
        rectTransform.anchorMin = rectTransform.sizeDelta = Vector2.zero;
        rectTransform.anchorMax = Vector2.one;
        rectTransform.pivot = new Vector2(0.5f, 0.5f);
        rectTransform.localScale = Vector3.one;
        rectTransform.anchoredPosition = rectTransform.localPosition = Vector3.zero;
        blackscreenImage = blackImgObj.GetComponent<Image>();
        blackscreenImage.color = Color.black;
        blackscreenImage.raycastTarget = false;
    }

    public void FadeIn(float _time = 3.0f, TweenCallback _onComplete = null)
    {
        fader.DOFade(1.0f, 0.0f);
        fader.DOFade(0.0f, _time).SetEase(Ease.InQuart).OnComplete(_onComplete);
    }

    public void FadeOut(float _time = 3.0f, TweenCallback _onComplete = null)
    {
        fader.DOFade(1.0f, _time).SetEase(Ease.OutQuart).OnComplete(_onComplete);
    }

    public void Force(float _v)
    {
        fader.DOFade(_v, 0.0f);
    }
}
