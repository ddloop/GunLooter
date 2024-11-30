using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderZone : MonoBehaviour
{
    [SerializeField]
    private int gunID;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    public ObjectManager objectManager;

    public void Setup(int id ,Sprite sprite) 
    {
        gunID = id;
        spriteRenderer.sprite = sprite;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Gun") 
        {
            var gunBehav = collision.GetComponent<GunBehavior>();
            if (gunID == gunBehav.Type)
            {
                objectManager.RetrieveGun(gunBehav);
            }
        }
    }
}
