using System;
using Microsoft.CSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BoardLogicUnitTest
{
    [TestClass]
    public class BoardLogicTest
    {
        [TestMethod]
        public void BoardLogicGetSet()
        {
            PieceLogic[] expectedPieces = new PieceLogic[24];
            for (int i = 0; i < 12; i++)
            {
                PieceLogic p = new PieceLogic(i, "Red");
                expectedPieces[i] = p;

            }
            for (int i = 11; i < 24; i++)
            {
                PieceLogic p = new PieceLogic(i, "Black");
                expectedPieces[i] = p;
            }
            BoardLogic board = new BoardLogic(expectedPieces);
            PieceLogic[] actualPieces = board.GetPieces();

            // test GetPieces
            Assert.AreEqual(expectedPieces, actualPieces);

            for (int i = 0; i < 12; i++)
            {
                PieceLogic p = new PieceLogic(i, "Black");
                expectedPieces[i] = p;

            }
            for (int i = 11; i < 24; i++)
            {
                PieceLogic p = new PieceLogic(i, "Red");
                expectedPieces[i] = p;
            }

            // test SetPieces
            board.SetPieces(expectedPieces);
            actualPieces = board.GetPieces();
            Assert.AreEqual(expectedPieces, actualPieces);

        }

        [TestMethod]
        public void BoardLogicRemovePiece()
        {
            PieceLogic[] expectedPieces = new PieceLogic[24];
            for (int i = 0; i < 12; i++)
            {
                PieceLogic p = new PieceLogic(i, "Red");
                expectedPieces[i] = p;

            }
            for (int i = 11; i < 24; i++)
            {
                PieceLogic p = new PieceLogic(i, "Black");
                expectedPieces[i] = p;
            }
            BoardLogic board = new BoardLogic(expectedPieces);

            int idToRemove = 1;

            // test RemovePiece
            board.RemovePiece(idToRemove);
            PieceLogic[] actualPieces = board.GetPieces();
            for (int i = 0; i < actualPieces.Length; i++)
            {
                if (actualPieces[i].GetId() == idToRemove)
                {
                    int[] actualLocation = actualPieces[i].GetLocation();
                    int[] expectedLocation = new int[] {-1, -1};
                    Assert.AreEqual(actualLocation[0], expectedLocation[0]);
                }
            }
        }
    }
}
