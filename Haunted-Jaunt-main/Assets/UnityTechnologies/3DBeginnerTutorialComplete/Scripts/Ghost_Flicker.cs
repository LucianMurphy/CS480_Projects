using UnityEngine;

public class Ghost_Flicker : MonoBehaviour
{
    public static float Lerp(float start, float end, float t)
    {
        if (t < 0) t = 0;
        if (t > 1) t = 1;
        return (1 - t) * start + t * end;
    }
    
    public Color colorStart = Color.black;
    public Color colorEnd = Color.white;

    private Light myLight;
    public float color_float = 0.0f;
    public bool color_red = true;


    void Start()
    {
        myLight = GetComponent<Light>();
        
    }

    
    void Update() 
    {
        
        float color_float = UnityEngine.Random.Range(0.0f, 1.0f);
        

        myLight.color = Color.Lerp(colorStart, colorEnd, color_float);
    
        
    }
}

