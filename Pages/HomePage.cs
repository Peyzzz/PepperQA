using Microsoft.Playwright;
using PepperQA.Templates;

namespace PepperQA.Pages
{
    public class HomePage
    {
        private IPage _page;
        private readonly ILocator _ussername;
        private readonly ILocator _password;
        private readonly ILocator _btnsignup;
        private readonly ILocator _btnlogin;
        private readonly ILocator _btnAvatar;
        private readonly ILocator _btnContrinueWithoutAccept;
        private readonly ILocator _btnAcceptCookies;

        public HomePage(IPage page)
        {
            _page = page;
            _ussername = _page.GetByPlaceholder("Jan_Kowalski");
            _password = _page.GetByPlaceholder("**************");
            _btnsignup = _page.GetByRole(AriaRole.Button, new() { Name = "Zarejestruj się / Zaloguj Się" });
            _btnlogin = _page.GetByRole(AriaRole.Button, new() { Name = "Zaloguj się", Exact = true });
            _btnAvatar = _page.GetByRole(AriaRole.Button, new() { Name = "Awatar użytkownika Temp1" });
            _btnAcceptCookies = _page.GetByRole(AriaRole.Dialog).Filter(new() { HasText = "Cześć! Jesteśmy ciasteczkami! Kontynuuj bez akceptacji Do działania oraz analizy" }).GetByRole(AriaRole.Button, new() { Name = "Akceptuj wszystkie" });
            _btnContrinueWithoutAccept = _page.GetByRole(AriaRole.Dialog).Filter(new() { HasText = "Zaakceptuj wszystkie pliki cookie, aby zobaczyć spersonalizowane okazje Do dział" }).GetByRole(AriaRole.Button, new() { Name = "Akceptuj wszystkie" });
        }

        public async Task ClickSignUp() => await _btnsignup.ClickAsync();

        public async Task SignUp(string username, string password)
        {
            await _ussername.FillAsync(username);
            await _password.FillAsync(password);
            await _btnlogin.ClickAsync();
        }

        public async Task AcceptCookies()
        {
            await _btnAcceptCookies.ClickAsync();
            await _btnContrinueWithoutAccept.WaitForAsync();
            await _btnContrinueWithoutAccept.ClickAsync();
        }

        //public async Task ClickAvatar() => await _btnAvatar.ClickAsync();

        public async Task<bool> AvatarExist(string username)
        {
            await _btnAvatar.ClickAsync();
            return await _page.GetByText(username).IsVisibleAsync();
        }

        public string Title => ".thread-link";

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
