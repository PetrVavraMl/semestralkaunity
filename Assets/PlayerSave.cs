using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerSave
{
    // Start is called before the first frame update
    public int healthPoints;
    public int score;
    public float[] position;
    public int level;
    public PlayerSave(MovementScript ms)
    {
        healthPoints = ms.healthPoints;
        score = ms.score;
        position = new float[3];
        position[0] = ms.transform.position.x;
        position[1] = ms.transform.position.y;
        position[2] = ms.transform.position.z;
        level = ms.level;
    }
}
