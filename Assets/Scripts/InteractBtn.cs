using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractBtn : MonoBehaviour
{
    private Button btn;

    private void Awake()
    {
        btn = GetComponent<Button>();
    }

    public void SetUp(IInteractable interactable, Player player)
    {
        btn.onClick.RemoveAllListeners();
        btn.GetComponentInChildren<TextMeshProUGUI>().text = interactable.InteractName();
        btn.onClick.AddListener(() =>
        {
            player.PlayInteractAnimation();
            interactable.Interact();
            gameObject.SetActive(false);
        });
    }
}
