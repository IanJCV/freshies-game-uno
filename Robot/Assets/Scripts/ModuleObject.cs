using UnityEngine;

public class ModuleObject : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public ModuleBehaviour module;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


}
