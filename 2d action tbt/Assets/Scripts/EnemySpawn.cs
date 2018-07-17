using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : Enemy {

    private void Awake()
    {
        Enemy<Acolyte> badguy = new Enemy<Acolyte>("badguy");
        badguy.ScriptComponent.Initialize(

            position: new Vector2(0, 0)

            );
    }

    protected override void Patrol()
    {

    }
}
