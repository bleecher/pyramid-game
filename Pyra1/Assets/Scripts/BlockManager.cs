using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour {

	public GameObject Block1;
	public GameObject Block2;
	public GameObject PrevBlock;
	public GameObject NewBlock;
	public GameObject CurBlock;
	public GameObject blockFab;

	public GameObject BlockGroup;

	public float spinSpeed = 50.0f;
	public float give = 30.0f;
	private float inverseMoveTime;

	public int curBlock;

	Coroutine SpinBlockRoutine = null;


	// Awake
	void Awake () {

		curBlock = 2;


	}

	// Start Game
	public void StartGame () {
		
		CurBlock = Block2;

		StartCoroutine(MoveBlock(CurBlock));
		// SpinBlockRoutine = StartCoroutine(SpinBlock(Block2));
		StartCoroutine(CheckRot(Block2));
	}


	// Update is called once per frame
	void Update () {

	}

//	protected IEnumerator MoveBlock(Transform block, Vector3 tar)
//	{
//		float sqrRemainingDistance = (transform.position - tar).sqrMagnitude;
//
//		while (sqrRemainingDistance > float.Epsilon)
//		{
//			float step = 0.5F * Time.deltaTime;
//			block.position = Vector3.MoveTowards(block.position, tar, step);
//			yield return null;
//		}
//
//	}

	protected IEnumerator MoveBlock(GameObject block)
	{
		float curblocksize = CurBlock.GetComponent<Renderer> ().bounds.size.y;
		print ("Cur block bounds y = " + curblocksize);
			
		Vector3 tar = new Vector3 (0, PrevBlock.transform.position.y + PrevBlock.GetComponent<Renderer>().bounds.size.y /2, 0);

		// Vector3 tar = new Vector3 (0, PrevBlock.transform.position.y, 0);

		// print (PrevBlock.GetComponent<Renderer>().bounds.size);

		while (block.transform.position != tar)
		{
			float step = 1.5f * Time.deltaTime;
			block.transform.position = Vector3.MoveTowards(block.transform.position, tar, step);
			yield return null;
			// print ("DONE MOVING");
		}


	}

//	IEnumerator SpinBlock (GameObject block) {
//
//		print ("SPIN");
//
//		while (true) {
//
//
//			yield return null;
//
//		}
//			
//	}

	IEnumerator CheckRot(GameObject block) {
		for(;;) {
			
			yield return new WaitForSeconds(.02f);

		}
	}

	IEnumerator SnapBlock (GameObject block) {

		// print ("SNAP SNAP");
		Vector3 to = new Vector3(0, 0, 0);
		block.transform.eulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, to, Time.deltaTime);
		yield return null;



	}

	IEnumerator Shift () {

		while (true) {
			
		}

	}

	public void NextBlock () {

		curBlock += 1;

		NewBlock = Instantiate(blockFab, new Vector3(0, 2.5f, 0), Quaternion.Euler(0, 0, 0));
		NewBlock.name = "Block " + curBlock;

		CurBlock = NewBlock;

		StartCoroutine(MoveBlock(NewBlock));

		// StartCoroutine(SpinBlock(NewBlock));
	}



	public bool CheckAlign () {

		bool aligned;

		float yRot = Block2.transform.localRotation.eulerAngles.y;

		if (yRot >= (360.0f - give)  && yRot <= 360.0f || yRot >= 0.0f && yRot <= give) {

			// print ("Stopped spinning at " + Block2.transform.localRotation.eulerAngles.y);

			// StopCoroutine(SpinBlockRoutine);

			BlockScript cbs = CurBlock.GetComponent<BlockScript>();
			cbs.isSpinning = false;

			BlockScript pbs = PrevBlock.GetComponent<BlockScript>();

			cbs.ScaleAndMove (1.00f);

			pbs.ScaleAndMove (1.5f);

			StartCoroutine(SnapBlock(CurBlock));

			PrevBlock = CurBlock;

			// NextBlock ();

			return true;

		} else {
			
			return false;
		}

	}

}
