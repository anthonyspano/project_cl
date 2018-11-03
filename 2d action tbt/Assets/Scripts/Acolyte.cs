using System;

public class Acolyte : Enemy   // TBI
{
    public float speed;

    
    protected override void Move(float spd)
    {
        base.Move(speed);
    }

    private void Update()
    {
        Move(speed);
    }

    //private float wait;

    //public Acolyte(float waittime)
    //{
    //    wait = waittime;
    //}
    //public float GetWaitTime()
    //{
    //    return wait;
    //}
}