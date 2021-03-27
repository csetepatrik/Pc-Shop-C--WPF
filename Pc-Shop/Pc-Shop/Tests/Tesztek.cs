using Model;
using NUnit.Framework;
using PcShopDataBase;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestFixture]
    public class Tesztek
    {
        /// <summary>
        /// Adatbázis függvények.
        /// </summary>
        private Functions func = new Functions();

        /// <summary>
        /// Adatelérés az Servicen keresztül.
        /// </summary>
        private DataService dser = new DataService();

        /// <summary>
        /// Loginhoz szükséges objektum.
        /// </summary>
        private LoginService log = new LoginService();

        /// <summary>
        /// Adott megrendeléshez hány PC van kész, egy sincs.
        /// </summary>
        [Test]
        public void PC_hany_van_kesz_0()
        {
            Megrendeles megrendeles = new Megrendeles(new List<Model.PC>(), 1, "", "", Status.Elvallalva);
            var result = func.PcToOrderd(megrendeles);
            Assert.That(result, Is.EqualTo(0));
        }

        /// <summary>
        /// Adott rendeléshez tartozó sztgépek, üres lista.
        /// </summary>
        [Test]
        public void PC_Rendeleshez_ID()
        {
            var result = func.OrderToPc(0);
            Assert.That(result, Is.EqualTo(new List<Model.PC>()));
        }

        /// <summary>
        /// Adott rendeléshez tartozó sztgépek, létezik-e ilyen.
        /// </summary>
        [Test]
        public void Rendeleshez_Tartozo_biciklik_1_test()
        {
            var result = func.OrderToPc(1);
            Assert.IsNotEmpty(result);
        }

        /// <summary>
        /// adott megrendelésnél hány bicikli készült el, 0.
        /// </summary>
        [Test]
        public void Megrendeleshez_Hany_PC()
        {
            Megrendeles megrendeles = new Megrendeles(new List<Model.PC>(), 1, "", "", Status.Elvallalva);
            var result = dser.Megrendeleshez_Mennyi_PC_Done(megrendeles);

            Assert.That(result, Is.EqualTo(0));
        }
      
        /// <summary>
        /// Sikeres belépés esetén nem dob exceptiont.
        /// </summary>
        [Test]
        public void Login_test_siker()
        {
            Assert.DoesNotThrow(() => { User result = log.Login("btamas", "1234"); });
        }

        /// <summary>
        /// Sikeretelen belépés esetén dob-e exceptiont.
        /// </summary>
        [Test]
        public void Login_test_sikertelen()
        {
            Assert.Throws(typeof(InvalidLoginException), () => { User u = log.Login("k", "j"); });
        }

        /// <summary>
        /// Teszteli a titkosítást.
        /// </summary>
        [Test]
        public void Titkosits_test()
        {
            string result = log.Titkosit("1234");
            Assert.That(result, Is.EqualTo("6789"));
        }

        /// <summary>
        /// A függvény sosem szabad, hogy üres listát adjon vissza.
        /// </summary>
        [Test]
        public void Kelloalkatresz_meghatarozas_nemures_lista_test()
        {
            var result = dser.KelloAlkMeghatarozas(new List<Model.PC>());
            Assert.IsNotEmpty(result);
        }

        [Test]
        public void PostitiveTest()
        {
            int x = 7;
            int y = 7;

            Assert.AreEqual(x, y);
        }
    }
}
