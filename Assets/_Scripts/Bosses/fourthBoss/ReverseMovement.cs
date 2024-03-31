using UnityEngine;

public class ReverseMovement : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private bool isReversed = false;
    private bool isActive = false; // Flag to indicate whether the effect is currently active

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    public void ActivateReverse(float duration)
    {
        if (!isActive) // Only activate if the effect is not already active
        {
            isReversed = true;
            isActive = true; // Set the flag to indicate that the effect is active
            playerMovement.movementSpeed *= -1; // Reverse the movement speed
            Invoke(nameof(DeactivateReverse), duration);
        }
    }

    private void DeactivateReverse()
    {
        isReversed = false;
        playerMovement.movementSpeed *= -1; // Reset the movement speed back to normal
        isActive = false; // Reset the flag to indicate that the effect is no longer active
    }
}
