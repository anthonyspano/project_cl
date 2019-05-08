using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Contains decision checkpoints
/* Override abstract functions
 * 
 */

public abstract class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected abstract void SecondPhase();

    protected abstract void ThirdPhase();

    protected abstract void FourthPhase();


}
