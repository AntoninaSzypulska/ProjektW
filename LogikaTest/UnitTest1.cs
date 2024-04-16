using Dane;
using Logika;

namespace LogikaTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestCreate()
        {
            Logika.Logika logika = new Logika.Logika();

            int amount = 5;

            logika.create(amount);

            Assert.AreEqual(amount, logika.kulkiRepository.kulkiL.Count);

        }

        [TestMethod]
        public void TestCreateKulka_RandomCoordinates()
        {

            Logika.Logika logika = new Logika.Logika();

            Kulka kulka = logika.createKulka();

            Assert.IsNotNull(kulka);
            Assert.IsTrue(kulka.X >= 0 && kulka.X < logika.plansza.GetWidth);
            Assert.IsTrue(kulka.Y >= 0 && kulka.Y < logika.plansza.GetHeight);
        }

        [TestMethod]
        public void TestMovekulka()
        {

            Logika.Logika logika = new Logika.Logika();

            Kulka kulka = logika.createKulka();

            Assert.IsNotNull(kulka);

            float oldX = kulka.X;
            float oldY = kulka.Y;
            logika.MoveToNextPosition(kulka);
            float newX = kulka.X;
            float newY = kulka.Y;
            Assert.AreNotEqual(oldX, newX);
            Assert.AreNotEqual(oldY, newY);
        }


        [TestMethod]
        public void TestNextPosition()
        {

            Logika.Logika logika = new Logika.Logika();

            (float xNext, float yNext) = logika.NextPosition();

            Assert.IsNotNull(xNext);
            Assert.IsNotNull(yNext);

        }
    }
}
