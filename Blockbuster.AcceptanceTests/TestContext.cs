using Blockbuster.API;
using Blockbuster.Application.Movies.TransferObjects;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Blockbuster.AcceptanceTests
{
    public class TestContext
    {
        public WebApplicationFactory<Program> ApplicationFactory { get; set; }


        public HttpClient Client { get; set; }


        public HttpResponseMessage ResponseMessage { get; set; }


        public MovieResponse MovieResponse {  get; set; }


        public MovieInfoResponse MovieInfo { get; set; }

        public string MovieId {  get; set; }
    }
}
