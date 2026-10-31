using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private Vector3 targetWhenDown;
    [SerializeField] private float downSpeed = 3f;
    [SerializeField] private bool isWinDoor = false;
    private bool canInteracted = true;

    public void Interact()
    {
        canInteracted = false;
        if (isWinDoor) UIManager.Instance.SetWin();
    }

    private void Update()
    {
        if (canInteracted) return;
        if (transform.position == targetWhenDown) return;

        transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetWhenDown, downSpeed * Time.deltaTime);
    }

    public string InteractName() => "Open Door";
    public bool CanInteract() => canInteracted;
}
