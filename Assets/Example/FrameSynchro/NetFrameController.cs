using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace newnamespace {

	public class NetFrameController : MonoBehaviour {

        float AccumilatedTime = 0f;
        int GameFrame;
        float FrameLength = 0.05f; //50 miliseconds 

        public void Update() {
            //Basically same logic as FixedUpdate, but we can scale it by adjusting FrameLength 
            AccumilatedTime = AccumilatedTime + Time.deltaTime;

            //in case the FPS is too slow, we may need to update the game multiple times a frame 
            while(AccumilatedTime > FrameLength) {
                //GameFrameTurn();
                AccumilatedTime = AccumilatedTime - FrameLength;
            }
        }

        //void GameFrameTurn() {
        //    //first frame is used to process actions 
        //    if(GameFrame == 0) {
        //        if(LockStepTurn()) {
        //            GameFrame++;
        //        }
        //    } else {
        //        //update game 
        //        //SceneManager.Manager.TwoDPhysics.Update(GameFramesPerSecond);

        //        List<IHasGameFrame> finished = new List<IHasGameFrame>();
        //        foreach(IHasGameFrame obj in SceneManager.Manager.GameFrameObjects) {
        //            obj.GameFrameTurn(GameFramesPerSecond);
        //            if(obj.Finished) {
        //                finished.Add(obj);
        //            }
        //        }

        //        foreach(IHasGameFrame obj in finished) {
        //            SceneManager.Manager.GameFrameObjects.Remove(obj);
        //        }

        //        GameFrame++;
        //        if(GameFrame == GameFramesPerLocksetpTurn) {
        //            GameFrame = 0;
        //        }
        //    }
        //}

        void Start () {
            
        }
	}
}