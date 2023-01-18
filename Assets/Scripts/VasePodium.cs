using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VasePodium : MonoBehaviour
{
    public GameObject pointObject;

    public GameObject visitorOneWayPoint;
    public GameObject visitorTwoWayPoint;
    public GameObject visitorThreeWayPoint;

    public bool vaseIsBroken;
    public GameObject visitorOne;
    public GameObject visitorTwo;
    public GameObject visitorThree;

    public VasePosition pos1;
    public VasePosition pos2;
    public VasePosition pos3;

    public List<GameObject> vaseParts = new List<GameObject>();

    public float timer;
    public float repairSpeed;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void NewVisitor(GameObject soul)
    {
        SoulAI soulAI = soul.GetComponent<SoulAI>();
        if (visitorOne == null)
        {
            visitorOne = soul;
            pos1.visitor = soul;
            soulAI.currentTarget = visitorOneWayPoint;
            soulAI.MoveToNext();
        }
        else if (visitorTwo == null)
        {
            visitorTwo = soul;
            pos2.visitor = soul;
            soulAI.currentTarget = visitorTwoWayPoint;
            soulAI.MoveToNext();
        }
        else if (visitorThree == null)
        {
            visitorThree = soul;
            pos3.visitor = soul;
            soulAI.currentTarget = visitorThreeWayPoint;
            soulAI.MoveToNext();
        }
        else
        {
            soulAI.currentTarget = pointObject.GetComponent<Points>().ChooseNextTarget();
            soulAI.MoveToNext();
        }
    }

    public void LeftVase(SoulAI leavier)
    {
        if (leavier.gameObject == visitorOne)
        {
            visitorOne = null;
            pos1.visitor = null;
        }
        else if (leavier.gameObject == visitorTwo)
        {
            visitorTwo = null;
            pos2.visitor = null;
        }
        else
        {
            visitorThree = null;
            pos3.visitor = null;
        }

        leavier.currentTarget = pointObject.GetComponent<Points>().ChooseNextTarget();
        leavier.MoveToNext();
    }

    private void OnMouseDrag()
    {
        if (vaseIsBroken)
        {
            timer += Time.deltaTime * repairSpeed;
            for (int i = 0; i < vaseParts.Count; i++)
            {
                vaseParts[i].GetComponent<BoxCollider>().enabled = false;
                vaseParts[i].GetComponent<Rigidbody>().useGravity = false;
                vaseParts[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
                vaseParts[i].GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                Vector3 curPos = vaseParts[i].transform.localPosition;
                Vector3 curRot = vaseParts[i].transform.eulerAngles;
                float x = Mathf.Lerp(curPos.x, 0, timer);
                float y = Mathf.Lerp(curPos.y, 0, timer);
                float z = Mathf.Lerp(curPos.z, 0, timer);
                vaseParts[i].transform.localPosition = new Vector3(x, y, z);
                float a = Mathf.Lerp(curRot.x, 0, timer);
                float b = Mathf.Lerp(curRot.y, 0, timer);
                float c = Mathf.Lerp(curRot.z, 0, timer);
                vaseParts[i].transform.localEulerAngles = new Vector3(a, b, c);
            }
            if (timer > 1)
            {
                GameObject.Find("VaseAbsorbtion").GetComponent<AudioSource>().Play();
                vaseIsBroken = false;
                for (int i = 0; i < vaseParts.Count; i++)
                {
                    vaseParts[i].GetComponent<BoxCollider>().enabled = false;
                    vaseParts[i].GetComponent<Rigidbody>().useGravity = false;
                    vaseParts[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
                    vaseParts[i].GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                    vaseParts[i].transform.localPosition = Vector3.zero;
                    vaseParts[i].transform.localEulerAngles = Vector3.zero;
                }
                GameRules.currentVasesCount++;
                timer = 0;
            }
        }
    }

    private void OnMouseUp()
    {
        if (vaseIsBroken)
            for (int i = 0; i < vaseParts.Count; i++)
            {
                vaseParts[i].GetComponent<BoxCollider>().enabled = true;
                vaseParts[i].GetComponent<Rigidbody>().useGravity = true;
                timer = 0;
            }
    }
}

