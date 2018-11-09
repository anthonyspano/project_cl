using System;

public class Acolyte : Enemy   // TBI
{
    private float cd;

    public float speed;

    private Acolyte()
    {
        speed = 6f;
        MoveSpeed = 2f;
        Wait = 2f;
        Cooldown = 2f;
        cd = 2f;
        AttackRate = 2f;
        NextAttack = AttackRate;
        AttackRange = 2f;
        SightRange = 6f;
        EHealth = 100f;

    }

    private void Update()
    {
        LocatePlayer();

        // Distance between enemy and player
        dX = transform.position.x - player.transform.position.x;
        dY = transform.position.y - player.transform.position.y;

        // bool coolDown > 0 
        if (attackReady(ref cd))
        {
            Delay(ref cd);
        }

        else
        {
            CanAttack = true;
        }

        // Primary walk trigger
        if (!IsAttacking && inSight(dX, dY))
            Walk();

    }

    protected override void Walk()
    {
        // if inRange or not inSight
        if (!inRange(dX, dY) && inSight(dX, dY))
            base.Walk();

        else    // trigger attack sequence (Update())
        {
            if (CanAttack)
            {
                IsAttacking = true;
                MoveToward(speed);
                
            }
        }

    }
    

    private bool inRange(float X, float Y)
    {
        if ((X * X + Y * Y) < AttackRange)
            return true;

        else
            return false;
    }

    private bool inSight(float X, float Y)
    {
        // if player distance is greater than sightRange, don't move
        if (X < SightRange && Y < SightRange) // 6 is a good number
            return true;
        else
            return false;
    }

    protected override void MoveToward(float speed)
    {
        // Acolyte only charges, so go fast
        base.MoveToward(speed);

        
        
        
    }


}