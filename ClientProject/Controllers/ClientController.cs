using ClientProject.Data.Interface;
using ClientProject.Models;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ClientProject.Controllers
{
    public class ClientController : ApiController
    {
        private IClientRepository _repositorio { get; }

        public ClientController(IClientRepository repositorio)
        {
            _repositorio = repositorio;
        }

        public HttpResponseMessage Get()
        {
            try
            {
                var listClient = _repositorio.GetAll().ToList();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, listClient);

                return response;
            }
            catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            
        }

        public HttpResponseMessage Get(int id)
        {
            try
            {
                var client = _repositorio.GetById(id);
                HttpResponseMessage response = Request.CreateResponse<ClientModel>(HttpStatusCode.OK, client);
                return response;
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        public HttpResponseMessage Post([FromBody] ClientModel client)
        {
            this.Validate(client);

            if (ModelState.IsValid)
            {
                ClientModel result = _repositorio.Create(client);
                try
                {
                    return Request.CreateResponse(result != null && result.Id > 0 ? HttpStatusCode.OK : HttpStatusCode.NotModified);
                }
                catch
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError);
                }
            }
            else
            {
                string error = string.Empty;
                foreach(var item in ModelState.Values)
                {
                    if(item.Errors != null && item.Errors.Count > 0)
                    {
                        error += item.Errors[0].ErrorMessage + "¿";
                    }
                }

                error = error.Length > 0 ? error.Substring(0, error.Length - 1) : error;
                return Request.CreateResponse(HttpStatusCode.NotAcceptable, error);
            }
        }

        public HttpResponseMessage Put(ClientModel client)
        {
            this.Validate(client);

            if (ModelState.IsValid)
            {

                bool result = false;
                try
                {
                    result = _repositorio.Update(client);
                    return Request.CreateResponse(result ? HttpStatusCode.OK : HttpStatusCode.NotModified);
                }
                catch
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError);
                }
            } else
            {
                return Request.CreateResponse(HttpStatusCode.NotAcceptable);
            }

        }

        public HttpResponseMessage Delete(string cpf)
        {
            bool result = false;

            try
            {
                result = _repositorio.DeleteByCpf(cpf);
                return Request.CreateResponse(result ? HttpStatusCode.OK : HttpStatusCode.NotModified);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    }
}
