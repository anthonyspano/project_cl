using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxDebug : MonoBehaviour
{
    public float radius;
    public Vector3 position;


    // Start is called before the first frame update
    private void OnDrawGizmos()
    {
        //offset = transform.position;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(position, radius);
    }


}
