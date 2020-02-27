using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Random = System.Random;

public class BubbleShooter : MonoBehaviour
{
    public GameObject currentBubble;
    public GameObject bubblePrefab;
    public Transform shootPosition;
    public Transform bubbleStartPosi;
    public GameObject predictionBubble;
    public Vector3 originalScale;
    public BubbleSO[] possiblePool;
    private IBubble _currentSelection;
    private IBubble _previousSelection;
    private Vector2Int _targetPosi;
    private Vector2Int _previousTargetPosi;

    private void Awake()
    {
        ReplaceBubble();
    }

    public void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 posi = shootPosition.position;
            RaycastHit2D hit2D = Physics2D.Raycast(posi,  new Vector2(Input.mousePosition.x, Input.mousePosition.y) - (new Vector2(posi.x, posi.y)), Mathf.Infinity);
            if (hit2D)
            {
                Debug.Log(hit2D.transform.name);
                _currentSelection=  hit2D.transform.GetComponent<IBubble>();
                _targetPosi = _currentSelection.GetNearestAvailableNeighbour(hit2D.point);
                ShootBubble();
            }
        }
        else if (Input.touchCount > 0)
        {
            Vector2 posi = shootPosition.position;
            RaycastHit2D hit2D = Physics2D.Raycast(posi,  new Vector2(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y) - (new Vector2(posi.x, posi.y)), Mathf.Infinity);
            if (hit2D)
            {
                Debug.Log(hit2D.transform.name);
                _currentSelection = hit2D.transform.GetComponent<IBubble>();
                _targetPosi = _currentSelection.GetNearestAvailableNeighbour(hit2D.point);
            }
        }

        if (_previousTargetPosi != _targetPosi)
        {
            //DisplayPrediction(_targetPosi);
        }
        _previousTargetPosi = _targetPosi;
    }

    public void ShootBubble()
    {
        currentBubble.transform.position = new Vector3(_targetPosi.x, _targetPosi.y, 0);
        BubbleManager.Instance.SetNode(_targetPosi.x, _targetPosi.y, currentBubble.GetComponent<IBubble>());

        currentBubble.transform.DOMove(new Vector3(_targetPosi.x, _targetPosi.y, 0), 1f);
        Invoke(nameof(CheckWithDelay), 1f);
        
    }

    public void CheckWithDelay()
    {
        currentBubble.GetComponent<IBubble>().StartMerge();
        ReplaceBubble();
    }

    public void ReplaceBubble()
    {
        currentBubble = Instantiate(bubblePrefab, bubbleStartPosi.position, Quaternion.identity);
        currentBubble.GetComponent<IBubble>().Init(possiblePool[UnityEngine.Random.Range(0, possiblePool.Length)]);
    }

    private void DisplayPrediction(Vector2Int position)
    {
        var colour = currentBubble.GetComponent<SpriteRenderer>().color;
        colour.a = .4f;
        predictionBubble.GetComponent<SpriteRenderer>().color = colour;
        predictionBubble.transform.localScale = Vector3.zero;
        transform.position = new Vector3(position.x, position.y, 0);
        transform.DOScale(originalScale, .3f).SetEase(Ease.InOutQuart);
    }
}
