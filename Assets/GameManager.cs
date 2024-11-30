using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    ObjectManager objectManager;

    //UI
    [SerializeField]
    Transform panel;
    [SerializeField]
    Button button;
    [SerializeField]
    TMPro.TextMeshProUGUI gameOverText;

    public TMPro.TextMeshProUGUI timerText;
    int timer = 20;

    void Start()
    {
        StartCoroutine("Countdown");
    }

    void Update()
    {
        CheckWinCondition();
    }

    public void Restart() 
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SceneManager.LoadScene(0);
    }

    public void CheckWinCondition() 
    {
        Debug.Log("Guns left:" + objectManager.HowManyGuns());
        if (objectManager.HowManyGuns() == 0)
        {
            Cursor.lockState = CursorLockMode.None;
            gameOverText.text = "You Won!";
            panel.gameObject.SetActive(true);
            Cursor.visible = true;
        }
    }

    IEnumerator Countdown() 
    {
        while (timer >= 0)
        {
            timerText.text = timer.ToString();
            timer--;
            yield return new WaitForSeconds(1);
        }

        if (objectManager.HowManyGuns() > 0)
        {
            panel.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
