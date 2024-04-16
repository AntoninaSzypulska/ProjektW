using Dane;

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

            float oldX = kulka.getX();
            float oldY = kulka.getY();
            logika.MoveToNextPosition(kulka);
            float newX = kulka.getX();
            float newY = kulka.getY();
            Assert.AreNotEqual(oldX, newX);
            Assert.AreNotEqual(oldY, newY);
        }
    }
}