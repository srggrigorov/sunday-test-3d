using UnityEngine;

public class ObjectRotator : MonoBehaviour
{
    public float RotationSpeed = 5f;
    public Vector3 RotationAxisVector;
    

    

    private void Update()
    {
        transform.Rotate(RotationAxisVector.normalized, RotationSpeed * Time.deltaTime);
    }

   
}