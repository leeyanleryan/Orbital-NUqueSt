using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits_UI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private GameObject button;
    [SerializeField] private TextMeshProUGUI credits;

    private int curr;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            curr++;
        }
        if (curr == 0)
        {
            text.text = "Thank you for playing our game!";
        }
        else if (curr == 1)
        {
            text.text = "We hope you had fun playing it.";
        }
        else if (curr == 2)
        {
            text.text = "If you are an incoming NUS undergraduate, welcome to NUS!";
        }
        else if (curr == 3)
        {
            text.text = "Hopefully this game was able to teach you a thing or two.";
        }
        else if (curr == 4)
        {
            text.text = "That's all. Goodbye!";
        }
        else if (curr == 5)
        {
            text.gameObject.SetActive(false);
            button.SetActive(false);
            credits.gameObject.SetActive(true);
            credits.text = "Game Developers: Thaw Tun Zan, Lee Yan Le Ryan\n\nProject Advisor: Eugene Tang Kang Jie\n\n" +
            "Game Testers: Edwin Zheng Yuan Yi, Toh Li Yuan, Brannon Aw Xu Wei, Sean William Bulawan Villamin, Project Sage, " +
            "Anders Foong Zer Hong, Danial Hisham" +
            "\n\nPeer Evaluators: Unmei no Farfalla, PestControl, GrassToucher";
        }
        else if (curr == 6)
        {
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }
    }

    public void ContinueButton()
    {
        curr++;
    }
}
