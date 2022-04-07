using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
    void OnClick(){
		DataHolder.dataHolder.OpenMainMenu();
	}
}
