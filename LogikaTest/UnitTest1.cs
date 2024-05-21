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
    }
}
