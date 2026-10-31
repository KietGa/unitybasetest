using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private float camSpeed = 2f;
    [SerializeField] private Button playBtn;
    [SerializeField] private Button quitBtn;
    [SerializeField] private GameObject loadingObject;

    private void Start()
    {
        playBtn.onClick.AddListener(PlayGame);
        quitBtn.onClick.AddListener(QuitGame);
    }

    private void PlayGame()
    {
        loadingObject.SetActive(true);
        Invoke(nameof(StartGame), 2f);
    }

    private void StartGame()
    {
        SceneManager.LoadScene("Gameplay");
    }

    private void QuitGame()
    {
        Application.Quit();
    }

    private void Update()
    {
        Camera.main.transform.Rotate(0, camSpeed * Time.deltaTime, 0);
    }
}
