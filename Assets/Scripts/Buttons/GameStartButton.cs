using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartButton : MonoBehaviour, IInteractable
{
    private Coroutine pushButtonCoroutine;
    private bool isPushed;
    public void Interact()
    {
        if (!isPushed)
        {
            if (pushButtonCoroutine != null)
            {
                StopCoroutine(pushButtonCoroutine);
            }
            pushButtonCoroutine = StartCoroutine(PushButton());
            Debug.Log("GameStart");
        }
    }

    private IEnumerator PushButton()
    {
        //  Hard Coding.....................
        isPushed = true;
        transform.localPosition = new Vector3(0, 0, 0.01f);
        yield return new WaitForSeconds(1f);
        isPushed = false;
        transform.localPosition = new Vector3(0, 0, 0.03f);
    }
}
