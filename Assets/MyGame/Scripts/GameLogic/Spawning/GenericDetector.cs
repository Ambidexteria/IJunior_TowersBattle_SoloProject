using UnityEngine;

[RequireComponent(typeof(Collider))]
public class GenericDetector<T> : MonoBehaviour 
    where T : MonoBehaviour
{
}
