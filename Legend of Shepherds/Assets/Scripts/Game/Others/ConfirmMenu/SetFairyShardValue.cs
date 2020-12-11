using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetFairyShardValue : MonoBehaviour {


    public Text fairyShardText;

	public void SetValue(int fairyShard)
    {
        fairyShardText.text = fairyShard.ToString();
    }
}
