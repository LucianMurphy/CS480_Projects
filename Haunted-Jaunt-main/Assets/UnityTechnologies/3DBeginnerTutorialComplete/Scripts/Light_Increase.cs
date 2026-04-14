

using UnityEngine;


public class Light_Increase : MonoBehaviour
{
    public static float Lerp(float start, float end, float t)
    {
        if (t < 0) t = 0;
        if (t > 1) t = 1;
        return (1 - t) * start + t * end;
    }
    
    public Color colorStart = Color.black;
    public Color colorEnd = Color.red;

    private Light myLight;
    public float c_float = 0.0f;
    public bool color_red = true;


    void Start()
    {
        myLight = GetComponent<Light>();
        
    }

    
    void Update() 
    {
        
        //float c_float = UnityEngine.Random.Range(0.0f, 1.0f);
        if (c_float >= 1.0f)
        {
            color_red = false;
        }
        else if (c_float <= 0.0f)
        {
            color_red = true;
        }

        if (color_red == true)
        {
            c_float+= Time.deltaTime /2;
        }
        else
        {
            c_float-= Time.deltaTime /2;
        }

        myLight.color = Color.Lerp(colorStart, colorEnd, c_float);
    
        
    }
}

