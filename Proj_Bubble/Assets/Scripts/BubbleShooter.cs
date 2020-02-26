using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BubbleShooter : MonoBehaviour
{
    public GameObject currentBubble;
    public BubbleSO[] possiblePool;

    public void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                RaycastHit2D hit2D = Physics2D.Raycast(currentBubble.transform.position, hit.point, Mathf.Infinity);
                if (hit2D.transform.GetComponent<IBubble>() != null)
                {
                    hit2D.transform.GetComponent<IBubble>().GetNearestAvailableNeighbour(hit2D.point);
                }
            }
            
        }
    }

    public void ShootBubble()
    {
        
    }
}
