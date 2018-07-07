using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {

    private HealthSystem healthSystem;
    public EnemyController eControl;

    public void Setup(HealthSystem healthSystem)
    {
        this.healthSystem = healthSystem;
        healthSystem.OnHealthChanged += HealthSystem_OnHealthChanged; // EventHandler
    }

    private void HealthSystem_OnHealthChanged(object sender, System.EventArgs e)
    {
        transform.Find("Bar").localScale = new Vector3(eControl.healthSystem.GetHealthPercent(), 1, 0); 

        Debug.Log("Health bar object: " + healthSystem.GetHealthPercent());
        //Debug.Log(transform.Find("Bar").localScale);
    }

    private void Update()
    {
        //transform.Find("Bar").localScale = new Vector3(healthSystem.GetHealthPercent(), 1);
    }
}
