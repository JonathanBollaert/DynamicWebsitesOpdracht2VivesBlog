using Microsoft.AspNetCore.Mvc;
using VivesBlog.Dto.Requests;
using VivesBlog.Dto.Results;
using VivesBlog.Sdk;

namespace VivesBlog.Ui.Mvc.Controllers
{
    public class PeopleController : Controller
    {
        private readonly PersonSdk _personSdk;

        public PeopleController(PersonSdk personSdk)
        {
            _personSdk = personSdk;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var people = await _personSdk.Find();

            return View(people);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PersonRequest request)
        {
            if (!ModelState.IsValid)
            {
                var person = new PersonResult
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email
                };
                return View(person);
            }

            await _personSdk.Create(request);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute]int id)
        {
            var result = await _personSdk.Get(id);

            if (!result.IsSuccess || result.Data is null)
            {
                return RedirectToAction("Index");
            }

            return View(result.Data);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute]int id, [FromForm]PersonRequest request)
        {
            if (!ModelState.IsValid)
            {
                var result = await _personSdk.Get(id);
                if (!result.IsSuccess || result.Data is null)
                {
                    return RedirectToAction("Index");
                }

                var person = result.Data;
                person.FirstName = request.FirstName;
                person.LastName = request.LastName;
                person.Email = request.Email;

                return View(person);
            }

            await _personSdk.Update(id, request);

            return RedirectToAction("Index");
        }


        [HttpPost("/[controller]/Delete/{id:int?}"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _personSdk.Delete(id);

            return RedirectToAction("Index");
        }
    }
}
