using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    [SerializeField] private GameObject resultObject;
    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] private Button resetBtn;
    [SerializeField] private Button menuBtn;

    private void Awake()
    {
        Instance = this;
        resetBtn.onClick.AddListener(ResetGame);
        menuBtn.onClick.AddListener(MenuGame);
    }

    private void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void MenuGame()
    {
        SceneManager.LoadScene("Menu");
    }

    public void SetWin()
    {
        resultText.text = "YOU WIN!";
        resultText.color = Color.yellow;
        resultObject.SetActive(true);
    }

    public void SetLose()
    {
        resultText.text = "YOU ARE DEAD!";
        resultText.color = Color.red;
        resultObject.SetActive(true);
    }
}
