using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class ObjectEvent : UnityEvent<Collider> { }

public class OnTrigger : MonoBehaviour
{
    [SerializeField] private string m_tag;
    
    [Header("Events")]
    [SerializeField] private ObjectEvent m_onTriggerEnter;
    [SerializeField] private ObjectEvent m_onTriggerExit;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(m_tag) || m_tag == null)
        {
            m_onTriggerEnter?.Invoke(other);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(m_tag) || m_tag == null)
        {
            m_onTriggerExit?.Invoke(other);
        }
    }
    
}
