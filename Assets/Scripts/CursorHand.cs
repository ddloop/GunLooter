using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CursorHand : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;

    public Sprite point;
    public Sprite openHand;
    public Sprite closeHand;

    public Vector2 cursorPosition;
    public float sensitivity; 
    public Vector2 screenBounds;

    private int gunCollisionTracker = 0;
    private HandEnum handState = HandEnum.point;
    private Transform mousedOverTransformGun;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
        {
            // Hide the cursor and lock it to the center of the screen
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            // Show the cursor and unlock it when the application loses focus
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }


    void Update()
    {
        //Control Type
        HoldControl();

        // Get mouse movement
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        // Update virtual cursor position
        cursorPosition += new Vector2(mouseX, mouseY);

        // Clamp the virtual cursor within screen bounds (if necessary)
        cursorPosition.x = Mathf.Clamp(cursorPosition.x, -screenBounds.x, screenBounds.x);
        cursorPosition.y = Mathf.Clamp(cursorPosition.y, -screenBounds.y, screenBounds.y);

        // Change cursor image position
        transform.position = cursorPosition;
    }

    public void SwitchState(HandEnum handEnum) 
    {
        switch (handEnum)
        {
            case HandEnum.point:
                spriteRenderer.sprite = point;
                handState = handEnum;
                break;
            case HandEnum.grab:
                spriteRenderer.sprite = closeHand;
                handState = handEnum;
                break;
            case HandEnum.open:
                spriteRenderer.sprite = openHand;
                handState = handEnum;
                break;
        }
    }

    public enum HandEnum { point, open, grab }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Gun" && handState != HandEnum.grab) 
        {
            mousedOverTransformGun = collision.transform;
            ++gunCollisionTracker;
            SwitchState(HandEnum.open);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Gun" && handState != HandEnum.grab)
        {
            --gunCollisionTracker;
            if (gunCollisionTracker == 0)
            {
                mousedOverTransformGun = null;
                SwitchState(HandEnum.point);
            }
        }
    }

    private void ClickControl() 
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (handState == HandEnum.open && mousedOverTransformGun != null)
            {
                mousedOverTransformGun.GetComponent<GunBehavior>().StartFollowing(true, transform);
                SwitchState(HandEnum.grab);
            }
            else if (handState == HandEnum.grab && mousedOverTransformGun != null)
            {
                mousedOverTransformGun.GetComponent<GunBehavior>().StartFollowing(false);
                SwitchState(HandEnum.point);
            }
        }
    }

    private void HoldControl() 
    {
        if (Input.GetButtonDown("Fire1") && handState == HandEnum.open && mousedOverTransformGun != null)
        {
            mousedOverTransformGun.GetComponent<GunBehavior>().StartFollowing(true, transform);
            SwitchState(HandEnum.grab);
        }
        else if (Input.GetButtonUp("Fire1") && handState == HandEnum.grab)
        {
            if(mousedOverTransformGun != null) mousedOverTransformGun.GetComponent<GunBehavior>().StartFollowing(false);
            SwitchState(HandEnum.point);
        }
    }
}
