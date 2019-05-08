using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCommand
{
    private float[] dir; // N S E W
    private bool[] keyPress;
    private byte size; // 0 - 255

    public AttackCommand () { dir = new float[4]; keyPress = new bool[3]; }
    public AttackCommand (byte size) { dir = new float[3]; keyPress = new bool[size]; }

    public void setKeyPress(int index, bool val)
    {
        keyPress[index] = val;
    }

    public bool getKeyPress(int index)
    {
        return keyPress[index];
    }
        
    

}
