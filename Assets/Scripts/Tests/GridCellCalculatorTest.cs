using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class GridCellCalculatorTest
    {
        // A Test behaves as an ordinary method
        [Test]
        public void GridCellCalculatorTestSimplePasses()
        {
            Vector2 position = GridCellCalculator.ComputeChunkPosition(new Vector2(-30,0));
            Debug.Log(position);
            Assert.AreEqual(position, Vector2.zero);

            position = GridCellCalculator.ComputeChunkPosition(new Vector2(-61,0));
            Assert.AreEqual(position, new Vector2(-64, 0));

            position = GridCellCalculator.ComputeChunkPosition(new Vector2(-93,0));
            Assert.AreEqual(position, new Vector2(-64, 0));

            position = GridCellCalculator.ComputeChunkPosition(new Vector2(-122,0));
            Assert.AreEqual(position, new Vector2(-128, 0));

            position = GridCellCalculator.ComputeChunkPosition(new Vector2(-228,0));
            Assert.AreEqual(position, new Vector2(-191, 0));


        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator GridCellCalculatorTestWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
    }
}
