using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Portoa.Persistence;

namespace Portoa.Web.Rest.MvcTest.Controllers {

	public class User : Entity<int>, IDtoMappable<UserDto> {
		public User() {
			Created = DateTime.Now;
		}

		public virtual string Username { get; set; }
		public virtual string Email { get; set; }
		public virtual DateTime Created { get; set; }

		public virtual UserDto ToDto() {
			return new UserDto {
				Id = Id,
				Username = Username,
				Email = Email,
				Created = Created
			};
		}
	}

	public class UserDto : IdentifiableDto {
		public string Username { get; set; }
		public string Email { get; set; }
		public DateTime Created { get; set; }
	}

	public interface IRestService {
		IEnumerable<UserDto> GetUsers(RestRequest request);
	}

	public class RestService : RestServiceBase, IRestService {
		private readonly IRepository<User> repository;

		public RestService(IRepository<User> repository) {
			this.repository = repository;
		}

		[UnitOfWork]
		public IEnumerable<UserDto> GetUsers(RestRequest request) {
			return GetRecords<User, UserDto>(request, repository.Records);
		}
	}

	public class RestController : Controller {
		private readonly IRestService restService;

		public RestController(IRestService restService) {
			this.restService = restService;
		}

		public ActionResult Index() {
			return View();
		}

		public ActionResult ListUsers(RestRequest request) {
			return Json(restService.GetUsers(request), JsonRequestBehavior.AllowGet);
		}

	}
}
