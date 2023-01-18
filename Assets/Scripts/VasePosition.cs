using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VasePosition : MonoBehaviour
{

    public GameObject visitor;
    public VasePodium vase;

    void Start()
    {
      
    }

    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == visitor)
        {
            SoulAI soul = other.gameObject.GetComponent<SoulAI>();
            soul.StopMoving();
            soul.currentAction = SoulAI.Actions.Vase;
            float ran = Random.Range(0f, 1f);
            if (ran < soul.sinChance)
                soul.doSomeSin = true;
            soul.timer = 0;
            soul.actionObject = vase.gameObject;
        }
    }
}
