using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class PickCrumbMinigame : Minigame
{
    [Header("Minigame specific")]
    public Transform m_crumbLocationsParent;
    public Transform m_breadCrumbPrefab;
    private CircleCollider2D m_circleCollider;
    public Hand controller;

    public Dictionary<EGameDifficulty, int> numberOfCrumbsPerDifficulty = new Dictionary<EGameDifficulty, int>();
    public Dictionary<EGameDifficulty, int> amountCrumbsToSpawn = new Dictionary<EGameDifficulty, int>();
    private List<Transform> breadCrumbs = new List<Transform>();

    public AudioClip crumbPickUp, crumbRelease;
    public TextMeshProUGUI remainderText;
    private string m_remainderString = "Remove all bread crumbs from the face.\nRemaining: ";
    public RectTransform remainderContainer;
    private int m_totCrumbs;
    private int m_prvCount = 0;
    private float m_baseNumber;

    protected override void Init()
    {
        controller.onReleaseHeldObject += OnBreadCrumbRelease;
        controller.gameObject.SetActive(false);
        controller.onPickUp = crumbPickUp;
        controller.onRelease = crumbRelease;
        m_circleCollider = m_crumbLocationsParent.GetComponent<CircleCollider2D>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    private void OnDisable()
    {
        controller.onReleaseHeldObject -= OnBreadCrumbRelease;        
    }

    private void OnBreadCrumbRelease(Transform _breadCrumb)
    { 
        if(IsCrumbOutSideCircle(_breadCrumb.position))
        {
            var rigidBody = _breadCrumb.GetComponent<Rigidbody2D>();
            if(rigidBody == null)
                _breadCrumb.gameObject.AddComponent<Rigidbody2D>();
            if (breadCrumbs.Contains(_breadCrumb))
                breadCrumbs.Remove(_breadCrumb);
            remainderText.text = m_remainderString + breadCrumbs.Count;

            m_baseNumber = numberOfCrumbsPerDifficulty[EGameDifficulty.Even_Your_Nan_Can_Do_It];
            int p = GetPercentage();
            ProgressQuest(ref p);
        }
    }

    private int GetPercentage()
    {
        float currentPercentage = (((float)m_totCrumbs - (float)breadCrumbs.Count) / m_totCrumbs) * m_baseNumber;
        float totalPercentage = currentPercentage + m_prvCount;

        // Debug.Log(totalPercentage);
        return Mathf.FloorToInt(totalPercentage);
    }

    protected override void BeginMinigame()
    {
        controller.gameObject.SetActive(true);
        m_totCrumbs = numberOfCrumbsPerDifficulty[GameSettings.Inst.gameDifficulty];
        for (int i = 0; i < m_totCrumbs; i++)
        {
            var obj = Instantiate(m_breadCrumbPrefab);
            obj.transform.position = m_circleCollider.GetRandomPointInCircle();
            obj.transform.rotation = Extensions.GetRandomZRotation();
            obj.transform.parent = transform;
            breadCrumbs.Add(obj.transform);
        }
        remainderText.text = m_remainderString + breadCrumbs.Count;
        remainderContainer.DOScale(1.0f, 0.5f).SetEase(Ease.OutQuint);
    }

    public void SpawnNewBreadCrumb()
    {
        var obj = Instantiate(m_breadCrumbPrefab);
        m_breadCrumbPrefab.position = m_circleCollider.transform.position;
        Vector3 point = m_circleCollider.GetRandomPointOnEdge();
        breadCrumbs.Add(obj);
        float t = 0.35f;
        obj.DORotate(Extensions.GetRandomVectorZRotation(), t).SetEase(Ease.OutQuint);
        obj.DOMove(point, t).SetEase(Ease.OutQuint);
        remainderText.text = m_remainderString + breadCrumbs.Count;
    }

    public void SpawnBreadCrumbs(float _interval)
    {
        m_prvCount = GetPercentage();
        StartCoroutine(SpawnBreadCrumbs_co(amountCrumbsToSpawn[GameSettings.Inst.gameDifficulty], _interval));
    }

    private IEnumerator SpawnBreadCrumbs_co(int _amt, float _interval)
    {
        int amt = 0;
        while(amt < _amt)
        {
            SpawnNewBreadCrumb();
            amt++;
            yield return new WaitForSeconds(_interval);
        }

        m_baseNumber = _amt;
        m_totCrumbs = breadCrumbs.Count;
    }

    public bool IsCrumbOutSideCircle(Vector3 _pos)
    {
        float distance = Vector3.Distance(_pos, m_circleCollider.transform.position);
        return (distance > m_circleCollider.radius);
    }

    protected override void EndMinigame()
    {
        controller.gameObject.SetActive(false);
        gameObject.SetActive(false);

    }
}
