using UnityEngine;

public class Customer : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float waitTime = 3f;

    private Vector3 counterPosition = new Vector3(0, 1, 0);
    private Vector3 exitPosition = new Vector3(0, -4, 0);
    private ShopSlot shopSlot;
    private float waitTimer = 0f;

    enum CustomerState { WalkingIn, Waiting, WalkingOut }
    CustomerState state = CustomerState.WalkingIn;

    void Start()
    {
        shopSlot = FindAnyObjectByType<ShopSlot>();
    }

    void Update()
    {
        switch (state)
        {
            case CustomerState.WalkingIn:
                // Walk towards counter
                transform.position = Vector3.MoveTowards(
                    transform.position, counterPosition, moveSpeed * Time.deltaTime);

                // Arrived at counter
                if (Vector3.Distance(transform.position, counterPosition) < 0.1f)
                {
                    if (shopSlot != null && shopSlot.hasItem)
                    {
                        // Buy the item
                        state = CustomerState.Waiting;
                        waitTimer = waitTime;
                        Debug.Log("Customer is looking at " + shopSlot.itemName);
                    }
                    else
                    {
                        // Nothing to buy, leave
                        Debug.Log("Customer leaving - nothing for sale");
                        state = CustomerState.WalkingOut;
                    }
                }
                break;

            case CustomerState.Waiting:
                waitTimer -= Time.deltaTime;
                if (waitTimer <= 0)
                {
                    // Buy item and leave
                    if (shopSlot != null && shopSlot.hasItem)
                    {
                        shopSlot.SellItem();
                        Debug.Log("Customer bought item!");
                    }
                    state = CustomerState.WalkingOut;
                }
                break;

            case CustomerState.WalkingOut:
                // Walk to exit
                transform.position = Vector3.MoveTowards(
                    transform.position, exitPosition, moveSpeed * Time.deltaTime);

                // Reached exit - destroy
                if (Vector3.Distance(transform.position, exitPosition) < 0.1f)
                    Destroy(gameObject);
                break;
        }
    }
}