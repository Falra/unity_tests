using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    [Header("Set Dynamically")]
    public Rigidbody rigid;
    private Neighborhood neighborhood;
    private void Awake()
    {
        neighborhood = GetComponent<Neighborhood>();
        rigid = GetComponent<Rigidbody>();
        pos = Random.insideUnitSphere * Spawner.S.spawnRadius;
        Vector3 vel = Random.onUnitSphere * Spawner.S.velocity;
        rigid.velocity = vel;

        LookAhead();

        Color randColor = Color.black;
        while (randColor.r + randColor.g + randColor.b < 1.0f)
        {
            randColor = new Color(Random.value, Random.value, Random.value);
        }

        Renderer[] rends = gameObject.GetComponentsInChildren<Renderer>();
        foreach (Renderer rend in rends)
        {
            rend.material.color = randColor;
        }

        TrailRenderer tRend = GetComponent<TrailRenderer>();
        tRend.material.SetColor("_TintColor", randColor);
    }
    void LookAhead()
    {
        transform.LookAt(pos + rigid.velocity);
    }

    public Vector3 pos
    {
        get { return transform.position; }
        set { transform.position = value; }
    }
    private void FixedUpdate()
    {
        Vector3 vel = rigid.velocity;
        Spawner spn = Spawner.S;

        // Avoid closest
        Vector3 velAvoid = Vector3.zero;
        Vector3 tooClosePos = neighborhood.avgClosePos;
        if (tooClosePos != Vector3.zero)
        {
            velAvoid = pos - tooClosePos;
            velAvoid.Normalize();
            velAvoid *= spn.velocity;
        }

        // avg velocity
        Vector3 velAllign = neighborhood.avgVel;
        if (velAllign != Vector3.zero)
        {
            velAllign.Normalize();
            velAllign *= spn.velocity;
        }

        // avg center
        Vector3 velCenter = neighborhood.avgPos;
        if (velCenter != Vector3.zero)
        {
            velCenter -= transform.position;
            velCenter.Normalize();
            velCenter *= spn.velocity;
        }

        Vector3 delta = Attractor.POS - pos;
        bool attracted = (delta.magnitude > spn.attractPushDist);
        Vector3 velAttract = delta.normalized * spn.velocity;

        float ftd = Time.fixedDeltaTime;

        if (velAvoid != Vector3.zero)
        {
            vel = Vector3.Lerp(vel, velAvoid, spn.collAvoid * ftd);
        }
        else
        {
            if (velAllign != Vector3.zero)
            {
                vel = Vector3.Lerp(vel, velAllign, spn.velMatching * ftd);
            }
            if (velCenter != Vector3.zero)
            {
                vel = Vector3.Lerp(vel, velAllign, spn.flockCentering * ftd);
            }
            if (velAttract != Vector3.zero)
            {
                if (attracted)
                {
                    vel = Vector3.Lerp(vel, velAttract, spn.attractPull * ftd);
                }
                else
                {
                    vel = Vector3.Lerp(vel, -velAttract, spn.attractPush * ftd);
                }
            }
        }

        vel = vel.normalized * spn.velocity;
        rigid.velocity = vel;

        LookAhead();
    }
}
