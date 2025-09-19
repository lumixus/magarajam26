using UnityEngine;

public class HoleLight : MonoBehaviour
{
    public Sprite lightOnSprite;
    public Sprite lightOffSprite;
    public SpriteRenderer sr;
    public bool LightsOn = false;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void LightOn()
    {
        sr.sprite = lightOnSprite;
        LightsOn = true;
    }

    public void LightOff()
    {
        sr.sprite = lightOffSprite;
        LightsOn = false;
    }
}
