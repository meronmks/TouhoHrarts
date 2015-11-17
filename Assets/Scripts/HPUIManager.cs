using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HPUIManager : MonoBehaviour {

    public Image circleGauge;
    public Image lineGauge;
    public Image circleGaugeBase;
    public Image lineGaugeBase;
    private CharaStatus status;

    // Use this for initialization
    void Start () {
        status = GetComponent<CharaStatus>();
    }
	
	// Update is called once per frame
	void Update ()
	{
	    if (status.HP > 75)
	    {
	        circleGauge.fillAmount = 0.75f;
	        lineGauge.fillAmount = ((status.HP - 75)/100.0f);

	    }
	    else
	    {
	        circleGauge.fillAmount = (status.HP / 100.0f);
            lineGauge.fillAmount = 0.0f;
	    }

        if (status.Max_HP > 75)
        {
            circleGaugeBase.fillAmount = 0.75f;
            lineGaugeBase.fillAmount = ((status.Max_HP - 75) / 100.0f);

        }
        else
        {
            circleGaugeBase.fillAmount = (status.Max_HP / 100.0f);
            lineGaugeBase.fillAmount = 0.0f;
        }
    }
}
