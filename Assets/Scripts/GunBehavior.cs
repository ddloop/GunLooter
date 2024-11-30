using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class GunBehavior : MonoBehaviour
{
    int type = 0;

    bool follow = false;
    private Transform transformToFollow;
    private Rigidbody2D rb;

    public int Type { get { return type; } set { type = value; } }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void StartFollowing(bool start, Transform toFollow = null)
    {
        if (start)
        {
            follow = true;
            transformToFollow = toFollow;
            StartCoroutine("Follow");
        }
        else
        {
            follow = false;
        }
    }

    IEnumerator Follow() 
    {     
        while(follow)
        {
            rb.velocity += ((Vector2)(transformToFollow.position - transform.position)).normalized * ((Vector2)(transformToFollow.position - transform.position)).magnitude * 120 * Time.deltaTime;
            yield return 0;
        }
    } 
}
