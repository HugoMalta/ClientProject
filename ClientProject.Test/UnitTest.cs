using ClientProject.Controllers;
using ClientProject.Data.Interface;
using ClientProject.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Hosting;
using System.Web.Http.Routing;

namespace ClientProject.Test
{
    [TestClass()]
    public class UnitTest
    {
        /// <summary>
        /// Get HttpRequestMessage default.
        /// </summary>
        /// <returns></returns>
        private HttpRequestMessage GetRequestMoq()
        {
            HttpRequestMessage requestMoq = new HttpRequestMessage();
            requestMoq.Properties[HttpPropertyKeys.HttpConfigurationKey] = new HttpConfiguration();
            requestMoq.Properties[HttpPropertyKeys.HttpRouteDataKey] = new HttpRouteData(new HttpRoute());
            return requestMoq;
        }

        /// <summary>
        /// Get Instance of ClientModel Populated to Be Used as Default.
        /// </summary>
        /// <returns></returns>
        private ClientModel GetClientModel1()
        {
            ClientModel client = new ClientModel();
            client.City = "Belo Horizonte";
            client.Country = "Brasil";
            client.Cpf = "92486487204"; //https://www.geradordecpf.org/
            client.Email = "hugomalta@gmail.com";
            client.MaritalStatus = 1;
            client.Name = "Hugo Malta";
            client.Number = "41";
            client.Phones = "67657567";
            client.State = "Minas Gerais";
            client.Street = "Abcd";
            client.ZipCode = "456";

            return client;
        }

        /// <summary>
        /// Get Instance of ClientModel Populated to Be Used as Default
        /// </summary>
        /// <returns></returns>
        private ClientModel GetClientModel2()
        {
            ClientModel client = new ClientModel();
            client.City = "Ibirité";
            client.Country = "Brasil";
            client.Cpf = "57626589882"; //https://www.geradordecpf.org/
            client.Email = "hugomalta@outlook.com";
            client.MaritalStatus = 2;
            client.Name = "Hugo Leandro Malta";
            client.Number = "1, bloco 2, ap 3";
            client.Phones = "234234223";
            client.State = "Minas Gerais";
            client.Street = "Três";
            client.ZipCode = "32400000";

            return client;
        }

        /// <summary>
        /// Get Instance of ClientModel Populated to Be Used as Default
        /// </summary>
        /// <returns></returns>
        private ClientModel GetClientModelErrorCpf()
        {
            ClientModel client = new ClientModel();
            client.City = "Ibirité";
            client.Country = "Brasil";
            client.Cpf = "12121212121"; //https://www.geradordecpf.org/
            client.Email = "hugomalta@outlook.com";
            client.MaritalStatus = 2;
            client.Name = "Hugo Leandro Malta";
            client.Number = "1256, bloco 4, ap 204";
            client.Phones = "986365084";
            client.State = "Minas Gerais";
            client.Street = "Hum";
            client.ZipCode = "32400000";

            return client;
        }

        /// <summary>
        /// Tests the ClientController.Get method.
        /// </summary>
        [TestMethod()]
        public void GetTest()
        {
            Mock<IClientRepository> moqClientRepository = new Mock<IClientRepository>();

            ClientController clientControllerMoq = new ClientController(moqClientRepository.Object);

            clientControllerMoq.Request = GetRequestMoq();

            HttpResponseMessage a = clientControllerMoq.Get();

            moqClientRepository.Verify(x => x.GetAll());
        }

        /// <summary>
        /// Tests the ClientController.Get(id) method.
        /// </summary>
        [TestMethod()]
        public void GetByIdTest()
        {
            Mock<IClientRepository> moqClientRepository = new Mock<IClientRepository>();

            ClientController clientControllerMoq = new ClientController(moqClientRepository.Object);

            clientControllerMoq.Request = GetRequestMoq();

            HttpResponseMessage a = clientControllerMoq.Get(1);

            moqClientRepository.Verify(x => x.GetById(1));
        }

        /// <summary>
        /// Tests the ClientController.Post method.
        /// </summary>
        [TestMethod()]
        public void PostTest()
        {
            Mock<IClientRepository> moqClientRepository = new Mock<IClientRepository>();

            ClientController clientControllerMoq = new ClientController(moqClientRepository.Object);

            clientControllerMoq.Request = GetRequestMoq();

            ClientModel client = GetClientModel1();

            HttpResponseMessage responseControllerMoq = clientControllerMoq.Post(client);

            moqClientRepository.Verify(x => x.Create(client));

            //ClientModel client2 = GetClientModel2();
            //oqClientRepository.Verify(x => x.Create(client2));
        }

        /// <summary>
        /// Tests the ClientController.Put method.
        /// </summary>
        [TestMethod()]
        public void PutTest()
        {
            Mock<IClientRepository> moqClientRepository = new Mock<IClientRepository>();

            ClientController clientControllerMoq = new ClientController(moqClientRepository.Object);

            clientControllerMoq.Request = GetRequestMoq();

            ClientModel client = GetClientModel1();


            HttpResponseMessage a = clientControllerMoq.Put(client);

            moqClientRepository.Verify(x => x.Update(client));

            //ClientModel client2 = GetClientModel2();
            //moqClientRepository.Verify(x => x.Update(client2));
        }

        /// <summary>
        /// Tests the ClientController.Delete method.
        /// </summary>
        [TestMethod()]
        public void DeleteTest()
        {
            Mock<IClientRepository> moqClientRepository = new Mock<IClientRepository>();

            ClientController clientControllerMoq = new ClientController(moqClientRepository.Object);

            clientControllerMoq.Request = GetRequestMoq();

            HttpResponseMessage a = clientControllerMoq.Delete("88888888888");

            moqClientRepository.Verify(x => x.DeleteByCpf("88888888888"));
        }

        /// <summary>
        /// Check exception for invalid cpf.
        /// </summary>
        [TestMethod()]
        public void CpfInvalidTest()
        {
            Mock<IClientRepository> moqClientRepository = new Mock<IClientRepository>();

            ClientController clientControllerMoq = new ClientController(moqClientRepository.Object);

            clientControllerMoq.Request = GetRequestMoq();

            ClientModel client = GetClientModel1();
            client.Cpf = "12121212121";

            HttpResponseMessage responseControllerMoq = clientControllerMoq.Post(client);

            bool statusNotAcceptable = responseControllerMoq.StatusCode == System.Net.HttpStatusCode.NotAcceptable;
            if (statusNotAcceptable)
            {
                var error = ((ObjectContent)responseControllerMoq.Content).Value.ToString();
                Assert.IsTrue(statusNotAcceptable, "CPF validation presented error as expected. See error: " + error);
            }
            else
            {
                Assert.Fail("CPF validation failed because expected errors. CPF used in validation: " + client.Cpf);
            }
        }

        /// <summary>
        /// Check exception for invalid email.
        /// </summary>
        [TestMethod()]
        public void Email1InvalidTest()
        {
            Mock<IClientRepository> moqClientRepository = new Mock<IClientRepository>();

            ClientController clientControllerMoq = new ClientController(moqClientRepository.Object);

            clientControllerMoq.Request = GetRequestMoq();

            ClientModel client = GetClientModel1();
            client.Email = "ç@é.com.br";

            HttpResponseMessage responseControllerMoq = clientControllerMoq.Post(client);

            bool statusNotAcceptable = responseControllerMoq.StatusCode == System.Net.HttpStatusCode.NotAcceptable;
            if (statusNotAcceptable)
            {
                var error = ((ObjectContent)responseControllerMoq.Content).Value.ToString();
                Assert.IsTrue(statusNotAcceptable, "Email Validation returned error as expected. See error: " + error);
            }
            else
            {
                Assert.Fail("Email validation failed because expected errors. E-mail used in validation: " + client.Email);
            }
        }

        /// <summary>
        /// Check exception for invalid email.
        /// </summary>
        [TestMethod()]
        public void Email2InvalidTest()
        {
            Mock<IClientRepository> moqClientRepository = new Mock<IClientRepository>();

            ClientController clientControllerMoq = new ClientController(moqClientRepository.Object);

            clientControllerMoq.Request = GetRequestMoq();

            ClientModel client = GetClientModel1();
            client.Email = "A@B";

            HttpResponseMessage responseControllerMoq = clientControllerMoq.Post(client);

            bool statusNotAcceptable = responseControllerMoq.StatusCode == System.Net.HttpStatusCode.NotAcceptable;
            if (statusNotAcceptable)
            {
                var error = ((ObjectContent)responseControllerMoq.Content).Value.ToString();
                Assert.IsTrue(statusNotAcceptable, "Email validation returned error as expected. See error: " + error);
            }
            else
            {
                Assert.Fail("Email validation failed because expected errors. E-mail used in validation: " + client.Email);
            }
        }
    }
}
