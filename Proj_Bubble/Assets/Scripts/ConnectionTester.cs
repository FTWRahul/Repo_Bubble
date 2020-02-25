using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionTester : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                if (hit.collider.CompareTag("Bubble"))
                {
                    hit.collider.GetComponent<IBubble>().GetNeighbour();
                }
            }
        }
    }
}
