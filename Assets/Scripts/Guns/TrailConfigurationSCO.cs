using UnityEngine;

[CreateAssetMenu(fileName = "Trail configuration", menuName = "Guns/Trail configuration", order = 4)]
public class TrailConfigurationSCO : ScriptableObject
{
    public Material material;
    public AnimationCurve widthCurve;
    public float time = 0.5f;
    public float minVertexDistance;
    public Gradient color;
    //Distance where we stop rendering
    public float missDistance;
    //Simulated speed of the bullet towards the hit location or the point where it missed
    public float simulatedSpeed;
    public GameObject bullethole;
}
