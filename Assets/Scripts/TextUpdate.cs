using UnityEngine;
using System.Collections;

public class TextUpdate : MonoBehaviour {

	public void UpdateText(string Text)
    {
		this.GetComponent<TextMesh>().text = Text;
    }
}
