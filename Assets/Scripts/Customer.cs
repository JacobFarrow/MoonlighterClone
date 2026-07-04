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

        if (AudioManager.instance != null)
            AudioManager.instance.PlayBell();
    }

    void Update()
    {
        switch (state)
        {
            case CustomerState.WalkingIn:
                transform.position = Vector3.MoveTowards(
                    transform.position, counterPosition, moveSpeed * Time.deltaTime);
                if (Vector3.Distance(transform.position, counterPosition) < 0.1f)
                {
                    if (shopSlot != null && shopSlot.hasItem)
                    {
                        state = CustomerState.Waiting;
                        waitTimer = waitTime;
                    }
                    else
                    {
                        state = CustomerState.WalkingOut;
                    }
                }
                break;

            case CustomerState.Waiting:
                waitTimer -= Time.deltaTime;
                if (waitTimer <= 0)
                {
                    if (shopSlot != null && shopSlot.hasItem)
                        shopSlot.SellItem();
                    state = CustomerState.WalkingOut;
                }
                break;

            case CustomerState.WalkingOut:
                transform.position = Vector3.MoveTowards(
                    transform.position, exitPosition, moveSpeed * Time.deltaTime);
                if (Vector3.Distance(transform.position, exitPosition) < 0.1f)
                    Destroy(gameObject);
                break;
        }
    }
}