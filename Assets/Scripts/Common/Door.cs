using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform player; 
    public bool isLocked = false;   
    private Collider2D doorCollider;

    private void Start()
    {
        doorCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (player != null && (transform.position - player.position).magnitude < 1.0f)
        {
            Debug.Log("Nhấn 'E' để mở cửa");

            if (Input.GetKeyDown(KeyCode.E))
            {
                TryOpenDoor();
            }
        }
    }

    void TryOpenDoor()
    {
        OpenDoor();
    }

    void OpenDoor()
    {
        gameObject.SetActive(false);
        Debug.Log("🚪 Cửa đã mở, Player có thể đi qua!");
    }
}
