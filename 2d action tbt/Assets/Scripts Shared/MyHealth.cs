using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyHealth : MonoBehaviour
{
    HealthBar healthbar;
    public HealthSystem healthSystem;

    public int maxHealth;
    public float invulnTimer;

    // Start is called before the first frame update
    void Start()
    {
        healthbar = gameObject.GetComponentInChildren<HealthBar>();
        healthSystem = new HealthSystem(maxHealth, invulnTimer);
        healthbar.Setup(healthSystem);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
