using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Redcode.Pools;
using System;
using Unity.VisualScripting;

public class ObjectManager : MonoBehaviour
{
    public GunBehavior gunPrefab;
    public Transform gunPoolTransform;

    [SerializeField] private List<Sprite> gunSprites;
    [SerializeField] private int arrayLimit = 4;

    private Pool<GunBehavior> pool;
    
    void Start()
    {
        pool = Pool.Create(gunPrefab, 100, gunPoolTransform);
        SpawnRandomGuns(10);
    }

    private void SpawnRandomGuns(int howManyItems)
    {
        for (int i = 0; i < howManyItems; i++) 
        {
            GunBehavior currentItem = pool.Get();
            int randomNumbah = UnityEngine.Random.Range(0, arrayLimit);
            currentItem.Type = randomNumbah;
            currentItem.GetComponent<SpriteRenderer>().sprite = gunSprites[randomNumbah];
            currentItem.GetComponent<BoxCollider2D>().size = currentItem.GetComponent<SpriteRenderer>().bounds.size;
            currentItem.transform.position = new Vector3(UnityEngine.Random.Range(-3.5f,3.5f), UnityEngine.Random.Range(-3.5f, 3.5f), 0);
            currentItem.transform.Rotate(new Vector3(0,0,UnityEngine.Random.Range(0,359)));
        }
    }

    public void RetrieveGun(GunBehavior gunBehavior) 
    {
        pool.Take(gunBehavior);
    }

    public Sprite GetSprite(int index) 
    {
        return gunSprites[index];
    }

    public int HowManyGuns() 
    {
        return gunPoolTransform.GetComponentsInChildren<GunBehavior>().Length;
    }
}
