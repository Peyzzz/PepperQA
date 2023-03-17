using Microsoft.Playwright;

namespace PepperQA.Pages
{
    public class HomePage
    {
        private IPage _page;

        public HomePage(IPage page) => this._page = page;

        public string Title => ".product-title";

        public async Task SelectDefineProduct(string name)
        {
            var products = await _page.QuerySelectorAllAsync(Title);

            foreach (var product in products)
            {
                if (await product.InnerTextAsync() == name)
                {
                    await product.ClickAsync();
                    break;
                }
            }
        }
    }
}
