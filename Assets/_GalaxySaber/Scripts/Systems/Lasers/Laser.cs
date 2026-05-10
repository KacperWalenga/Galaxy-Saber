using UnityEngine;

public class Laser : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Rigidbody m_rigidbody;
    
    [Header("Movement")]
    [field: SerializeField]
    public float Speed { get; private set; } = 5f;
    

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        var newPosition = m_rigidbody.position + transform.forward * (Speed * Time.fixedDeltaTime);
        m_rigidbody.MovePosition(newPosition);
    }

    public void Enable()
    {
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

    public void OnHit()
    {
        LasersManager.Instance.OnLaserHit(this);
    }
    
    public void OnMissed()
    {
        LasersManager.Instance.OnLaserMiss(this);
    }

    public void SetSpeed(float speed)
    {
        Speed = speed;
    }
}
