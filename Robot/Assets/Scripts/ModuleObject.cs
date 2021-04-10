using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ModuleObject : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public ModuleBehaviour behaviour;

    public int durability;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


}
