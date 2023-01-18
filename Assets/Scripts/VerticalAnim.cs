using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalAnim : MonoBehaviour
{
    public bool work;
    public float normalY;
    public float amplitude;
    public float movingSpeed;
    private float t;
    private float downY;
    private float topY;
    private bool up = true;

    void Start()
    {
        gameObject.transform.position = new Vector3(transform.position.x, normalY - amplitude / 2, transform.position.z);
    }

    void Update()
    {
        downY = normalY - amplitude / 2;
        topY = normalY + amplitude / 2;

        t += Time.deltaTime * movingSpeed;
        float posY = Mathf.Lerp(up ? downY : topY, up ? topY : downY, t);
        transform.position = new Vector3(transform.position.x, posY, transform.position.z);
        if (posY == topY && up || posY == downY && !up)
        {
            up = !up;
            t = 0;
        }
    }
}
