using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRHand : MonoBehaviour
{
    public string Hand;
    public Animator animator;

    public Transform spawnPoint;
    public GameObject spawnPrefab;

    private string triggerButton;
    private string gripButton;

    public float coolDown = 2f;
    public bool waitCoolDown = false;


    // Start is called before the first frame update
    void Start()
    {
        triggerButton = Hand + "Trigger";
        gripButton = Hand + "Grip";
    }

    // Update is called once per frame
    void Update()
    {
        float gripValue = Input.GetAxis(gripButton);

        Debug.Log(gripValue);

        if (gripValue > 0.8f)
        {
            animator.SetBool("Grip", true);
        }
        else
        {
            animator.SetBool("Grip", false);
        }

        float triggerValue = Input.GetAxis(triggerButton);
        
        
        if (triggerValue > 0.8 && !waitCoolDown)
        {
            CastSpell();
        }
    }

    void CastSpell()
    {
        animator.SetTrigger("Cast");
        waitCoolDown = true;
        Invoke("ResetCoolDown", coolDown);

        GameObject newCast = Instantiate(spawnPrefab, spawnPoint.position, spawnPoint.rotation);
        newCast.GetComponent<Rigidbody>().AddForce(spawnPoint.transform.forward * 20, ForceMode.Impulse);



    }

    void ResetCoolDown()
    {
        waitCoolDown = false;
    }
}