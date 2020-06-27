using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dew : MonoBehaviour
{
    #region Variables
    private int value = 5;
    #endregion


    public int DewFeedValue
    {
        get
        {
            return value;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Veggie")
        {
            if (collision.gameObject.GetComponent<VegQuality>().CanFeed)
            {
                collision.gameObject.GetComponent<VegQuality>().VegFeeding();
                Destroy(this.gameObject);
            }
            else
            {
                collision.gameObject.GetComponent<VegQuality>().VegFeeding();
            }
        }
    }
}
