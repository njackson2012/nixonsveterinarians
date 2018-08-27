using System;
using Microsoft.CSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PieceLogicUnitTest
{
    [TestClass]
    public class PieceLogicTest
    {
        [TestMethod]
        public void TestPieceLogic()
        {
            int expectedID = 2;
            string expectedColor = "red";
            int[] expectedLocation = new int[] {0,0};
            bool expectedIsKing = false;

            PieceLogic p = new PieceLogic(expectedID, expectedColor);
            p.SetLocation(expectedLocation);

            int actualID = p.GetId();
            string actualColor = p.GetColor();
            int[] actualLocation = p.GetLocation();
            bool actualIsKing = p.IsKing();

            // test GetID
            Assert.AreEqual(expectedID, actualID);

            // test setLocation getLocation
            Assert.AreEqual(expectedLocation, actualLocation);

            // test getColor
            Assert.AreEqual(expectedColor, actualColor);

            //test IsKing 
            Assert.AreEqual(expectedIsKing, actualIsKing);

            // test KingMe
            p.KingMe();
            expectedIsKing = true;
            actualIsKing = p.IsKing();
            Assert.AreEqual(expectedIsKing, actualIsKing);
        }
    }
}
