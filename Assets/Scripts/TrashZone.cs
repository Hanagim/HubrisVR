using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashZone : MonoBehaviour
{
 
    private void Start()
    {
        GetComponent<TriggerZone>().OnEnterEvent.AddListener(disableTrash);
    }
    public void disableTrash(GameObject trash)
    {
        trash.SetActive(false);
    }
}
