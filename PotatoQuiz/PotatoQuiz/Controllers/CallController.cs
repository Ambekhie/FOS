using OrderSystem.Models;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System;

namespace OrderSystem
{
    [Authorize]
    [RoutePrefix("api/call")]
    public class CallController : ApiController
    {
        private ApplicationDbContext db;
        public CallController()
        {
            this.db = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.db.Dispose();
            }

            base.Dispose(disposing);
        }
        private async Task<Call> NextCallAsync(int id)
        {
            if (this.db.Calls.Count() == 0)
            {
                return null;
            }

            if (id < 0 || id >= this.db.Calls.Count())
            {
                throw new ArgumentOutOfRangeException();
            }

            if (id == this.db.Calls.Count())
            {
                return null;
            }

            return await this.db.Calls
                .Where(o => o.Id == id + 1)
                .FirstOrDefaultAsync();
        }

        // GET api/Order
        [HttpGet]
        [ResponseType(typeof(Call))]
        public async Task<IHttpActionResult> Get()
        {
            Call call = await this.LatestCallAsync();

            if (call == null)
            {
                return this.NotFound();
            }

            return this.Ok(call);
        }

        private async Task<Call> LatestCallAsync()
        {
            return await this.db.Calls
                .FirstOrDefaultAsync();
        }

        // GET api/Order
        [HttpPost]
        [Route("publish/{userID}")]
        [ResponseType(typeof(Call))]
        public async Task<IHttpActionResult> Publish([FromBody]Call call, string userID)
        {
            Call result = await this.PublishAsync(call, userID);

            if (result == null)
            {
                return this.NotFound();
            }

            return this.Ok(result);
        }

        private async Task<Call> PublishAsync(Call call, string userID)
        {
            var result = this.db.Calls.Add(new Call() {
                Restaurant = call.Restaurant,
                Time = call.Time,
                UserID = userID
            });
            var changes = await this.db.SaveChangesAsync();
            if (changes == 0)
            {
                return null;
            }

            return result;
        }

        // GET api/Order
        [HttpGet]
        [Route("next/{id}")]
        [ResponseType(typeof(Call))]
        public async Task<IHttpActionResult> GetNext(int id)
        {
            Call call = await this.NextCallAsync(id);

            if (call == null)
            {
                return this.NotFound();
            }

            return this.Ok(call);
        }

        [Route("prev/{id}")]
        [HttpGet]
        [ResponseType(typeof(Call))]
        public async Task<IHttpActionResult> GetPrev(int id)
        {
            Call call = await this.PrevCallAsync(id);

            if (call == null)
            {
                return this.NotFound();
            }

            return this.Ok(call);
        }

        private async Task<Call> PrevCallAsync(int id)
        {
            if (this.db.Calls.Count() == 0)
            {
                return null;
            }

            if (id <= 0 || id > this.db.Calls.Count())
            {
                throw new ArgumentOutOfRangeException();
            }

            if (id == 1)
            {
                return null;
            }

            return await this.db.Calls
                .Where(o => o.Id == id - 1)
                .FirstOrDefaultAsync();
        }
    }
}
