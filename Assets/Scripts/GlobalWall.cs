using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalWall : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
        GameRules.currentSoulsCount--;
    }
}
