using _GalaxySaber;
using UnityEngine;

public class LasersManager : Singleton<LasersManager>
{
    [SerializeField] private LasersPool _lasersPool;
    
    
    public void OnLaserMiss(Laser laser)
    {
        Debug.Log("Laser missed!");
        
        _lasersPool.DisableLaser(laser);
            
        EventManager.TriggerEvent(Consts.Events.Laser.Missed);
    }
    
    
    public void OnLaserHit(Laser laser)
    {
        Debug.Log("Laser hit by saber!");
        
        _lasersPool.DisableLaser(laser);
        
        EventManager.TriggerEvent(Consts.Events.Laser.Hit);
    }
}
