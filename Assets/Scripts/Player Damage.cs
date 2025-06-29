using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDamage : MonoBehaviour
{
    private Text lifeText;

    private int lifeCount;
    private ScenesManager sm;

    private bool canDamage;

    // Start is called before the first frame update
    void Start()
    {
        lifeCount = 3;
        canDamage = true;
        lifeText = GameObject.Find("Life Text").GetComponent<Text>();
        sm = GameObject.Find("ScenesManager").GetComponent<ScenesManager>();
    }

    public void DealDamage()
    {
        if (canDamage)
        {
            if (lifeCount > 1)
            {
                lifeCount--;
                lifeText.text = "x" + lifeCount.ToString();
            }
            else
            {
                sm.GameOver();
            }

            canDamage = false;
            StartCoroutine(WaitForDamage());
        }
    }

    private IEnumerator WaitForDamage()
    {
        yield return new WaitForSeconds(2f);
        canDamage = true;
    }
}
