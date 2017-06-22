using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommonLibrary4Net.Test
{
    [TestClass]
    public class FuncExtTest
    {
        [TestMethod]
        public async Task AsTask_NoParameter_Verify()
        {
            var result = await new Func<int>(GetZero).AsTask();
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public async Task AsTask_OneParameter_Verify()
        {
            var result = await new Func<int, int>(GetNumber).AsTask(10);
            Assert.AreEqual(10, result);
        }

        [TestMethod]
        public async Task AsTask_TwoParameters_Verify()
        {
            var result = await new Func<int, int, int>(GetNumber).AsTask(10, 10);
            Assert.AreEqual(10 * 2, result);
        }

        [TestMethod]
        public async Task AsTask_ThreeParameters_Verify()
        {
            var result = await new Func<int, int, int, int>(GetNumber).AsTask(10, 10, 10);
            Assert.AreEqual(10 * 3, result);
        }

        [TestMethod]
        public async Task AsTask_FourParameters_Verify()
        {
            var result = await new Func<int, int, int, int, int>(GetNumber).AsTask(10, 10, 10, 10);
            Assert.AreEqual(10 * 4, result);
        }

        [TestMethod]
        public async Task AsTask_FiveParameters_Verify()
        {
            var result = await new Func<int, int, int, int, int, int>(GetNumber).AsTask(10, 10, 10, 10, 10);
            Assert.AreEqual(10 * 5, result);
        }

        [TestMethod]
        public async Task AsTask_SixParameters_Verify()
        {
            var result = await new Func<int, int, int, int, int, int, int>(GetNumber).AsTask(10, 10, 10, 10, 10, 10);
            Assert.AreEqual(10 * 6, result);
        }

        [TestMethod]
        public async Task AsTask_SevenParameters_Verify()
        {
            var result =
                await new Func<int, int, int, int, int, int, int, int>(GetNumber).AsTask(10, 10, 10, 10, 10, 10, 10);
            Assert.AreEqual(10 * 7, result);
        }

        [TestMethod]
        public async Task AsTask_EightParameters_Verify()
        {
            var result =
                await
                    new Func<int, int, int, int, int, int, int, int, int>(GetNumber).AsTask(10, 10, 10, 10, 10, 10, 10,
                        10);
            Assert.AreEqual(10 * 8, result);
        }

        [TestMethod]
        public async Task AsTask_NineParameters_Verify()
        {
            var result =
                await
                    new Func<int, int, int, int, int, int, int, int, int, int>(GetNumber).AsTask(10, 10, 10, 10, 10, 10,
                        10, 10, 10);
            Assert.AreEqual(10 * 9, result);
        }

        [TestMethod]
        public async Task AsTask_TenParameters_Verify()
        {
            var result =
                await
                    new Func<int, int, int, int, int, int, int, int, int, int, int>(GetNumber).AsTask(10, 10, 10, 10, 10,
                        10, 10, 10, 10, 10);
            Assert.AreEqual(10 * 10, result);
        }

        [TestMethod]
        public async Task AsTask_ElevenParameters_Verify()
        {
            var result =
                await
                    new Func<int, int, int, int, int, int, int, int, int, int, int, int>(GetNumber).AsTask(10, 10, 10,
                        10, 10, 10, 10, 10, 10, 10, 10);
            Assert.AreEqual(10 * 11, result);
        }

        [TestMethod]
        public async Task AsTask_TwelveParameters_Verify()
        {
            var result =
                await
                    new Func<int, int, int, int, int, int, int, int, int, int, int, int, int>(GetNumber).AsTask(10, 10,
                        10, 10, 10, 10, 10, 10, 10, 10, 10, 10);
            Assert.AreEqual(10 * 12, result);
        }

        [TestMethod]
        public async Task AsTask_ThirteenParameters_Verify()
        {
            var result =
                await
                    new Func<int, int, int, int, int, int, int, int, int, int, int, int, int, int>(GetNumber).AsTask(
                        10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10);
            Assert.AreEqual(10 * 13, result);
        }

        [TestMethod]
        public async Task AsTask_FourteenParameters_Verify()
        {
            var result =
                await
                    new Func<int, int, int, int, int, int, int, int, int, int, int, int, int, int, int>(GetNumber)
                        .AsTask(10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10);
            Assert.AreEqual(10 * 14, result);
        }

        [TestMethod]
        public async Task AsTask_FifteenParameters_Verify()
        {
            var result =
                await
                    new Func<int, int, int, int, int, int, int, int, int, int, int, int, int, int, int, int>(GetNumber)
                        .AsTask(10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10);
            Assert.AreEqual(10 * 15, result);
        }

        [TestMethod]
        public async Task AsTask_SixteenParameters_Verify()
        {
            var result =
                await
                    new Func<int, int, int, int, int, int, int, int, int, int, int, int, int, int, int, int, int>(
                        GetNumber).AsTask(10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10);
            Assert.AreEqual(10 * 16, result);
        }

        [TestMethod]
        [ExpectedException(typeof(TaskCanceledException))]
        public async Task AsTask_NoParameter_Cancelled()
        {
            await new Func<int>(GetZeroCancelled).AsTask();
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), AllowDerivedTypes = true)]
        public async Task AsTask_NoParameter_Exception()
        {
            await new Func<int>(GetZeroException).AsTask();
        }

        private int GetNumber(int i)
        {
            return i;
        }

        private int GetNumber(int i1, int i2)
        {
            return i1 + i2;
        }

        private int GetNumber(int i1, int i2, int i3)
        {
            return i1 + i2 + i3;
        }

        private int GetNumber(int i1, int i2, int i3, int i4)
        {
            return i1 + i2 + i3 + i4;
        }

        private int GetNumber(int i1, int i2, int i3, int i4, int i5)
        {
            return i1 + i2 + i3 + i4 + i5;
        }

        private int GetNumber(int i1, int i2, int i3, int i4, int i5, int i6)
        {
            return i1 + i2 + i3 + i4 + i5 + i6;
        }

        private int GetNumber(int i1, int i2, int i3, int i4, int i5, int i6, int i7)
        {
            return i1 + i2 + i3 + i4 + i5 + i6 + i7;
        }

        private int GetNumber(int i1, int i2, int i3, int i4, int i5, int i6, int i7, int i8)
        {
            return i1 + i2 + i3 + i4 + i5 + i6 + i7 + i8;
        }

        private int GetNumber(int i1, int i2, int i3, int i4, int i5, int i6, int i7, int i8, int i9)
        {
            return i1 + i2 + i3 + i4 + i5 + i6 + i7 + i8 + i9;
        }

        private int GetNumber(int i1, int i2, int i3, int i4, int i5, int i6, int i7, int i8, int i9, int i10)
        {
            return i1 + i2 + i3 + i4 + i5 + i6 + i7 + i8 + i9 + i10;
        }

        private int GetNumber(int i1, int i2, int i3, int i4, int i5, int i6, int i7, int i8, int i9, int i10, int i11)
        {
            return i1 + i2 + i3 + i4 + i5 + i6 + i7 + i8 + i9 + i10 + i11;
        }

        private int GetNumber(int i1, int i2, int i3, int i4, int i5, int i6, int i7, int i8, int i9, int i10, int i11,
            int i12)
        {
            return i1 + i2 + i3 + i4 + i5 + i6 + i7 + i8 + i9 + i10 + i11 + i12;
        }

        private int GetNumber(int i1, int i2, int i3, int i4, int i5, int i6, int i7, int i8, int i9, int i10, int i11,
            int i12, int i13)
        {
            return i1 + i2 + i3 + i4 + i5 + i6 + i7 + i8 + i9 + i10 + i11 + i12 + i13;
        }

        private int GetNumber(int i1, int i2, int i3, int i4, int i5, int i6, int i7, int i8, int i9, int i10, int i11,
            int i12, int i13, int i14)
        {
            return i1 + i2 + i3 + i4 + i5 + i6 + i7 + i8 + i9 + i10 + i11 + i12 + i13 + i14;
        }

        private int GetNumber(int i1, int i2, int i3, int i4, int i5, int i6, int i7, int i8, int i9, int i10, int i11,
            int i12, int i13, int i14, int i15)
        {
            return i1 + i2 + i3 + i4 + i5 + i6 + i7 + i8 + i9 + i10 + i11 + i12 + i13 + i14 + i15;
        }

        private int GetNumber(int i1, int i2, int i3, int i4, int i5, int i6, int i7, int i8, int i9, int i10, int i11,
            int i12, int i13, int i14, int i15, int i16)
        {
            return i1 + i2 + i3 + i4 + i5 + i6 + i7 + i8 + i9 + i10 + i11 + i12 + i13 + i14 + i15 + i16;
        }

        private int GetZero()
        {
            return 0;
        }

        private int GetZeroCancelled()
        {
            throw new OperationCanceledException();
        }

        private int GetZeroException()
        {
            throw new InvalidOperationException();
        }
    }
}