using UnityEngine;

[RequireComponent(typeof(Collider))]
public class GenericDetector<Type> : MonoBehaviour where Type 
    : MonoBehaviour
{
}
