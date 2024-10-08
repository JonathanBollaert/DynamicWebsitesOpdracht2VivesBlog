using Microsoft.AspNetCore.Mvc;
using VivesBlog.Dto.Requests;
using VivesBlog.Dto.Results;
using VivesBlog.Sdk;

namespace VivesBlog.Ui.Mvc.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly ArticleSdk _articleSdk;
        private readonly PersonSdk _personSdk;

        public ArticlesController(
            ArticleSdk articleSdk,
            PersonSdk personSdk)
        {
            _articleSdk = articleSdk;
            _personSdk = personSdk;
        }
        public async Task<IActionResult> Index()
        {
            var articles = await _articleSdk.Find();
            return View(articles);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return await CreateEditView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ArticleRequest request)
        {
            if (!ModelState.IsValid)
            {
                // Reload the list of authors
                var authors = await _personSdk.Find();

                ViewBag.Authors = authors;

                var article = new ArticleResult
                {
                    Title = request.Title,
                    Description = request.Description,
                    Content = request.Content,
                };
                return View(article);
            }

            await _articleSdk.Create(request);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute] int id)
        {
            var result = await _articleSdk.Get(id);

            if (!result.IsSuccess || result.Data is null)
            {
                return RedirectToAction("Index");
            }

            return await CreateEditView(result.Data);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromForm] ArticleRequest request)
        {
            if (!ModelState.IsValid)
            {
                var result = await _articleSdk.Get(id);
                if (!result.IsSuccess || result.Data is null)
                {
                    return RedirectToAction("Index");
                }
                var article = result.Data;
                article.Title = request.Title;
                article.Description = request.Description;

                return await CreateEditView(article);
            }

            await _articleSdk.Update(id, request);

            return RedirectToAction("Index");
        }

        [HttpPost("/[controller]/Delete/{id:int?}"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _articleSdk.Delete(id);

            return RedirectToAction("Index");
        }


        private async Task<IActionResult> CreateEditView(ArticleResult? article = null)
        {
            var authors = await _personSdk.Find();
            ViewBag.Authors = authors;

            return View(article);
        }
    }
}
