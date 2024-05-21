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

        [TestMethod]
        public void TestRemove()
        {
            Logika.Logika logika = new Logika.Logika();
            int amount = 5;
            logika.create(amount);

            logika.remove();

            Assert.AreEqual(0, logika.kulkiRepository.kulkiL.Count);
        }

        [TestMethod]
        public void TestIsColliding()
        {
            Logika.Logika logika = new Logika.Logika();
            KulkiRepository repository = logika.kulkiRepository;

            Kulka kulka1 = new Kulka(0, 0, 0, 0, 10, 20);
            Kulka kulka2 = new Kulka(30, 0, 30, 0, 10, 20);
            Kulka kulka3 = new Kulka(15, 0, 15, 0, 10, 20);

            repository.add(kulka1);

            bool collision2 = logika.IsColliding(kulka2);
            Assert.IsFalse(collision2);

            bool collision3 = logika.IsColliding(kulka3);
            Assert.IsTrue(collision3);
        }

        [TestMethod]
        public void TestMoveToNextPosition()
        {

            Logika.Logika logika = new Logika.Logika();
            Kulka kulka1 = new Kulka(60, 60, 0, 60, 50, 20);
            Kulka kulka2 = new Kulka(100, 100, 0, 0, 50, 20);
            logika.kulkiRepository.add(kulka1);
            logika.kulkiRepository.add(kulka2);

            logika.MoveToNextPosition(kulka1);
            logika.MoveToNextPosition(kulka2);

            Assert.IsTrue(kulka1.X != 0 || kulka1.Y != 60);
            Assert.IsTrue(kulka2.X != 100 || kulka2.Y != 100);
        }

        [TestMethod]
        public void TestOdbicie()
        {
            Logika.Logika logika = new Logika.Logika();
            Kulka kulka1 = new Kulka(0, 0, 0, 0, 50, 20);
            Kulka kulka2 = new Kulka(100, 0, 0, 0, 50, 20);
            kulka1.VelocityX = 1;
            kulka2.VelocityX = -1;
            float oldVelocityX1 = kulka1.VelocityX;
            float oldVelocityX2 = kulka2.VelocityX;

            logika.odbicie(kulka1, kulka2);

            Assert.AreEqual(oldVelocityX1 * -1, kulka1.VelocityX);
            Assert.AreEqual(oldVelocityX2 * -1, kulka2.VelocityX);
        }
    }
}
