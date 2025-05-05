using UnityEngine;

[System.Serializable]
public class GhostRecorderData
{
    public Vector3 position;
    public Quaternion rotation;
    public float speed;
    public bool jump;
    public bool grounded;
    public bool flipX;
    public bool buttonPressed;

    public GhostRecorderData(Vector3 pos, Quaternion rot, float speed, bool jump, bool grounded, bool flipX, bool buttonPressed)
    {
        position = pos;
        rotation = rot;
        this.speed = speed;
        this.jump = jump;
        this.grounded = grounded;
        this.flipX = flipX;
        this.buttonPressed = buttonPressed;
    }
}
