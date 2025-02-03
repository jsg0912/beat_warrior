using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public static InteractionManager Instance;
    public void Awake() { Instance = this; }

    private List<ObjectWithInteractionPrompt> objects = new();

    public void AddObject(ObjectWithInteractionPrompt objectWithInteractionPrompt)
    {
        objects.Add(objectWithInteractionPrompt);
    }

    public void RemoveObject(ObjectWithInteractionPrompt objectWithInteractionPrompt)
    {
        objects.Remove(objectWithInteractionPrompt);
    }

    public void InteractWithLastObject()
    {
        if (objects.Count > 0)
        {
            objects[objects.Count - 1].StartInteraction();
        }
    }
}