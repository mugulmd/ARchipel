using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtUtility : MonoBehaviour
{
    public GameObject lookAtObject = null;
    private bool enableLookAt = false;
    public Animator animator = null; //if this is set null, we use transform to simulate animation
    public void SetLookAtObject(GameObject obj)
    {
        lookAtObject = obj;
        if (obj == null)
        {
            enableLookAt = false;
        }
        else
        {
            enableLookAt = true;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (enableLookAt && animator == null)
        {
            Vector3 delta = lookAtObject.transform.position - transform.position;
            delta.y = 0;
            delta = delta.normalized;
            this.transform.rotation = Quaternion.LookRotation(delta);
            //enableLookAt = false;
        }
    }

    void OnAnimatorIK(int layer)
    {
        if (!enableLookAt)
        {
            return;
        }
        if (layer == 1)
        {
            animator.SetLookAtPosition(lookAtObject.transform.position);
            animator.SetLookAtWeight(0.25f, 0.5f, 1f, 1f, 0.6f);
        }
    }
}
