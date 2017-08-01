using System.Web.Http;

namespace Provider.Controllers
{
    public class CardController : ApiController
    {
        public IHttpActionResult Get(int id)
        {
            var card = new Card {Id = 1, DisplayName = "VISA"};
            return Ok(card);
        }
    }

    public class Card
    {
        public int Id { get; set; }
        public string DisplayName { get; set; }
    }
}
