using UnityEngine;

public class Laser : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Rigidbody m_rigidbody;
    
    [Header("Movement")]
    [SerializeField] private float m_speed = 5f;

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        var newPosition = m_rigidbody.position + transform.forward * (m_speed * Time.fixedDeltaTime);
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
}
