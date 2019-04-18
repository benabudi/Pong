using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    #region Editor exposed members
    [SerializeField] private float _minVelocity = 5;
    #endregion

    #region Events
    public event Action<EndZone.EndZoneType> EnteredEndZone;
    #endregion

    
    public void GiveRandomVelocity()
    {
        GetComponent<Rigidbody>().velocity = _minVelocity * Vector3.right * (Random.Range(0, 2) * 2 - 1);
        GetComponent<Rigidbody>().velocity += _minVelocity * Vector3.up * (Random.Range(0, 2) * 2 - 1);
       
    }

    
    public void Reset()
    {
        transform.position = Vector3.zero;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (GetComponent<Rigidbody>().velocity.magnitude < _minVelocity)
        {
            float ratio = _minVelocity / GetComponent<Rigidbody>().velocity.magnitude;
            GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity * ratio;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        EndZone zone = collider.GetComponent<EndZone>();
        if (zone != null)
        {
            EnteredEndZone(zone.EndZoneSide);
        }
        
    }
}