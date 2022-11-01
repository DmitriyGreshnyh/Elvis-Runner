using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBodyJump : MonoBehaviour
{
    // Start is called before the first frame update
    CharacterController characterController;

    [SerializeField] AnimationCurve AnimationCurve;
    float currenTime;
    [SerializeField] float maxTime;
    [SerializeField] float height;
    Vector3 _startPossition;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            StartCoroutine(Jump());
        }
    }

    IEnumerator Jump()
    {
        currenTime = 0;
        Vector3 direction = Vector3.zero;
        _startPossition = transform.position;
       // direction = -_startPossition + transform.position;
        while (currenTime <= maxTime)
        {

            float normalizedTime = currenTime / maxTime;

           // characterController.isKinematic = true;
          
            direction.y = _startPossition.y +AnimationCurve.Evaluate(normalizedTime)*height - transform.position.y;
            // direction.y = AnimationCurve.Evaluate(normalizedTime)*height;



            characterController.Move(direction);


           currenTime += Time.deltaTime;
            print(direction);
           yield return new WaitForFixedUpdate();
            
        }
        //characterController.isKinematic = false;
        yield return null;
    }


}
