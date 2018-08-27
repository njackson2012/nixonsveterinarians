using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class GameControllerUnitTest
    {
        //Helper Method which constructs a game controller.  Is used by many other tests.
        [TestMethod]
        public GameController BuildGameController()
        {
            PieceLogic[] testPieces = new PieceLogic[24];
            int[,] locations = new int[24, 2] { {0,0}, {0,2}, {0,4}, {0,6}, {1,1}, {1,3}
                , {1,5}, {1,7}, {2,0}, {2,2}, {2,4}, {2,6}, {5,1}, {5,3}, {5,5}, {5,7}, {6,0}, {6,2}, {6,4}, {6,6}, {7,1}, {7,3}, {7,5}, {7,7}
    };
            for (int i = 0; i < 12; i++)
            {
                testPieces[i] = new PieceLogic(i, "Red");
                testPieces[i].SetLocation(new int[]{locations[i,0], locations[i,1]});
                testPieces[i + 12] = new PieceLogic(i+12, "Black");
                testPieces[i + 12].SetLocation(new int[] {locations[i+12,0], locations[i+12,1]});
            }
            BoardLogic testBoard = new BoardLogic(testPieces);
            GameController testGc = new GameController(2, "Red", testBoard);
            testGc.SetGameStatus("Ongoing");
            return testGc;
        }

        //Tests the Game Controller Constructor
        [TestMethod]
        public void TestGameController()
        {
            GameController testGc = BuildGameController();
            Assert.AreEqual("Red", testGc.GetPlayerColor());
            Assert.AreEqual(2, testGc.GetGameId());
        }

        //Tests GetGameId()
        [TestMethod]
        public void TestGetGameId()
        {
            GameController testGc = BuildGameController();
            Assert.AreEqual(2, testGc.GetGameId());
        }

        //Tests GetGameStatus()
        [TestMethod]
        public void TestGetGameStatus()
        {
            GameController testGc = BuildGameController();
            Assert.AreEqual("Ongoing", testGc.GetGameStatus());
        }

        //Tests GetRequestStatus()
        [TestMethod]
        public void TestGetRequestStatus()
        {
            GameController testGc = BuildGameController();
            Assert.AreEqual("None", testGc.GetRequestStatus());

        }

    }
}
