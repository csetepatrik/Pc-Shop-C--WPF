namespace Tesztek
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Model;
    using NUnit.Framework;
    using PcShopDataBase;
    using Services;

    [TestFixture]

    /// <summary>
    /// Tesztek
    /// </summary>
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
        /// service
        /// </summary>
        private MessageService service = new MessageService("Bakó Tamás");

        /// <summary>
        /// Adott megrendeléshez hány PC van kész, egy sincs.
        /// </summary>
        [Test]
        public void PC_hany_van_kesz_0()
        {
            Megrendeles megrendeles = new Megrendeles(new List<Model.PC>(), 1, string.Empty, string.Empty, Status.Elvallalva);
            var result = this.func.PcToOrderd(megrendeles);
            Assert.That(result, Is.EqualTo(0));
        }

        /// <summary>
        /// Adott rendeléshez tartozó sztgépek, üres lista.
        /// </summary>
        [Test]
        public void PC_Rendeleshez_ID()
        {
            var result = this.func.OrderToPc(0);
            Assert.That(result, Is.EqualTo(new List<Model.PC>()));
        }

        /// <summary>
        /// Adott rendeléshez tartozó sztgépek, létezik-e ilyen.
        /// </summary>
        [Test]
        public void Rendeleshez_Tartozo_pck_1_test()
        {
            var result = this.func.OrderToPc(1);
            Assert.IsNotEmpty(result);
        }

        /// <summary>
        /// adott megrendelésnél hány pc készült el, 0.
        /// </summary>
        [Test]
        public void Megrendeleshez_Hany_PC()
        {
            Megrendeles megrendeles = new Megrendeles(new List<Model.PC>(), 1, string.Empty, string.Empty, Status.Elvallalva);
            var result = this.dser.Megrendeleshez_Mennyi_PC_Done(megrendeles);

            Assert.That(result, Is.EqualTo(0));
        }

        /// <summary>
        /// Sikeres belépés esetén nem dob exceptiont.
        /// </summary>
        [Test]
        public void Login_test_siker()
        {
            Assert.DoesNotThrow(() => { User result = log.Login("1234", "btamas"); });
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
            string result = this.log.Titkosit("1234");
            Assert.That(result, Is.EqualTo("4f37c061f1854f9682f543fecb5ee9d652c803235970202de97c6e40c8361766"));
        }

        /// <summary>
        /// A függvény sosem szabad, hogy üres listát adjon vissza.
        /// </summary>
        [Test]
        public void Kelloalkatresz_meghatarozas_nemures_lista_test()
        {
            var result = this.dser.KelloAlkMeghatarozas(new List<Model.PC>());
            Assert.IsNotEmpty(result);
        }

        /// <summary>
        /// Alkatrészek nem empty
        /// </summary>
        [Test]
        public void GetAllAlkatreszTest_notempty()
        {
            var result = this.dser.GetAllAlkatresz();
            Assert.IsNotEmpty(result);
        }

        /// <summary>
        /// Berakja-e az üzenetet az adatbázisba
        /// </summary>
        [Test]
        public void SendUzenetTest()
        {
            Level level = new Level("Valaki", "Bakó Tamás", "Ez egy üzenet", DateTime.Now);
            service.SendLevel(level);
            List<Level> levelek = service.BejovoUzenetek;
            var result = from x in levelek where x.Felado == "Valaki" select x;
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Rendelhető alkatrészek nem üres.
        /// </summary>
        [Test]
        public void RendelhetoAlkatreszek_Nem_Ures()
        {
            List<Alkatresz> alkatreszek = this.dser.RendelhetoAlkatreszek();
            Assert.IsNotEmpty(alkatreszek);
        }

        /// <summary>
        /// Felveszi-e a rendelést az adatbázisba
        /// </summary>
        [Test]
        public void RendelesFelvetele_Test()
        {
            Megrendeles megrendeles = new Megrendeles(new List<Model.PC> { new Model.PC(PcTipus.Gamer, new List<Alkatresz>(), false, 200) }, 444, "Egy ember", "Egy dolgozó", Status.Nincs_elvallalva);
            this.dser.RendelesFelvetele(megrendeles);
            var query = from x in this.dser.GetAllMegrendeles() where x.Megrendelo == "Egy ember" select x.Sorszam;
            Assert.IsNotEmpty(query);
        }

        /// <summary>
        /// Ellenőrzi, hogy a felhasználók listája nem üres
        /// </summary>
        [Test]
        public void Felhasznalok_Nem_Ures()
        {
            Assert.IsNotEmpty(this.dser.Felhasznalok);
        }

        /// <summary>
        /// Ellenőrzi, hogy a PCUpdate funkció működik-e.
        /// </summary>
        [Test]
        public void PCUpdateKesz_Test()
        {
            ObservableCollection<Megrendeles> temp = this.dser.GetAllMegrendeles();
            this.dser.PCUpdate(temp[0].Pck[0]);
            temp = this.dser.GetAllMegrendeles();
            Assert.That(temp[0].Pck[0].Keszvan);
        }

        /// <summary>
        /// SendAlkatreszRendeles tesztje.
        /// </summary>
        [Test]
        public void GetAllLevel_Not_Empty()
        {
            Assert.IsNotEmpty(service.GetAllLevels());
        }

        /// <summary>
        /// Ellenőrzi, hogy a PC típusok lekérdezés nem üres.
        /// </summary>
        [Test]
        public void PCTipusok_Not_Empty()
        {
            Assert.IsNotEmpty(this.dser.PcTipusok());
        }

        /// <summary>
        /// Ellenőrzi, hogy az adott listára igaz-e, hogy az összes PC elkészült ==> Nem
        /// </summary>
        [Test]
        public void OsszesPCElkeszult_No()
        {
            ObservableCollection<Model.PC> pck = new ObservableCollection<Model.PC>()
            {
                new Model.PC(PcTipus.Gamer, new List<Alkatresz>(), false, 12345)
            };

            Assert.IsFalse(this.dser.OsszPCElkeszult(pck));
        }

        /// <summary>
        /// Ellenőrzi, hogy az adott listára igaz-e, hogy az összes PC elkészült ==> Nem
        /// </summary>
        [Test]
        public void OsszesPCElkeszult_Yes()
        {
            ObservableCollection<Model.PC> pck = new ObservableCollection<Model.PC>()
            {
                new Model.PC(PcTipus.Gamer, new List<Alkatresz>(), true, 12344)
            };

            Assert.IsTrue(this.dser.OsszPCElkeszult(pck));
        }

        /// <summary>
        /// Teszteli, hogy a kimenő üzenetek listája nem üres egy levél küldése során.
        /// </summary>
        [Test]
        public void KimenoUzenetek_Nem_Ures()
        {
            Level level = new Level("Bakó Tamás", "Bakó Tamás", "Ez egy üzenet", DateTime.Now);
            service.SendLevel(level);
            Assert.IsNotEmpty(service.KimenoUzenetek);
        }

        /// <summary>
        /// Teszteli, hogy a MaxID nem null.
        /// </summary>
        [Test]
        public void MaxID_Test()
        {
            Assert.NotZero(this.func.MaxId);
        }
    }
}
