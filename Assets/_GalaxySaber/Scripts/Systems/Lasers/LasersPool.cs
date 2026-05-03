using System.Collections.Generic;
using UnityEngine;

public class LasersPool : MonoBehaviour
{
    [SerializeField] private GameObject m_LaserPrefab;
    
    private List<Laser> _activeLasers = new ();
    private List<Laser> _disabledLasers = new ();
    
    private void AddActiveLaser(Laser laser)
    {
        _activeLasers.Add(laser);
    }
    
    public void DisableLaser(Laser laser)
    {
        _activeLasers.Remove(laser);
        _disabledLasers.Add(laser);
        laser.Disable();
    }
    
    public Laser GetLaser(Vector3 position, Quaternion rotation)
    {
        Laser laser;
        if (_disabledLasers.Count > 0)
        {
            laser = _disabledLasers[0];
            _disabledLasers.RemoveAt(0);
            laser.transform.position = position;
            laser.transform.rotation = rotation;
            laser.Enable();
        }
        else
        {
            var laserObject = Instantiate(m_LaserPrefab, position, rotation);
            laser = laserObject.GetComponent<Laser>();
        }
        
        AddActiveLaser(laser);
        return laser;
    }
}
