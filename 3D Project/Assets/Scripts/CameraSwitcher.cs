using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    [SerializeField]private List<Camera> cameras;
    void Start()
    {
        if(cameras == null || cameras.Count <= 0)
        {
            Debug.LogError("Camera list is empty or not assigned!");
            return;
        }
        ActiveCamera(0);
    }

    void Update()
    {
        for (int i = 0; i < cameras.Count; i++) {
            if(Input.GetKeyDown(KeyCode.Alpha1 + i)){
                ActiveCamera(i);
            }
        }
    }

    void ActiveCamera(int index)
    {
        if(index < 0 || index >= cameras.Count)
        {
            Debug.LogError("Invalid camera index");
            return;
        }
        for (int i = 0; index < cameras.Count; i++) {
            cameras[i].gameObject.SetActive(i == index);
        }
    }
}
