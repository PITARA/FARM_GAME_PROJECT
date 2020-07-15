using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VeggieInfoPanel : MonoBehaviour
{
    #region Variables
    Ray ray;
    RaycastHit hit;

    public Text hungerText;
    public Text qualityText;

    public GameObject veggiePanel;

    private string vegQualityCheck;
    #endregion

    private void Awake()
    {
        veggiePanel.SetActive(false);
    }

    private void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.GetComponent<VegQuality>())
            {
                if (hit.collider.gameObject.GetComponent<PickThrow>().distance <= 3f)
                {
                    UpdateVeggieInfoPanel();
                }
                else
                {
                    veggiePanel.SetActive(false);
                }
            }
            else
            {
                veggiePanel.SetActive(false);
            }
        }
    }

    private void UpdateVeggieInfoPanel()
    {
        veggiePanel.SetActive(true);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.GetComponent<VegQuality>())
            {
                Color qualityTextColor;

                vegQualityCheck = hit.collider.gameObject.GetComponent<VegQuality>().vegQuality.ToString();
                qualityText.text = vegQualityCheck;

                switch (vegQualityCheck)
                {
                    case "DECAYING":
                        qualityTextColor = new Color(219/255.0f, 56/255.0f, 56/255.0f); // red
                        break;

                    case "NORMAL":
                        qualityTextColor = new Color(249/255.0f, 162/255.0f, 40/255.0f); // orange
                        break;

                    case "GOOD":
                        qualityTextColor = new Color(254/255.0f, 204/255.0f, 47/255.0f); // yellow
                        break;

                    case "GREAT":
                        qualityTextColor = new Color(178/255.0f, 194/255.0f, 37/255.0f); // green
                        break;

                    case "EXCELLENT":
                        qualityTextColor = new Color(163/255.0f, 99/255.0f, 217/255.0f); // purple
                        break;

                    case "PRISTINE":
                        qualityTextColor = Color.white; // rainbow
                        break;

                    default:
                        qualityTextColor = Color.black;
                        break;
                }

                qualityText.color = qualityTextColor;

                if (hit.collider.gameObject.GetComponent<VegQuality>().canFeed)
                {
                    if (!hit.collider.gameObject.GetComponent<VegQuality>().isStarving)
                    {
                        hungerText.text = "HUNGRY";
                        hungerText.color = qualityTextColor = new Color(249 / 255.0f, 162 / 255.0f, 40 / 255.0f);
                    }
                    else
                    {
                        hungerText.text = "STARVING";
                        hungerText.color = qualityTextColor = new Color(219 / 255.0f, 56 / 255.0f, 56 / 255.0f);
                    }
                }
                else
                {
                    hungerText.text = "FULL BELLY";
                    hungerText.color = qualityTextColor = new Color(178 / 255.0f, 194 / 255.0f, 37 / 255.0f);
                }
            }
        }
    }
}
