using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockScript : MonoBehaviour {

	public bool shouldSpin;

	public bool isSpinning;

	public float spinSpeed = 70.0f;

	void Awake () {

		isSpinning = true;

	}

	// Use this for initialization
	void Start () {
		
	}

	public void ScaleAndMove (float size) {

		StartCoroutine (ScaleAndMoveI (size));

	}

	public IEnumerator ScaleAndMoveI (float size ) {

		float mtime = 1.0f;
		float stime = 1.0f;

		Vector3 originalScale = transform.localScale;
		// Vector3 destinationScale = new Vector3(size, size, size);

		// x1.3 Scale
		Vector3 destinationScale = new Vector3(transform.localScale.x * 1.333f, transform.localScale.y * 1.333f, transform.localScale.z * 1.333f);

		Vector3 tar = new Vector3 (0, transform.position.y - 1.0f, 0);

		float currentTime = 0.0f;

		do
		{
			transform.localScale = Vector3.Lerp(originalScale, destinationScale, currentTime / mtime);
			transform.position = Vector3.MoveTowards(transform.position, tar, currentTime / stime);
			currentTime += Time.deltaTime;

//			while (transform.position != tar)
//			{
//				float step = 1.5f * Time.deltaTime;
//				transform.position = Vector3.MoveTowards(transform.position, tar, step);
//				yield return null;
//				// print ("DONE MOVING");
//			}




			yield return null;

		} while (currentTime <= stime);
			

	}

	IEnumerator Spin () {

		while (true) {


			yield return null;

		}

	}
	

	void Update () {
		
		if (isSpinning && shouldSpin) {

			transform.Rotate (Vector3.up * Time.deltaTime * spinSpeed);

		} else {

			print ("Can't spin!");

		}
		
	}

}
