using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldBtn : MonoBehaviour, IInteractable
{
    [SerializeField] private Platform[] platforms;
    private bool canInteracted = true;

    public void Interact()
    {
        foreach (var platform in platforms)
        {
            platform.StartExpand();
        }
    }

    public string InteractName() => "Press Button";
    public bool CanInteract() => canInteracted;
}
