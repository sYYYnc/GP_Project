using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;


namespace NavegationTests
{
    [TestClass]
    public class UnitTest1
    {
        /* /// <summary>
        /// Método responsável por testar a página ./Manage/Index referente ao estado da candidatura, 
        /// informações relativas à bolsa e informações relativas ao utilizador logado.
        /// </summary>
        [TestMethod]
        public void testEstadoCandidatura()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Navigate().GoToUrl("https://projetog6.azurewebsites.net/");
            driver.Manage().Window.Maximize();

            autenticarUtilizador(driver, "edu__correia@hotmail.com", "123456789");
            driver.FindElement(By.XPath("//a[@href='/Manage/Index']")).Click();

            Assert.AreEqual("Etapas da candidatura", driver.FindElement(By.TagName("h2")).Text);
            Assert.AreEqual("Bolsa", driver.FindElement(By.CssSelector("#bolsa h2")).Text);
        } */ 

        /// <summary>
        /// Método responsável por testar a autenticação de um utilizador.
        /// </summary>
        [TestMethod]
        public void testAutenticarUser()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Navigate().GoToUrl("http://it4u.azurewebsites.net/");
            driver.Manage().Window.Maximize();

            autenticarUtilizador(driver, "edu__correia@hotmail.com", "123456789");
        }

        /// <summary>
        /// Método que autentica um utilizador através do objeto do tipo WebDriver.
        /// </summary>
        public void autenticarUtilizador(IWebDriver driver, string email, string password)
        {
            driver.FindElement(By.XPath("//a[@href='/Account/Login']")).Click();
            driver.FindElement(By.Id("Email")).SendKeys(email);
            driver.FindElement(By.Id("Password")).SendKeys(password);
            driver.FindElement(By.ClassName("btn")).SendKeys(Keys.Enter);
        }

        /// <summary>
        /// Método que preenche o form para adicionar um projeto.
        /// </summary>
        private void fillFormAddProjeto(IWebDriver driver, string nome, string tec, string des)
        {
            driver.FindElement(By.XPath("//a[@href='/Projetos/Create']")).Click();
            driver.FindElement(By.Id("ProjectName")).SendKeys(nome);
            driver.FindElement(By.Id("Technology")).SendKeys(tec);
            driver.FindElement(By.Id("Description")).SendKeys(des);
        }

        /// <summary>
        /// Método responsável por testar a página ./Projetos. Página responsável por mostrar
        /// os projetos disponiveis, adicionar projetos, download e detalhes de cada projeto.
        /// </summary>
        [TestMethod]
        public void testProjetosDisponiveisSLogin()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Navigate().GoToUrl("http://it4u.azurewebsites.net/");
            driver.Manage().Window.Maximize();

            driver.FindElement(By.XPath("//a[@href='/Projetos']")).Click();


        }

        /// <summary>
        /// Método responsável por testar a página ./Candidaturas/Create referente à criação
        /// de uma nova candidatura por parte do utilizador logado.
        /// </summary>
        [TestMethod]
        public void testAdicaoProjeto()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Navigate().GoToUrl("http://it4u.azurewebsites.net/");
            driver.Manage().Window.Maximize();

            autenticarUtilizador(driver, "edu__correia@hotmail.com", "123456789");
            driver.FindElement(By.XPath("//a[@href='/Projetos']")).Click();

            fillFormAddProjeto(driver, "ProjetoTeste", "TecnologiaTeste", "DescriçaoTeste");

        }

        /* 

        /// <summary>
        /// Método responsável por testar a página ./Candidaturas/Create referente à criação
        /// de uma nova candidatura por parte do utilizador logado.
        /// </summary>
        [TestMethod]
        public void testNovaCandidatura()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Navigate().GoToUrl("http://it4u.azurewebsites.net/");
            driver.Manage().Window.Maximize();

            autenticarUtilizador(driver, "edu__correia@hotmail.com", "123456789");
            driver.FindElement(By.XPath("//a[@href='/Projetos']")).Click();

            IWebElement identifier = driver.FindElement(By.Id("ProgramaDestino_ProgramaMobilidadeID"));
            new SelectElement(driver.FindElement(By.Id("ProgramaDestino_ProgramaMobilidadeID"))).SelectByIndex(1);
            new SelectElement(driver.FindElement(By.Id("destinos"))).SelectByValue("1");
        }

        /// <summary>
        /// Método responsável por testar a página ./ProgramasMobilidade/Apresentacao referente à página
        /// de apresentação dos programas de mobilidade cobertos pelo CIMOB com o utilizador logado.
        /// </summary>
        [TestMethod]
        public void testApresentacaoCLogin()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Navigate().GoToUrl("https://projetog6.azurewebsites.net/");
            driver.Manage().Window.Maximize();

            autenticarUtilizador(driver, "edu__correia@hotmail.com", "123456789");
            driver.FindElement(By.XPath("//a[@href='/ProgramasMobilidade/Apresentacao']")).Click();
        }

        /// <summary>
        /// Método responsável por testar a página ./ProgramasMobilidade/Apresentacao referente à página
        /// de apresentação dos programas de mobilidade cobertos pelo CIMOB sem que o utilizador esteja logado.
        /// </summary>
        [TestMethod]
        public void testApresentacaoSLogin()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Navigate().GoToUrl("https://projetog6.azurewebsites.net/");
            driver.Manage().Window.Maximize();

            driver.FindElement(By.XPath("//a[@href='/ProgramasMobilidade/Apresentacao']")).Click();
        }  

    */

        
    }
}

  

