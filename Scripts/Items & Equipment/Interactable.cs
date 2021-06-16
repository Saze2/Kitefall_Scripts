using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;

    protected bool isFocus = false;
    protected Transform player;

    protected bool hasInteracted = false;
    public Transform interactionTransform;

    //this method is ment to be overwritten
    public virtual void Interact()
    {
       //UnityEngine.Debug.Log("INTERACT with " + transform.name);
    }
    
    public virtual void InteractionRequirement()
    {
        if (isFocus && !hasInteracted)
        {
            float distance = Vector3.Distance(player.position, interactionTransform.position);
            if (distance <= radius)
            {
                Interact();
                hasInteracted = true;
            }
        }
    }

    void Update()
    {
        InteractionRequirement();

    }

    public void OnFocused (Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
        hasInteracted = false;
    }

    public void OnDeFocused()
    {
        isFocus = false;
        player = null;
        hasInteracted = false;
    }

    void OnDrawGizmosSelected()
    {
        if (interactionTransform == null)
                interactionTransform = transform;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }

}
