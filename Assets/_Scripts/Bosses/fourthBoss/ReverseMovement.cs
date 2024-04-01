using UnityEngine;

public class ReverseMovement : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private bool isReversed = false;
    private bool isActive = false; 

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    public void ActivateReverse(float duration)
    {
        if (!isActive) 
        {
            isReversed = true;
            isActive = true; 
            playerMovement.movementSpeed *= -1; 
            Invoke(nameof(DeactivateReverse), duration);
        }
    }

    private void DeactivateReverse()
    {
        isReversed = false;
        playerMovement.movementSpeed *= -1; 
        isActive = false; 
    }
}
