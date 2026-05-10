using System.Collections.Generic;
using UnityEngine;

public class LasersPool : MonoBehaviour
{
    [SerializeField] private GameObject m_LaserPrefab;
    [field: SerializeField] public float LaserSpeed { get; private set; } = 5f;
    
    private List<Laser> activeLasers = new ();
    private List<Laser> disabledLasers = new ();
    
    private void AddActiveLaser(Laser laser)
    {
        activeLasers.Add(laser);
    }

    public void SetSpeed(float speed)
    {
        LaserSpeed = speed;
    }
    
    public void DisableLaser(Laser laser)
    {
        activeLasers.Remove(laser);
        disabledLasers.Add(laser);
        laser.Disable();
    }
    
    public Laser GetLaser(Vector3 position, Quaternion rotation)
    {
        Laser laser;
        if (disabledLasers.Count > 0)
        {
            laser = disabledLasers[0];
            disabledLasers.RemoveAt(0);
            laser.transform.position = position;
            laser.transform.rotation = rotation;
            laser.Enable();
        }
        else
        {
            var laserObject = Instantiate(m_LaserPrefab, position, rotation);
            laser = laserObject.GetComponent<Laser>();
        }
        
        laser.SetSpeed(LaserSpeed);
        AddActiveLaser(laser);
        return laser;
    }
}
