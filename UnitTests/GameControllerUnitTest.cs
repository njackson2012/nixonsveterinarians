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
                testPieces[i].SetLocation(new int[] { locations[i, 0], locations[i, 1] });
                testPieces[i + 12] = new PieceLogic(i + 12, "Black");
                testPieces[i + 12].SetLocation(new int[] { locations[i + 12, 0], locations[i + 12, 1] });
            }
            BoardLogic testBoard = new BoardLogic(testPieces);
            GameController testGc = new GameController(2, "Red", testBoard);
            testGc.SetGameStatus("Ongoing");
            return testGc;
        }

        public GameController BuildGameControllerWithJumps()
        {
            PieceLogic[] testPieces = new PieceLogic[24];
            int[,] locations = new int[24, 2] { {0,0}, {0,2}, {0,4}, {0,6}, {1,1}, {1,3}
                , {1,5}, {1,7}, {2,0}, {3,3}, {2,4}, {3,5}, {4,2}, {4,4}, {4,6}, {5,7}, {6,0}, {6,2}, {6,4}, {6,6}, {7,1}, {7,3}, {7,5}, {7,7}
            };
            for (int i = 0; i < 12; i++)
            {
                testPieces[i] = new PieceLogic(i, "Red");
                testPieces[i].SetLocation(new int[] { locations[i, 0], locations[i, 1] });
                testPieces[i + 12] = new PieceLogic(i + 12, "Black");
                testPieces[i + 12].SetLocation(new int[] { locations[i + 12, 0], locations[i + 12, 1] });
            }
            BoardLogic testBoard = new BoardLogic(testPieces);
            GameController testGc = new GameController(2, "Red", testBoard);
            testGc.SetGameStatus("Ongoing");
            return testGc;
        }

        public GameController BuildGameControllerBlackPlayer()
        {
            PieceLogic[] testPieces = new PieceLogic[24];
            int[,] locations = new int[24, 2] { {0,0}, {0,2}, {0,4}, {0,6}, {1,1}, {1,3}
                , {1,5}, {1,7}, {2,0}, {2,2}, {2,4}, {2,6}, {5,1}, {5,3}, {5,5}, {5,7}, {6,0}, {6,2}, {6,4}, {6,6}, {7,1}, {7,3}, {7,5}, {7,7}
            };
            for (int i = 0; i < 12; i++)
            {
                testPieces[i] = new PieceLogic(i, "Red");
                testPieces[i].SetLocation(new int[] { locations[i, 0], locations[i, 1] });
                testPieces[i + 12] = new PieceLogic(i + 12, "Black");
                testPieces[i + 12].SetLocation(new int[] { locations[i + 12, 0], locations[i + 12, 1] });
            }
            BoardLogic testBoard = new BoardLogic(testPieces);
            GameController testGc = new GameController(2, "Black", testBoard);
            testGc.SetGameStatus("Ongoing");
            return testGc;
        }

        public GameController BuildGameControllerRedPlayer()
        {
            PieceLogic[] testPieces = new PieceLogic[24];
            int[,] locations = new int[24, 2] { {0,0}, {0,2}, {0,4}, {0,6}, {1,1}, {1,3}
                , {1,5}, {1,7}, {2,0}, {2,2}, {2,4}, {2,6}, {5,1}, {5,3}, {5,5}, {5,7}, {6,0}, {6,2}, {6,4}, {6,6}, {7,1}, {7,3}, {7,5}, {7,7}
            };
            for (int i = 0; i < 12; i++)
            {
                testPieces[i] = new PieceLogic(i, "Red");
                testPieces[i].SetLocation(new int[] { locations[i, 0], locations[i, 1] });
                testPieces[i + 12] = new PieceLogic(i + 12, "Black");
                testPieces[i + 12].SetLocation(new int[] { locations[i + 12, 0], locations[i + 12, 1] });
            }
            BoardLogic testBoard = new BoardLogic(testPieces);
            GameController testGc = new GameController(2, "Red", testBoard);
            testGc.SetGameStatus("Ongoing");
            return testGc;
        }

        public GameController BuildGameControllerBlackPlayerWithJumps()
        {
            PieceLogic[] testPieces = new PieceLogic[24];
            int[,] locations = new int[24, 2] { {0,0}, {0,2}, {0,4}, {0,6}, {1,1}, {1,3}
                , {1,5}, {1,7}, {2,0}, {3,3}, {2,4}, {3,5}, {4,2}, {4,4}, {4,6}, {5,7}, {6,0}, {6,2}, {6,4}, {6,6}, {7,1}, {7,3}, {7,5}, {7,7}
            };
            for (int i = 0; i < 12; i++)
            {
                testPieces[i] = new PieceLogic(i, "Red");
                testPieces[i].SetLocation(new int[] { locations[i, 0], locations[i, 1] });
                testPieces[i + 12] = new PieceLogic(i + 12, "Black");
                testPieces[i + 12].SetLocation(new int[] { locations[i + 12, 0], locations[i + 12, 1] });
            }
            BoardLogic testBoard = new BoardLogic(testPieces);
            GameController testGc = new GameController(2, "Black", testBoard);
            testGc.SetGameStatus("Ongoing");
            return testGc;
        }

        public GameController BuildGameControllerRedPlayerWithJumps()
        {
            PieceLogic[] testPieces = new PieceLogic[24];
            int[,] locations = new int[24, 2] { {0,0}, {0,2}, {0,4}, {0,6}, {1,1}, {1,3}
                , {1,5}, {1,7}, {2,0}, {3,3}, {2,4}, {3,5}, {4,2}, {4,4}, {4,6}, {5,7}, {6,0}, {6,2}, {6,4}, {6,6}, {7,1}, {7,3}, {7,5}, {7,7}
            };
            for (int i = 0; i < 12; i++)
            {
                testPieces[i] = new PieceLogic(i, "Red");
                testPieces[i].SetLocation(new int[] { locations[i, 0], locations[i, 1] });
                testPieces[i + 12] = new PieceLogic(i + 12, "Black");
                testPieces[i + 12].SetLocation(new int[] { locations[i + 12, 0], locations[i + 12, 1] });
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

        //Tests SetGameStatus()
        [TestMethod]
        public void TestSetGameStatus()
        {
            GameController testGc = BuildGameController();
            Assert.AreEqual("Ongoing", testGc.GetGameStatus());
            testGc.SetGameStatus("Waiting4Player2Join");
            Assert.AreEqual("Waiting4Player2Join", testGc.GetGameStatus());
        }

        //Tests GetRequestStatus()
        [TestMethod]
        public void TestGetRequestStatus()
        {
            GameController testGc = BuildGameController();
            Assert.AreEqual("None", testGc.GetRequestStatus());
        }

        //Tests SetRequestStatus()
        [TestMethod]
        public void TestSetRequestStatus()
        {
            GameController testGc = BuildGameController();
            Assert.AreEqual("None", testGc.GetRequestStatus());
            testGc.SetRequestStatus("DrawRequestBlack");
            Assert.AreEqual("DrawRequestBlack", testGc.GetRequestStatus());
        }

        //Tests GetPlayerColor()
        [TestMethod]
        public void TestGetPlayerColor()
        {
            GameController testGc = BuildGameController();
            Assert.AreEqual("Red", testGc.GetPlayerColor());
        }

        //Tests GetPlayerTurn() and SetPlayerTurn() and SwitchPlayer()
        [TestMethod]
        public void TestSetAndGetPlayerTurn()
        {
            GameController testGc = BuildGameController();
            testGc.SetPlayerTurn("Black");
            string playerColor = testGc.GetPlayerTurn();
            Assert.AreEqual("Black", playerColor);
            testGc.SetPlayerTurn("Red");
            playerColor = testGc.GetPlayerTurn();
            Assert.AreEqual("Red", playerColor);
            testGc.SwitchTurn();
            playerColor = testGc.GetPlayerTurn();
            Assert.AreEqual("Black", playerColor);
            testGc.SwitchTurn();
            playerColor = testGc.GetPlayerTurn();
            Assert.AreEqual("Red", playerColor);
        }

        //Tests TestFindValidMoves()
        [TestMethod]
        public void TestFindValidMoves()
        {
            GameController testGc = BuildGameController();
            GameController testGcWithJumps = BuildGameControllerWithJumps();
            GameController testGcBP = BuildGameControllerBlackPlayer();
            GameController testGcBPWithJump = BuildGameControllerBlackPlayerWithJumps();

            Assert.AreEqual("Red", testGc.GetPlayerColor());
            testGc.FindValidMoves();

            Assert.AreEqual("Red", testGcWithJumps.GetPlayerColor());
            testGcWithJumps.FindValidMoves();

            Assert.AreEqual("Black", testGcBP.GetPlayerColor());
            testGcBP.FindValidMoves();

            Assert.AreEqual("Black", testGcBPWithJump.GetPlayerColor());
            testGcBPWithJump.FindValidMoves();
        }

        //Tests IsMoveValid With() Slides
        [TestMethod]
        public void TestIsMoveValidWithSlides()
        {
            //Create GameController with a board with pieces in their starting positions
            GameController testGc = BuildGameControllerBlackPlayer();

            //Black valid slide
            int[,] move = new int[2,2] {{5,1},{4,0}};
            bool isValid = testGc.IsMoveValid(move);
            Assert.IsTrue(isValid);

            //Black invalid slide - off board
            move = new int[2, 2] { { 5, 7 }, { 4, 8 } };
            isValid = testGc.IsMoveValid(move);
            Assert.IsFalse(isValid);

            //Black invalid slide - on board
            move = new int[2, 2] { { 5, 7 }, { 4, 7 } };
            isValid = testGc.IsMoveValid(move);
            Assert.IsFalse(isValid);

            //switch to red player
            testGc = BuildGameControllerRedPlayer();
            //Red valid slide
            move = new int[2, 2] { { 2, 4 }, { 3, 3 } };
            isValid = testGc.IsMoveValid(move);
            Assert.IsTrue(isValid);

            //Red invalid slide - off board
            move = new int[2, 2] { { 2, 0 }, { 3, -1 } };
            isValid = testGc.IsMoveValid(move);
            Assert.IsFalse(isValid);

            //Red invalid slide - on board
            move = new int[2, 2] { { 0, 0 }, { 1, 1 } };
            isValid = testGc.IsMoveValid(move);
            Assert.IsFalse(isValid);

        }

        //Tests IsMoveValid With() Jumps
        [TestMethod]
        public void TestIsMoveValidWithJumps()
        {
            //Create GameController with a board with pieces in their starting positions
            GameController testGc = BuildGameControllerBlackPlayerWithJumps();

            //Black valid jump
            int[,] move = new int[2, 2] { { 4, 4 }, { 2, 2 } };
            bool isValid = testGc.IsMoveValid(move);
            Assert.IsTrue(isValid);

            //Black invalid jump - off board
            move = new int[2, 2] { { 4, 4 }, { -1, -1 } };
            isValid = testGc.IsMoveValid(move);
            Assert.IsFalse(isValid);

            //Black invalid jump - on board
            move = new int[2, 2] { { 6, 0 }, { 4, 2 } };
            isValid = testGc.IsMoveValid(move);
            Assert.IsFalse(isValid);
        

            //black invalid slide (because there are jumps)

            //switch to red player
            testGc = BuildGameControllerRedPlayerWithJumps();

            //Red valid jump
            move = new int[2, 2] { { 3, 3 }, { 5, 5 } };
            isValid = testGc.IsMoveValid(move);
            Assert.IsTrue(isValid);

            //Black valid jump, but should be invalid because its red's turn
            move = new int[2, 2] { { 4, 4 }, { 2, 2 } };
            isValid = testGc.IsMoveValid(move);
            Assert.IsFalse(isValid);

            //Red invalid jump - off board
            move = new int[2, 2] { { 2, 0 }, { 0, -1 } };
            isValid = testGc.IsMoveValid(move);
            Assert.IsFalse(isValid);

            //Red invalid jump - on board
            move = new int[2, 2] { { 2, 0 }, { 1, 0 } };
            isValid = testGc.IsMoveValid(move);
            Assert.IsFalse(isValid);

        }

    }
}
