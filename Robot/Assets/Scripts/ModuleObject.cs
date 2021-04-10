using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(AudioSource))]
public class ModuleObject : MonoBehaviour
{
    public ModuleBehaviour behaviour;
    public AudioSource audioSource;

    public int durability;

    public void OnAttach()
    {
        audioSource = GetComponent<AudioSource>();
        behaviour.source = audioSource;
        audioSource.clip = behaviour.sound;
    }


}
