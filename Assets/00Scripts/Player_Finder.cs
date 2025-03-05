using System.Collections.Generic;
using UnityEngine;

public class Player_Finder : MonoBehaviour
{
    [SerializeField] private float checkRadius = 5f;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] Canvas uiCanvas;
    [SerializeField] GameObject iconPrefab;
    [SerializeField] private float activationDistance = 3f;

    private Dictionary<Transform, GameObject> activeIcons = new Dictionary<Transform, GameObject>();

    private void Update()
    {
        Collider[] nearbyObjects = Physics.OverlapSphere(transform.position, checkRadius, interactableLayer);

        HashSet<Transform> currentObjects = new HashSet<Transform>();

        foreach(Collider obj in nearbyObjects)
        {
            Transform targetTransform = obj.transform;

            float distance = Vector3.Distance(transform.position, targetTransform.position);

            if(distance <= activationDistance)
            {
                ShowIcon(targetTransform);
                currentObjects.Add(targetTransform);
            }
        }

        List<Transform> toRemove = new List<Transform>();
        foreach(var iconEntry in activeIcons)
        {
            if(!currentObjects.Contains(iconEntry.Key))
            {
                iconEntry.Value.GetComponent<UI_Animation_Handler>().AnimationChange("Out");
                toRemove.Add(iconEntry.Key);
            }
        }

        foreach(var transformToRemove in toRemove)
        {
            activeIcons.Remove(transformToRemove);
        }
    }

    private void ShowIcon(Transform targetTransform)
    {
        if(activeIcons.ContainsKey(targetTransform))
        {
            UpdateIconPosition(targetTransform, activeIcons[targetTransform]);
            return;
        }

        GameObject iconInstance = Instantiate(iconPrefab, uiCanvas.transform);
        activeIcons[targetTransform] = iconInstance;

        UpdateIconPosition(targetTransform, iconInstance);
    }

    private void UpdateIconPosition(Transform targetTransform, GameObject icon)
    {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(targetTransform.position + new Vector3(0,1.5f,0));

        icon.GetComponent<RectTransform>().position = screenPosition;
    }
}
