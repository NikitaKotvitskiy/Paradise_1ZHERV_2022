using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points : MonoBehaviour
{
    public List<GameObject> nextPoints = new List<GameObject>();
    public enum PointType { Spawn, Empty, Vase };
    public PointType type;
    public GameObject specitalPoint;
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Soul entered");
            SoulAI soul = other.gameObject.GetComponent<SoulAI>();
            switch (type)
            {
                case PointType.Spawn:
                    soul.currentTarget = ChooseNextTarget();
                    soul.MoveToNext();
                    break;
                case PointType.Empty:
                    soul.currentTarget = ChooseNextTarget();
                    soul.MoveToNext();
                    break;
                case PointType.Vase:
                    VasePodium vp = specitalPoint.GetComponent<VasePodium>();
                    vp.NewVisitor(other.gameObject);
                    break;


            }
        }
    }

    public GameObject ChooseNextTarget()
    {
        int max = nextPoints.Count;
        int next = Random.Range(0, max);
        return nextPoints[next];
    }
}
