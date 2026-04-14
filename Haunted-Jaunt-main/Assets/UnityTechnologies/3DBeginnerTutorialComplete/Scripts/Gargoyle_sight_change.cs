
using UnityEngine;


public class Gar_sight : MonoBehaviour
{
    public static float Lerp(float start, float end, float t)
    {
        if (t < 0) t = 0;
        if (t > 1) t = 1;
        return (1 - t) * start + t * end;
    }
    
    private float heightStart = 0.0f;
    private float heightEnd = 3.0f;

    private CapsuleCollider myCapsule;
    public float sight_float = 0.0f;
    public bool going_up = true;


    void Start()
    {
        myCapsule = GetComponent<CapsuleCollider>();
        
    }

    
    void Update() 
    {
        
        //float c_float = UnityEngine.Random.Range(0.0f, 1.0f);
        if (sight_float >= 1.0f)
        {
            going_up = false;
        }
        else if (sight_float <= 0.0f)
        {
            going_up = true;
        }

        if (going_up == true)
        {
            sight_float += Time.deltaTime /2;
        }
        else
        {
            sight_float -= Time.deltaTime /2;
        }

        myCapsule.height = Lerp(heightStart, heightEnd, sight_float);
        
    }
}