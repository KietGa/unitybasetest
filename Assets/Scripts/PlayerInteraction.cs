
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float interactRange = 2f;
    [SerializeField] private LayerMask interactLayer;
    [SerializeField] private InteractBtn interactBtn;

    private readonly Collider[] results = new Collider[16];
    private IInteractable currentInteractable;
    private Player player;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        if (player.IsDeath) return;

        CheckInteractable();
    }

    private void CheckInteractable()
    {
        int count = Physics.OverlapSphereNonAlloc(
            transform.position,
            interactRange,
            results,
            interactLayer
        );

        IInteractable nearest = null;
        float nearestDistance = float.MaxValue;

        for (int i = 0; i < count; i++)
        {
            Collider col = results[i];

            IInteractable interactable =
                col.GetComponentInParent<IInteractable>();

            if (interactable == null)
                continue;

            float distance = Vector3.Distance(
                transform.position,
                 col.transform.position
            );

            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearest = interactable;
            }
        }

        if (currentInteractable != nearest)
        {
            currentInteractable = nearest;
            if (currentInteractable != null)
            {
                interactBtn.gameObject.SetActive(currentInteractable.CanInteract());
                interactBtn.SetUp(currentInteractable, player);
            }
            else
            {
                interactBtn.gameObject.SetActive(false);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactRange);
    }
}
