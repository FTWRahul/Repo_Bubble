﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Random = UnityEngine.Random;

public class BubbleTweener : MonoBehaviour
{

    private Vector3 origianlScale;
    private Color originalColour;
    private void Start()
    {
        StartingGrowthTween();
    }

    private void StartingGrowthTween()
    {
        float randomGrowthDelay = Random.Range(.1f, .5f);
        origianlScale = transform.localScale;
        var colour = GetComponent<SpriteRenderer>().color;
        colour.a = 1;
        originalColour = colour;
        transform.localScale = Vector3.zero;
        GetComponent<SpriteRenderer>().DOColor(colour, randomGrowthDelay).SetEase(Ease.Linear);
        transform.DOScale(origianlScale, randomGrowthDelay).SetEase(Ease.InOutQuart);
    }

    public void MergeIntoOther(Vector3 bubblePosition)
    {
        StartCoroutine(MergeAndPop(bubblePosition));
    }

    private IEnumerator MergeAndPop(Vector3 bubblePosition)
    {
        transform.DOMove(bubblePosition, .2f).SetEase(Ease.InQuart);
        var colour = GetComponent<SpriteRenderer>().color;
        colour.a = .5f;
        GetComponent<SpriteRenderer>().color = colour;
        yield return new WaitForSeconds(.2f);
        Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        AudioManager.instance.PlayPopSound();
    }
}
