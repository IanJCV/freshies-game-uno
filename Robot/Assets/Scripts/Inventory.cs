using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public List<ModuleBehaviour> modules = new List<ModuleBehaviour>();

    [System.Serializable]
    public class UIThing
    {
        public Slider slider;
        public Image image;
    }

    public List<UIThing> things = new List<UIThing>();

    public Transform
        armPoint,
        backPoint,
        legPoint,
        headPoint;

    const int playerLayer = 12;

    void Update()
    {
        ApplyInput();
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (modules.Count == 0)
        {
            return;
        }

        for (int i = 0; i < modules.Count - 1; i++)
        {
            things[i].slider.value = modules[i].Heat / 10;
        }
    }

    public void AddModule(GameObject module)
    {
        ModuleObject mObj = module.GetComponent<ModuleObject>();

        SetUI(mObj);
        modules.Add(mObj.behaviour);

        mObj.durability = Random.Range(1, mObj.behaviour.maxDurability + 1);
        module.layer = playerLayer;

        Destroy(module.GetComponent<Collider2D>());

        mObj.behaviour.parentObject = module;

        mObj.OnAttach();

        switch (mObj.behaviour.type)
        {
            case ModuleType.Arm:
                module.transform.parent = armPoint;
                break;
            case ModuleType.Back:
                module.transform.parent = backPoint;
                break;
            case ModuleType.Head:
                module.transform.parent = headPoint;
                break;
            case ModuleType.Leg:
                module.transform.parent = legPoint;
                break;

            default:
                Debug.Log("Improper setup of module! Check your shit");
                break;
        }
        module.transform.localPosition = Vector3.zero;
        module.transform.localRotation = Quaternion.identity;
    }
    private void SetUI(ModuleObject module)
    {
        things[modules.Count].image.sprite = module.behaviour.sprite;

    }

    public void RemoveModule(GameObject module)
    {
        
        ModuleBehaviour thing = module.GetComponent<ModuleObject>().behaviour;
        modules.Remove(thing);

    }

    private void ApplyInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetMouseButtonDown(0))
        {
            modules[0].OnPress();
        }
        if (Input.GetKeyUp(KeyCode.Alpha1) || Input.GetMouseButtonUp(0))
        {
            modules[0].OnRelease();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetMouseButtonDown(2))
        {
            modules[1].OnPress();
        }
        if (Input.GetKeyUp(KeyCode.Alpha2) || Input.GetMouseButtonDown(2))
        {
            modules[1].OnRelease();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetMouseButtonDown(1))
        {
            modules[2].OnPress();
        }
        if (Input.GetKeyUp(KeyCode.Alpha3) || Input.GetMouseButtonDown(1))
        {
            modules[2].OnRelease();
        }
    }
}
