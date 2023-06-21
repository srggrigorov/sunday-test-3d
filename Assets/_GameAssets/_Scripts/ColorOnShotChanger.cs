using UnityEngine;

public class ColorOnShotChanger : MonoBehaviour, IDamagaeble
{
    private Renderer objectRenderer;

    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
    }

    private void ChangeColor()
    {
        Color randomColor = new Color(Random.value, Random.value, Random.value);

        objectRenderer.material.color = randomColor;
    }

    public void GetDamage(float damageAmount)
    {
        ChangeColor();
    }
}