using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulAI : MonoBehaviour
{
    public bool sinner;
    public float sinChance;
    public bool doSomeSin;
    public float timer;

    public GameObject currentTarget;

    private Rigidbody rb;
    private Animator anim;
    public float speed;
    public float waitBeforeSin;
    public float waitBeforeLeave;
    public float waitAfterSin;
    public bool sinned;

    public enum Actions { None, Vase };
    public Actions currentAction;
    public GameObject actionObject;

    public ParticleSystem parts;
    public AudioSource vaseBreakingSound;
    public AudioSource soulKilling;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        float ran = Random.Range(0f, 1f);
        Debug.Log(ran);
        if (ran < GameRules.SinnerChance)
            sinner = true;

        sinChance = Random.Range(GameRules.MinSinChance, GameRules.MaxSinChance);
        vaseBreakingSound = GameObject.Find("VaseBreaking").GetComponent<AudioSource>();
        soulKilling = GameObject.Find("SoulKilling").GetComponent<AudioSource>();
    }

    void Update()
    {
        switch (currentAction)
        {
            case Actions.Vase:
                timer += Time.deltaTime;
                if (sinned && timer >= waitAfterSin)
                {
                    actionObject.GetComponent<VasePodium>().LeftVase(this);
                    currentAction = Actions.None;
                    sinned = false;
                    timer = 0;
                }
                else if (sinner && doSomeSin && timer >= waitBeforeSin && !actionObject.GetComponent<VasePodium>().vaseIsBroken)
                {
                    DoSin();
                    timer = 0;
                }
                else if (timer >= waitBeforeLeave)
                {
                    actionObject.GetComponent<VasePodium>().LeftVase(this);
                    currentAction = Actions.None;
                    timer = 0;
                }
                break;
            default:
                break;
        }

    }

    public void MoveToNext()
    {
        Vector3 targetPos = currentTarget.transform.position;
        Vector3 soulPos = transform.position;
        Vector3 dir = (targetPos - soulPos).normalized;
        rb.velocity = dir * speed;
        transform.rotation = Quaternion.LookRotation(dir);
        anim.SetBool("Moving", true);
    }

    public void StopMoving()
    {
        rb.velocity = Vector3.zero;
        anim.SetBool("Moving", false);
    }

    public void DoSin()
    {
        switch (currentAction)
        {
            case Actions.Vase:
                anim.Play("Action");
                VasePodium vp = actionObject.GetComponent<VasePodium>();
                for (int i = 0; i < vp.vaseParts.Count; i++)
                {
                    vp.vaseParts[i].GetComponent<BoxCollider>().enabled = true;
                    vp.vaseParts[i].GetComponent<Rigidbody>().useGravity = true;
                }
                vp.vaseIsBroken = true;
                sinned = true;
                GameRules.currentVasesCount--;
                vaseBreakingSound.Play();
                break;
            default:
                break;
        }
    }

    public void Death()
    {
        parts.Play();
        soulKilling.Play();
        parts.gameObject.transform.SetParent(null);
        Destroy(gameObject);
        GameRules.currentSoulsCount--;
        if (sinner)
            GameRules.currentPoints += GameRules.AddPointsForSuccess;
        else
            GameRules.currentPoints -= GameRules.SubPointsForUnsuccess;
    }

    private void OnMouseDown()
    {
        Death();
    }
}
