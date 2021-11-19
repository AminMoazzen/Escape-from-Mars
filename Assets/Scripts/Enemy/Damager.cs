using Assets.Scripts;
using UnityEngine;
using UnityEngine.Events;

public class Damager : MonoBehaviour
{
    [SerializeField] public float Damage;
    [SerializeField] public string ObjectTag = GameTags.Player;

    [SerializeField] public UnityEvent<GameObject> OnObjectHitted;

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag.Contains(ObjectTag))
        {
            // TODO:

            OnObjectHitted.Invoke(other.gameObject);
        }
    }
}
